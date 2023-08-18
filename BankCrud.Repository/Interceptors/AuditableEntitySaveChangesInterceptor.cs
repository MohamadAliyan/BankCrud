

using BankCrud.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;


namespace BankCrud.Interceptors;
public class AuditableEntitySaveChangesInterceptor : SaveChangesInterceptor
{



    public AuditableEntitySaveChangesInterceptor(
        //  ICurrentUserService currentUserService
        )
    {
        //_currentUserService = currentUserService;

    }

    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        UpdateEntities(eventData.Context);

        return base.SavingChanges(eventData, result);
    }

    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
    {
        UpdateEntities(eventData.Context);

        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    public void UpdateEntities(DbContext? context)
    {
        if (context == null) return;

        foreach (var entry in context.ChangeTracker.Entries<BaseEntity>())
        {
            if (entry.State == EntityState.Added)
            {
                //entry.Entity.CreatedBy = _currentUserService.UserId;
                entry.Entity.CreatorId = 1;
                entry.Entity.AddedDate = DateTime.Now;
            }

            if (entry.State == EntityState.Modified || entry.HasChangedOwnedEntities())
            {
                //entry.Entity.LastModifiedBy = _currentUserService.UserId;
                entry.Entity.ModifierId = 1;
                entry.Entity.ModifiedDate = DateTime.Now;
            }
            if (entry.State == EntityState.Deleted || entry.HasChangedOwnedEntities())
            {
                //entry.Entity.DeletedBy = _currentUserService.UserId;
                entry.Entity.DeletorId= 1;
                entry.Entity.DeletedDate = DateTime.Now;
                entry.State = EntityState.Modified;
                entry.Entity.IsDeleted = true;

            }

        }
    }
}

public static class Extensions
{
    public static bool HasChangedOwnedEntities(this EntityEntry entry) =>
        entry.References.Any(r =>
            r.TargetEntry != null &&
            r.TargetEntry.Metadata.IsOwned() &&
            (r.TargetEntry.State == EntityState.Added || r.TargetEntry.State == EntityState.Modified || r.TargetEntry.State == EntityState.Deleted));
}
