﻿using System.Linq.Expressions;
using BankCrud.Domain.Entities;
using BankCrud.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BankCrud.Repository.Concrate;

public class Repository<T> : IRepository<T> where T : BaseEntity

{

    private readonly ApplicationDbContext _context;
    private readonly DbSet<T> _entity;
    string errorMessage = string.Empty;

    public Repository(ApplicationDbContext context)
    {
        _context = context;

        _entity = context.Set<T>();
    }

    public virtual IQueryable<T> GetAll()
    {
        return _entity;
    }



    public virtual T GetLast()
    {
        return _entity.OrderByDescending(r => r.Id).First();
    }

    public  T Get(long id)
    {
        return _entity.SingleOrDefault(s => s.Id == id);
    }


    public virtual void Insert(T entity, int currentUserId)
    {

        if (entity == null)
        {
            throw new ArgumentNullException(nameof(entity));
        }
       
        _entity.Add(entity);
   

    }

    public virtual long InsertAndGetId(T entity, int currentUserId)
    {

        if (entity == null)
        {
            throw new ArgumentNullException(nameof(entity));
        }
        
        _entity.Add(entity);
      
        return entity.Id;

    }

    public virtual void Update(T entity, int currentUserId)
    {

        if (entity == null)
        {
            throw new ArgumentNullException(nameof(entity));
        }
    _entity.Update(entity);

    }


    public virtual void Delete(long id, int currentUserId)
    {
        var entity = Get(id);
        if (entity == null)
        {
            throw new ArgumentNullException(nameof(entity));
        }

        _entity.Remove(entity);



    }

    public virtual IQueryable<T> GetBy(Expression<Func<T, bool>> predicate)
    {
        return _entity.Where(predicate);
    }

    public virtual IQueryable<T> Get()
    {
        return _entity.AsQueryable();
    }
}