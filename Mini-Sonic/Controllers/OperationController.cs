using Microsoft.AspNetCore.Mvc;
using Mini_Sonic.DTO;
using Mini_Sonic.Model;
using Mini_Sonic.Service;
using Mini_Sonic_DAL.Contacts;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Mini_Sonic.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OperationController : ControllerBase
    {
        private readonly OperationService _operationService;

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
        public ActionResult<IEnumerable<OperationDetailDTO>> GetOperationDetails(int operationId)
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
        public ActionResult<Operation> Post(Operation operation)
        {
            try
            {
                var newOperation = _operationService.Add(operation);
                return Ok(newOperation);
            }
            catch (Exception ex)
            {
                // Log the exception or handle it accordingly
                return StatusCode(500, new { message = "Internal server error", error = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public ActionResult<Operation> Put(int id, Operation operation)
        {
            try
            {
                // Assuming you set the Id of the operation to be updated
                operation.Id = id;

                var updatedOperation = _operationService.Update(operation);
                return Ok(updatedOperation);
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
