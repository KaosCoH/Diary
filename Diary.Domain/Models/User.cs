using Diary.Core.Models;

namespace Diary.Domain.Models
{
    public class User : VersionedEntity
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Password { get; set; }
        public DateTime BirthDateTime { get; set; }
        public string? Gender { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
    }
}
