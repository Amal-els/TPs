using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using TP3.Models;
public class AuditInterceptor : SaveChangesInterceptor
{
    public override InterceptionResult<int> SavingChanges(
        DbContextEventData eventData,
        InterceptionResult<int> result)
    {
        var context = eventData.Context;
        if (context == null) return base.SavingChanges(eventData, result);

        var auditEntries = new List<AuditLog>();

        foreach (var entry in context.ChangeTracker.Entries())
        {
            if (entry.Entity is AuditLog) continue; // ne pas logger l’audit lui-même

            if (entry.State == EntityState.Added ||
                entry.State == EntityState.Modified ||
                entry.State == EntityState.Deleted)
            {
                var audit = new AuditLog
                {
                    TableName = entry.Entity.GetType().Name,
                    Action = entry.State.ToString(),
                    EntityKey = GetPrimaryKey(entry),
                    Changes = GetChangesJson(entry)
                };
                auditEntries.Add(audit);
            }
        }

        context.Set<AuditLog>().AddRange(auditEntries);

        return base.SavingChanges(eventData, result);
    }

    private string GetPrimaryKey(EntityEntry entry)
    {
        var key = entry.Properties.FirstOrDefault(p => p.Metadata.IsPrimaryKey());
        return key?.CurrentValue?.ToString() ?? "";
    }

    private string GetChangesJson(EntityEntry entry)
    {
        if (entry.State == EntityState.Modified)
        {
            var changes = entry.Properties
                .Where(p => p.IsModified)
                .ToDictionary(p => p.Metadata.Name, p => new { Old = p.OriginalValue, New = p.CurrentValue });

            return JsonSerializer.Serialize(changes);
        }
        return null;
    }
}
