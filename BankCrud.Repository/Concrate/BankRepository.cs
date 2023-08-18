using BankCrud.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BankCrud.Repository.Concrate;

public class BankRepository : Repository<Bank>, IBankRepository
{
    private readonly DbSet<Bank> _entity;
    public BankRepository(ApplicationDbContext context) : base(context)
    {
        _entity = context.Set<Bank>();
    }
  
}