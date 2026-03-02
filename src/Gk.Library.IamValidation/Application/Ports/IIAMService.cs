using System.Threading.Tasks;
using GK.Library.IamValidation.Infrastructure.Models;

namespace GK.Library.IamValidation.Application.Ports
{
    public interface IIamService
    {
        Task<AuthValidateResponse> Validate(AuthValidateRequest request);
    }
}
