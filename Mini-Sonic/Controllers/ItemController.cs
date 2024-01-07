// ItemController.cs
using Microsoft.AspNetCore.Mvc;
using Mini_Sonic.Model;
using Mini_Sonic.Service;
using Mini_Sonic_DAL.Contacts;
using System;
using System.Collections.Generic;

[Route("api/[controller]")]
[ApiController]
public class ItemController : ControllerBase
{
    private readonly ItemService _itemService;

    public ItemController(IRepository<Item> itemRepository)
    {
        _itemService = new ItemService(itemRepository);
    }

    [HttpGet]
    public IEnumerable<Item> Get()
    {
        return _itemService.GetAll();
    }

    [HttpGet("{id}")]
    public ActionResult<Item> Get(int id)
    {
        var item = _itemService.GetById(id);
        if (item == null)
        {
            return NotFound();
        }
        return Ok(item);
    }

    [HttpPost]
    public ActionResult<Item> Post(Item item)
    {
        var newItem = _itemService.Add(item);
        return CreatedAtAction(nameof(Get), new { id = newItem.Id }, newItem);
    }

    [HttpPut]
    public ActionResult<Item> Put(Item item)
    {
        try
        {
            var updatedItem = _itemService.Update(item);
            return Ok(updatedItem);
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