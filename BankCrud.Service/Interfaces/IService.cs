
using BankCrud.Domain.Entities;
using BankCrud.Service.ServiceModels;
using System.Linq.Expressions;


namespace BankCrud.Service.Interfaces
{
   
    public interface IBaseService<TInput, TResult>
        where TInput : BaseEntity
        where TResult : BaseServiceModel
    {
        IEnumerable<TResult> GetAll();
        TResult Get(long id);
        TResult GetLast();
        void Insert(TResult model, int currentUserId);
        long InsertAndGetId(TResult entity, int currentUserId);
        void Update(TResult model, int currentUserId);
        void Delete(long id, int currentUserId);
        IEnumerable<TResult> GetBy(Expression<Func<TResult, bool>> predicate);
    }
}
