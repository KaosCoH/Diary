namespace Diary.Core.Models
{
    public class ErrorModel
    {
        public ErrorModel(string erroyKey, string exceptionName, string message, int statusCode) 
        {
            ErrorKey = erroyKey;
            ExceptionName = exceptionName;
            Message = message;
            StatusCode = statusCode;
        }

        public string ErrorKey { get; private set; }
        public string ExceptionName { get; private set; }
        public string Message { get; private set; }
        public int StatusCode { get; private set; }
    }
}
