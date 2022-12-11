using Diary.Core.Utils;
using Diary.Data.Context;
using Diary.Data.Models.Requests;
using Diary.Data.Services.Interfaces;
using Diary.Domain.Models;

namespace Diary.Data.Services
{
    public class UsersService : IUsersService
    {
        private readonly DiaryContext _dbContext;

        public UsersService(DiaryContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Register(RegisterUserRequest registerUserRequest)
        {
            //Request validation
            registerUserRequest.IsValidOrThrow();

            // Create user
            User user = new User()
            {
                Name = registerUserRequest.Name!,
                Surname = registerUserRequest.Surname!,
                Password = SecretHasher.Hash(registerUserRequest.Password!),
                BirthDateTime = registerUserRequest.BirthDateTime!.Value,
                Gender = registerUserRequest.Gender,
                Email = registerUserRequest.Email!,
                PhoneNumber = registerUserRequest.PhoneNumber!
            };

            _dbContext.Users.Add(user);
            _dbContext.SaveChanges();
        }
    }
}
