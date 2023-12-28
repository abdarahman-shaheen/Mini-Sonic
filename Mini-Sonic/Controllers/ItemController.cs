using Microsoft.AspNetCore.Mvc;
using Mini_Sonic.Model;
using Mini_Sonic.Service;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Mini_Sonic.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        private readonly ItemService _serviceItem;

        public ItemController(ItemService serviceItem)
        {
            _serviceItem = serviceItem;
            
        }
        // GET: api/<ItemController>
        [HttpGet]
        public IEnumerable<Item> Get()
        {
            var items = _serviceItem.GetAllItems();
            return items;
        }

        // GET api/<ItemController>/5
        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        // POST api/<ItemController>
        [HttpPost]
        public Item Post(Item item)
        {
            var newItem = _serviceItem.AddItem(item);
            return newItem;
        }

        [HttpPut("{id}")]
        public ActionResult<Item> Put(int id, [FromBody] Item item)
        {
            try
            {
                // Assuming you set the Id of the item to be updated
                item.Id = id;

                var updatedItem = _serviceItem.UpdateItem(item);
                return Ok(updatedItem);
            }
            catch (Exception ex)
            {
                // Log the exception or handle it accordingly
                return StatusCode(500, new { message = "Internal server error", error = ex.Message });
            }
        }

        // DELETE api/<ItemController>/5
        [HttpDelete("{id}")]
        public ActionResult<int> Delete(int id)
        {
            try
            {
                _serviceItem.DeleteItem(id);
                return Ok(id);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Internal server error", error = ex.Message });
            }
        }
    }
}
