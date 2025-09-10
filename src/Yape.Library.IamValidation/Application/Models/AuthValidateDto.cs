
namespace YapeGeneralLimits.Application.Models.Dtos
{
    public class AuthValidateDto
    {
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string PublicToken { get; set; } = string.Empty;
        public string AppUserId { get; set; } = string.Empty;
        public string Channel { get; set; } = string.Empty;
    }
}