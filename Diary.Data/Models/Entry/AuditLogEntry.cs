using Diary.Data.Enums;
using Diary.Domain.Models;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Newtonsoft.Json;

namespace Diary.Data.Models.Entry
{
    public class AuditLogEntry
    {
        public AuditLogEntry(EntityEntry entry)
        {
            Entry = entry;
        }
        public EntityEntry Entry { get; }
        public string TableName { get; set; }
        public Dictionary<string, object> OldValues { get; } = new Dictionary<string, object>();
        public Dictionary<string, object> NewValues { get; } = new Dictionary<string, object>();
        public AuditType AuditType { get; set; }
        public List<string> ChangedColumns { get; } = new List<string>();

        public AuditLog ToAuditLog()
        {
            var audit = new AuditLog();
            audit.Type = AuditType.ToString();
            audit.TableName = TableName;
            audit.DateTime = DateTime.Now;
            audit.OldValues = OldValues.Count == 0 ? null : JsonConvert.SerializeObject(OldValues);
            audit.NewValues = NewValues.Count == 0 ? null : JsonConvert.SerializeObject(NewValues);
            audit.AffectedColumns = ChangedColumns.Count == 0 ? null : JsonConvert.SerializeObject(ChangedColumns);
            return audit;
        }
    }
}
