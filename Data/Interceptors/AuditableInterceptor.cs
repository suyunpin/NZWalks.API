using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Data.Interceptors;

public sealed class AuditableInterceptor : SaveChangesInterceptor
{
    public override InterceptionResult<int> SavingChanges(
        DbContextEventData eventData, InterceptionResult<int> result)
    {
        SetTime(eventData.Context);
        return result;
    }

    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData, InterceptionResult<int> result,
        CancellationToken cancellationToken = default)
    {
        SetTime(eventData.Context);
        return new ValueTask<InterceptionResult<int>>(result);
    }

    private static void SetTime(DbContext? ctx)
    {
        if (ctx == null) return;
        foreach (var entry in ctx.ChangeTracker.Entries<Region>())
        {
            if (entry.State == EntityState.Added)
            {
                entry.Entity.Time = DateTime.UtcNow;
                Console.WriteLine($"[AuditableInterceptor] 设置 Time = {entry.Entity.Time:O}");
            }
        }
    }
}