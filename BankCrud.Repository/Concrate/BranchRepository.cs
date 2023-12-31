﻿using BankCrud.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BankCrud.Repository.Concrate;

public class BranchRepository : Repository<Branch>, IBranchRepository
{
    private readonly DbSet<Branch> _entity;
    public BranchRepository(ApplicationDbContext context) : base(context)
    {
        _entity = context.Set<Branch>();
    }
 
}