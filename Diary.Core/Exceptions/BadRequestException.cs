using Diary.Core.Localization;
using Microsoft.AspNetCore.Http;

namespace Diary.Core.Exceptions
{
    public class BadRequestException : ApiException
    {
        public BadRequestException() : base(errorKey, StatusCodes.Status400BadRequest) { }
        public BadRequestException(string message) : base(errorKey, StatusCodes.Status400BadRequest, BadRequestMessage(message)) { }
        public BadRequestException(string message, Exception innerException) : base(errorKey, StatusCodes.Status400BadRequest, BadRequestMessage(message), innerException) { }

        public const string errorKey = "BadRequest";

        private static string BadRequestMessage(string message)
        {
            return $"{ExceptionMessages.ListOfInvalidParameters}: {message}";
        }
    }
}
