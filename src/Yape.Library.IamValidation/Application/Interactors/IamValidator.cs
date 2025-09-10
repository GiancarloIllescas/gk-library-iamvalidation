using System;
using System.Configuration;
using System.Threading.Tasks;
using Yape.Library.IamValidation.Application.Ports;
using Yape.Library.IamValidation.Infrastructure.Adapters;
using Yape.Library.IamValidation.Infrastructure.Models;
using YapeGeneralLimits.Application.Models.Dtos;

namespace Yape.Library.IamValidation.Application.Interactors
{
    public class IamValidator
    {
        private static readonly Lazy<IIamService> _lazyInstance = new Lazy<IIamService>(CreateProvider);
        private static readonly ICacheProvider _cache = new CacheProvider();
        private static readonly double _cacheMinutesTTL = double.TryParse(ConfigurationManager.AppSettings.Get("IAM.Validation.CacheMinutesTTL"), out double ttl) ? ttl : 60;
        private const string TransaccionCompletadaCorrectamente = "00";
        private const string _cacheKey = "yape:iamvalidator:{0}";

        private static IIamService CreateProvider()
        {
            var opt = IamServiceSetting.FromAppSettings();

            return new IamService(opt);
        }

        public static async Task<bool> Validate(AuthValidateDto dto)
        {
            var cacheKey = string.Format(_cacheKey, dto.Username);

            bool? authenticated = _cache.Get<bool?>(cacheKey);

            if (authenticated is not null)
            {
                return (bool)authenticated;
            }
            
            var req = new AuthValidateRequest
            {
                Username = dto.Username,
                Password = dto.Password,
                PublicToken = dto.PublicToken,
                AppUserId = dto.AppUserId,
                Channel = dto.Channel,
                Date = DateTime.Now.ToString("yyyyMMdd")
            };

            var resp = await _lazyInstance.Value.Validate(req).ConfigureAwait(false);

            if (resp is not null && resp.State == TransaccionCompletadaCorrectamente)
            {
                _cache.Set<bool>(cacheKey, true, TimeSpan.FromMinutes(_cacheMinutesTTL));

                return true;
            }

            return false;
        }
    }
}
