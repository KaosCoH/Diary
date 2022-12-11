using Diary.Core.Exceptions;
using Diary.Core.Extensions;

namespace Diary.Data.Models.Requests
{
    public class RegisterUserRequest
    {
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? Password { get; set; }
        public DateTime? BirthDateTime { get; set; }
        public string? Gender { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }

        public void IsValidOrThrow()
        {
            List<string> invalidParameters = new();

            if(Name.IsNullOrEmptyOrNullString())
            {
                invalidParameters.Add(nameof(Name));
            }

            if (Surname.IsNullOrEmptyOrNullString())
            {
                invalidParameters.Add(nameof(Surname));
            }

            if (Password.IsNullOrEmptyOrNullString())
            {
                invalidParameters.Add(nameof(Password));
            }

            if (!BirthDateTime.HasValue)
            {
                invalidParameters.Add(nameof(BirthDateTime));
            }

            if (Gender.IsNullOrEmptyOrNullString())
            {
                invalidParameters.Add(nameof(Gender));
            }

            if (Email.IsNullOrEmptyOrNullString())
            {
                invalidParameters.Add(nameof(Email));
            }

            if (PhoneNumber.IsNullOrEmptyOrNullString())
            {
                invalidParameters.Add(nameof(PhoneNumber));
            }

            if (invalidParameters.Any())
            {
                throw new BadRequestException(string.Join(", ", invalidParameters));
            }
        }
    }
}
