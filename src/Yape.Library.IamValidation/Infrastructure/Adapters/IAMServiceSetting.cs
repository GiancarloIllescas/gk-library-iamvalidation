using System;

namespace Yape.Library.IamValidation.Infrastructure.Adapters
{
    public class IamServiceSetting
    {
        public string BaseUrl { get; set; } = string.Empty;
        public string EndpointAuthValidate { get; set; } = string.Empty;
        public int TimeOutInSeconds { get; set; } = 30;
        
        public static IamServiceSetting FromAppSettings()
        {
            var a = System.Configuration.ConfigurationManager.AppSettings;

            return new IamServiceSetting
            {
                BaseUrl = GetRequired(a, "IAM.BaseUrl"),
                EndpointAuthValidate = GetRequired(a, "IAM.EndpointAuthValidate"),
                TimeOutInSeconds = int.TryParse(GetRequired(a, "IAM.TimeOutInSeconds"), out int ttl) ? ttl : 60
            };
        }

        private static string GetRequired(System.Collections.Specialized.NameValueCollection a, string key)
            => a[key] is null
               ? throw new InvalidOperationException($"AppSetting faltante o vacía: '{key}'.")
               : a[key];
    }
}