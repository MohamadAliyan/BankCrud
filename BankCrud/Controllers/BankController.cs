using BankCrud.Domain.Entities;
using BankCrud.Service.Interfaces;
using BankCrud.Service.ServiceModels;
using Mapster;
using Microsoft.AspNetCore.Mvc;

namespace BankCrud.Api.Controllers;

public class BankController: ApiControllerBase<Bank,BankServiceModel>
{
    private readonly IBankService _Service;
    public BankController(IBankService service) : base(service)
    {
        _Service = service;
    }

    [HttpPost]
    [Route("insertbank")]
    public  IActionResult Create([FromBody] CreateServiceModel model)
    {
        try
        {
            if (ModelState.IsValid)
            {
                _Service.Insert(model.Adapt<BankServiceModel>(), CurrentUserId);
                return Ok();
            }
            else
            {



                return StatusCode(406, ModelState.Keys.ToString());//Not Acceptable
            }
        }
        catch (Exception ex)
        {

            return BadRequest();
        }
        return Ok();

    }


    [HttpGet]
    [Route("GetAllWithBranchs")]
    public virtual IActionResult GetAllWithBranchs()
    {
        var model = _Service.GetAllWithBranchs();
        return Ok(model);
    }
}