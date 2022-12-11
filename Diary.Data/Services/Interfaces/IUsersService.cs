using Diary.Data.Models.Requests;

namespace Diary.Data.Services.Interfaces
{
    public interface IUsersService
    {
        public void Register(RegisterUserRequest registerUserRequest);
    }
}
