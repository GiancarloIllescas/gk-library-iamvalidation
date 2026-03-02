namespace GKGeneralLimits.Application.Models.Dtos
{
    public class AuthValidateDto
    {
        public string Authorization { get; set; } = string.Empty;
        public string PublicToken { get; set; } = string.Empty;
        public string AppUserId { get; set; } = string.Empty;
        public string Channel { get; set; } = string.Empty;
    }
}
