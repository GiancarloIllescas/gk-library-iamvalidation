namespace GK.Library.IamValidation.Infrastructure.Models
{
    public class AuthValidateResponse
    {
        public AuthValidateResponseData Data { get; set; } = new AuthValidateResponseData();
        public string State { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
    }

    public class AuthValidateResponseData
    {
        public int Id { get; set; }
        public string Username {  get; set; } = string.Empty;
        public string PublicToken { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public string Date { get; set; } = string.Empty;
    }
}
