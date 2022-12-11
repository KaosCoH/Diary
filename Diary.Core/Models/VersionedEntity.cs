namespace Diary.Core.Models
{
    public class VersionedEntity : Entity
    {
        public DateTime CreatedDateTime { get; set; }
        public DateTime ModifiedDateTime { get; set; }
    }
}
