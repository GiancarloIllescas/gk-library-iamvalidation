namespace GK.Library.IamValidation.Infrastructure.Models
{
    public class AuthValidateRequest
    {
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string PublicToken { get; set; } = string.Empty;
        public string AppUserId { get; set; } = string.Empty;
        public string Date { get; set; } = string.Empty;
        public string Channel { get; set; } = string.Empty;
    }
}
