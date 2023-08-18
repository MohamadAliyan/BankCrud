
using BankCrud.Domain.Entities;
using BankCrud.Interceptors;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
namespace BankCrud.Repository;

public interface IApplicationContext
{
    int SaveChanges();

}
public class ApplicationDbContext : DbContext, IApplicationContext
{
    private readonly AuditableEntitySaveChangesInterceptor _auditableEntitySaveChangesInterceptor;
    public ApplicationDbContext(DbContextOptions options, AuditableEntitySaveChangesInterceptor auditableEntitySaveChangesInterceptor)
        : base(options)
    {
        _auditableEntitySaveChangesInterceptor = auditableEntitySaveChangesInterceptor;
    }



    //public DbSet<Branch> Branchs { get; set; }

    public override int SaveChanges()
    {
        return base.SaveChanges();
    }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(builder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.AddInterceptors(_auditableEntitySaveChangesInterceptor);
    }

    //public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    //{
       

    //    return await base.SaveChangesAsync(cancellationToken);
    //}
}




