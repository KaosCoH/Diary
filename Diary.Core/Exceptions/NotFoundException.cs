using Microsoft.AspNetCore.Http;

namespace Diary.Core.Exceptions
{
    public class NotFoundException : ApiException
    {
        public NotFoundException(string errorKey) : base(errorKey, StatusCodes.Status404NotFound) { }
        public NotFoundException(string errorKey, string message) : base(errorKey, StatusCodes.Status404NotFound, message) { }
        public NotFoundException(string errorKey, string message, Exception innerException) : base(errorKey, StatusCodes.Status404NotFound, message, innerException) { }
    }
}