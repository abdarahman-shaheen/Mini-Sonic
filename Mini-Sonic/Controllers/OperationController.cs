using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mini_Sonic.Model;
using Mini_Sonic.Service;
using Mini_Sonic_DAL.Contacts;
using Mini_Sonic_DAL.Model;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Mini_Sonic.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class OperationController : ControllerBase
    {


        private readonly OperationService _operationService;
        private readonly string _connectionString = "Server=JO-SHAHEEN-PC\\SQLEXPRESS;Database=miniSonic;Integrated Security=SSPI;TrustServerCertificate=True";

        public OperationController(IRepository<Operation> operationRepository)
        {
            _operationService = new OperationService(operationRepository);
           
        }

        [HttpGet]
        public IEnumerable<Operation> Get()
        {
            return _operationService.GetAll();
        }

        [HttpGet("details/{operationId}")]
        public ActionResult<IEnumerable<Item>> GetOperationDetails(int operationId)
        {
            try
            {
                var operations = _operationService.GetOperationDetailsByOperationId(operationId);
                return Ok(operations);
            }
            catch (Exception ex)
            {
                // Log the exception or handle it accordingly
                return StatusCode(500, new { message = "Internal server error", error = ex.Message });
            }
        }
        [HttpPost]
        public ActionResult<Result> Post(Operation operation)
        {
            try
            {
                var result = _operationService.Add(operation);
                return Ok(result);
            }
            catch (Exception ex)
            {
                // Log the exception or handle it accordingly
                return StatusCode(500, new { message = "Internal server error", error = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public ActionResult<Result> Put(int id, Operation operation)
        {
            try
            {
                // Assuming you set the Id of the operation to be updated
                operation.Id = id;

                var result = _operationService.Update(operation);
                
                return Ok(result);
            }
            catch (Exception ex)
            {
                // Log the exception or handle it accordingly
                return StatusCode(500, new { message = "Internal server error", error = ex.Message });
            }
        }

            [HttpDelete("{id}")]
        public ActionResult<int> Delete(int id)
        {
            try
            {
                _operationService.Delete(id);
                return Ok(id);
            }
            catch (Exception ex)
            {
                // Log the exception or handle it accordingly
                return StatusCode(500, new { message = "Internal server error", error = ex.Message });
            }
        }

    }
}
