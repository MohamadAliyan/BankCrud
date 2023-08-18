
using System.Linq.Expressions;
using BankCrud.Domain.Entities;
using BankCrud.Repository;
using BankCrud.Repository.Interfaces;
using BankCrud.Service.Interfaces;
using BankCrud.Service.ServiceModels;
using Mapster;

namespace BankCrud.Service.Concrete
{
    public class BaseService<TInput, TResult> : IBaseService<TInput, TResult>
        where TResult : BaseServiceModel
        where TInput : BaseEntity
    {
        private readonly IRepository<TInput> _repository;
        public readonly ApplicationDbContext _context;
        public BaseService(IRepository<TInput> repository, ApplicationDbContext context)
        {
            _repository = repository;
            _context= context;
        }


        public virtual IEnumerable<TResult> GetAll()
        {
       
           var list = _repository.GetAll().ToList().Adapt<IEnumerable<TResult>>();
       
            return list;
        }

        public virtual TResult Get(long id)
        {
            return _repository.Get(id).Adapt<TResult>();

        }

        public virtual TResult GetLast()
        {
            return _repository.GetLast().Adapt<TResult>();

        }



        public virtual void Insert(TResult serviceModel, int currentUserId)
        {
            var config = new TypeAdapterConfig();
            config.NewConfig<TResult, TInput>().MaxDepth(1);
            _repository.Insert(serviceModel.Adapt<TInput>(config), currentUserId);
            _context.SaveChanges();
        }
    

        public virtual long InsertAndGetId(TResult serviceModel, int currentUserId)
        {
            var config = new TypeAdapterConfig();
            config.NewConfig<TResult, TInput>().MaxDepth(1);
            _context.SaveChanges();
            return _repository.InsertAndGetId(serviceModel.Adapt<TInput>(config), currentUserId);

        }


        public virtual void Update(TResult serviceModel, int currentUserId)
        {
            var props = typeof(TResult).GetProperties().Where(p => p.GetGetMethod().IsVirtual).Select(p => p.Name).ToArray();
            TypeAdapterConfig<TResult, TInput>.NewConfig()
                .Ignore(props)
                .Ignore(p => p.AddedDate)
                .Ignore(p => p.CreatorId)
                .Map(p => p.ModifiedDate, s => DateTime.Now)
                .Map(p => p.ModifierId, s => currentUserId)

                ;
            TypeAdapterConfig<TInput, TInput>.NewConfig()
                .Ignore(props)
                .Ignore(p => p.AddedDate)
                .Ignore(p => p.CreatorId)

                .Map(p => p.ModifiedDate, s => DateTime.Now)
                .Map(p => p.ModifierId, s => currentUserId);

            var model = _repository.Get(serviceModel.Id);
            var mapedEntity = serviceModel.Adapt(model);

            var m = mapedEntity.Adapt<TInput>();
            _repository.Update(m, currentUserId);
            _context.SaveChanges();


        }

        public virtual void Delete(long id, int currentUserId)
        {

            _repository.Delete(id, currentUserId);
            _context.SaveChanges();

        }
        

        public IEnumerable<TResult> GetBy(Expression<Func<TResult, bool>> predicate)
        {
            var ff = _repository.GetBy(predicate.ToReplaceParameter<TResult, TInput>())
                .AsEnumerable()
                .Adapt<IEnumerable<TResult>>();
            return ff;

        }
       
    }
    public static class ExpressionExtensions
    {
        public static Expression<Func<TTo, bool>> ToReplaceParameter<TFrom, TTo>(this Expression<Func<TFrom, bool>> target)
        {
            return (Expression<Func<TTo, bool>>)new WhereReplacerVisitor<TFrom, TTo>().Visit(target);
        }
        private class WhereReplacerVisitor<TFrom, TTo> : ExpressionVisitor
        {
            private readonly ParameterExpression _parameter = Expression.Parameter(typeof(TTo), "c");

            protected override Expression VisitLambda<T>(Expression<T> node)
            {
                // replace parameter here
                return Expression.Lambda(Visit(node.Body), _parameter);
            }

            protected override Expression VisitMember(MemberExpression node)
            {
                // replace parameter member access with new type
                if (node.Member.DeclaringType == typeof(TFrom) && node.Expression is ParameterExpression)
                {
                    return Expression.PropertyOrField(_parameter, node.Member.Name);
                }
                return base.VisitMember(node);
            }
        }
    }

}
