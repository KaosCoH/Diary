using Diary.Core.Models;
using Diary.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Diary.Data.Models.Entry;
using Microsoft.Extensions.Configuration;
using Diary.Core.Utils;

namespace Diary.Data.Context
{
    public class DiaryContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public DiaryContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public DiaryContext(DbContextOptions<DiaryContext> options, IConfiguration configuration)
            : base(options)
        {
            _configuration = configuration;
        }

        // Entites
        public DbSet<AuditLog> AuditLogs { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_configuration.GetConnectionString(Constants.DevDatabase));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");
        }

        public override int SaveChanges()
        {
            OnBeforeSaveChanges();

            var entries = ChangeTracker
            .Entries()
            .Where(e => e.Entity is VersionedEntity && (
                    e.State == EntityState.Added
                    || e.State == EntityState.Modified));

            var now = DateTime.Now;

            foreach (var entityEntry in entries)
            {
                ((VersionedEntity)entityEntry.Entity).ModifiedDateTime = now;

                if (entityEntry.State == EntityState.Added)
                {
                    ((VersionedEntity)entityEntry.Entity).CreatedDateTime = now;
                }
            }

            return base.SaveChanges();
        }        

        private void OnBeforeSaveChanges()
        {
            ChangeTracker.DetectChanges();

            var auditEntries = new List<AuditLogEntry>();
            foreach (var entry in ChangeTracker.Entries())
            {
                if (entry.Entity is AuditLog || entry.State == EntityState.Detached || entry.State == EntityState.Unchanged)
                {
                    continue;
                }

                var auditEntry = new AuditLogEntry(entry);
                auditEntry.TableName = entry.Entity.GetType().Name;
                auditEntries.Add(auditEntry);
                foreach (var property in entry.Properties)
                {
                    string propertyName = property.Metadata.Name;
                    switch (entry.State)
                    {
                        case EntityState.Added:
                            auditEntry.AuditType = Enums.AuditType.Create;
                            auditEntry.NewValues[propertyName] = property.CurrentValue!;
                            break;
                        case EntityState.Deleted:
                            auditEntry.AuditType = Enums.AuditType.Delete;
                            auditEntry.OldValues[propertyName] = property.OriginalValue!;
                            break;
                        case EntityState.Modified:
                            if (property.IsModified)
                            {
                                auditEntry.ChangedColumns.Add(propertyName);
                                auditEntry.AuditType = Enums.AuditType.Update;
                                auditEntry.OldValues[propertyName] = property.OriginalValue!;
                                auditEntry.NewValues[propertyName] = property.CurrentValue!;
                            }
                            break;
                    }
                }
            }
            foreach (var auditEntry in auditEntries)
            {
                AuditLogs.Add(auditEntry.ToAuditLog());
            }
        }

        public class DiaryContextFactory : IDesignTimeDbContextFactory<DiaryContext>
        {
            private readonly IConfiguration _configuration;

            public DiaryContextFactory(IConfiguration configuration)
            {
                _configuration = configuration;
            }

            public DiaryContext CreateDbContext(string[] args)
            {
                var optionsBuilder = new DbContextOptionsBuilder<DiaryContext>();
                optionsBuilder.UseSqlServer(_configuration.GetConnectionString(Constants.DevDatabase));

                return new DiaryContext(optionsBuilder.Options, _configuration);
            }
        }
    }
}
