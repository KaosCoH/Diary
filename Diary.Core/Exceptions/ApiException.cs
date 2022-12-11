namespace Diary.Core.Exceptions
{
    public class ApiException : Exception
    {
        public string ErrorKey { get; }
        public int StatusCode { get; }

        public ApiException(string errorKey, int statusCode) : base()
        {
            ErrorKey = errorKey;
            StatusCode = statusCode;
        }

        public ApiException(string errorKey, int statusCode, string message) : base(message)
        {
            ErrorKey = errorKey;
            StatusCode = statusCode;
        }

        public ApiException(string errorKey, int statusCode, string message, Exception innerException) : base(message, innerException)
        {
            ErrorKey = errorKey;
            StatusCode = statusCode;
        }
    }
}
