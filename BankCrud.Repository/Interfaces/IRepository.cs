using BankCrud.Domain.Entities;
using System.Linq.Expressions;

namespace BankCrud.Repository.Interfaces;

public interface IRepository<T> where T : BaseEntity
{
   
    IQueryable<T> GetAll();

    T Get(long id);
    T GetLast();
    void Insert(T entity, int currentUserId);
    long InsertAndGetId(T entity, int currentUserId);
    void Update(T entity, int currentUserId);
    void Delete(long id, int currentUserId);

    IQueryable<T> GetBy(Expression<Func<T, bool>> predicate);
    IQueryable<T> Get();
    


}