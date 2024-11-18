namespace AuthenticationService.Base
{
    public class ResponseModel<TResult>
    {
        public  TResult? Result { get; set; }
        public string? Error { get; set; }
        public bool IsValid { get; set; }
    }
}
