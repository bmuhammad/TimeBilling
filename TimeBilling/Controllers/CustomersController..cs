using FreeBilling.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using TimeBilling.Data;

namespace TimeBilling.Controllers
{
    [Route("/api/[controller]")]
    public class CustomersController : ControllerBase
    {
        public ILogger<CustomersController> _logger;
        private readonly IBillingRepository _repository;

        public CustomersController(ILogger<CustomersController> logger, IBillingRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }



        [HttpGet("")]
        public async Task<IEnumerable<Customer>> Get()
        {
            return await _repository.GetCustomers();

        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Customer?>> GetOne(int id)
        {
            try
            {
                var result = await _repository.GetCustomers(id);

                if (result is null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(result);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception thrown while reading customer");

                return Problem($"Exception thrown: {ex.Message}");
            }


        }
    }
}
