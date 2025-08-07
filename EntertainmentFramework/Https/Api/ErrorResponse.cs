namespace EntertainmentFramework.Api
{
    public class ErrorResponse
    {
        public int Status;
        public string Message;
    }

    public class OauthErrorResponse
    {
        public string Error;
        public string ErrorDescription;
        public string ErrorUri;
    }
}