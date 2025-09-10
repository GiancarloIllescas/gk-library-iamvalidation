using System.Threading.Tasks;
using Yape.Library.IamValidation.Infrastructure.Models;

namespace Yape.Library.IamValidation.Application.Ports
{
    public interface IIamService
    {
        Task<AuthValidateResponse> Validate(AuthValidateRequest request);
    }
}