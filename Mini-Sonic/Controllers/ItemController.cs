// ItemController.cs
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mini_Sonic.Model;
using Mini_Sonic.Service;
using Mini_Sonic_DAL.Contacts;
using Mini_Sonic_DAL.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
[Authorize]
[Route("api/[controller]")]
[ApiController]
public class ItemController : BaseController
{
    private readonly ItemService _itemService;
    protected readonly IConfiguration _configuration;
    public ItemController(IRepository<Item> itemRepository,IConfiguration configuration) : base(configuration)
    {
        _itemService = new ItemService(itemRepository);
}

    [HttpGet]
    public ActionResult<IEnumerable<Item>> Get()
    {
        try
        {
            var operation = _itemService.GetAll();
            return Ok(operation);
        }

        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Internal server error", error = ex.Message });
        }
       
    }

    [HttpGet("{id}")]
    public ActionResult<Item> Get(int id)
    {
        try
        {
            var item = _itemService.GetById(id);
            if (item == null)
            {
                return NotFound();
            }
            return Ok(item);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Internal server error", error = ex.Message });
        }
    }

    [HttpPost]
    public ActionResult<Item> Post(Item item)
    {
        try
        {
            var result = _itemService.Add(item);

           return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Internal server error", error = ex.Message });
        }
    }

    [HttpPut]
    public ActionResult<Item> Put(Item item)
    {
        try
        {
            var result = _itemService.Update(item);

            if (result == Result.Success)
            {
                return Ok(item);
            }
            else
            {
                return StatusCode(500, new { message = "Internal server error", error = "Failed to update item" });
            }
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Internal server error", error = ex.Message });
        }
    }

    [HttpDelete("{id}")]
    public ActionResult<int> Delete(int id)
    {
        try
        {
            _itemService.Delete(id);
            return Ok(id);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Internal server error", error = ex.Message });
        }
    }
}