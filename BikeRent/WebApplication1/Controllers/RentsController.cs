using BikeRent.Models;
using BikeRent.Services.RentsService;
using Microsoft.AspNetCore.Mvc;

namespace BikeRent.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RentsController : ControllerBase
    {
        private readonly IRentInterface _rentsInterface;

        public RentsController(IRentInterface rentsInterface)
        {
            _rentsInterface = rentsInterface;
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<List<Rent>>>> CreateRent(Rent newRent)
        {
            return Ok(await _rentsInterface.CreateRent(newRent));
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<List<Rent>>>> GetRentById(int id)
        {
            return Ok(await _rentsInterface.GetRentById(id));
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<ServiceResponse<Rent>>> UpdateRent(int id)
        {
            return Ok(await _rentsInterface.UpdateRent(id));
        }
    }

}