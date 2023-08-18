using BankCrud.Domain.Entities;
using BankCrud.Repository;
using BankCrud.Service.Interfaces;
using BankCrud.Service.ServiceModels;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace BankCrud.Service.Concrete;

public class BankService : BaseService<Bank, BankServiceModel>, IBankService
{
    private readonly IBankRepository _BankRepository;


    public BankService(IBankRepository BankRepository, ApplicationDbContext context) : base(BankRepository, context)
    {
        _BankRepository = BankRepository;

    }

    public override IEnumerable<BankServiceModel> GetAll()
    {
        var s = _BankRepository.GetAll().Include(p => p.Branches).ToList();
        var config = new TypeAdapterConfig();
        config.NewConfig<Bank, BankServiceModel>().MaxDepth(2);
        var list = s.Adapt<List<BankServiceModel>>(config);
        return list;
    }

}