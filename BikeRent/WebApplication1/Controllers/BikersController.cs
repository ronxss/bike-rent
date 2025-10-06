using BikeRent.Models;
using BikeRent.Services.BikersService;
using Microsoft.AspNetCore.Mvc;

namespace BikeRent.Controllers;

[Route("api/[controller]")]
    [ApiController]
    public class BikersController : ControllerBase
    {
        private readonly IBikersInterface _bikersInterface;

        public BikersController(IBikersInterface bikersInterface)
        {
            _bikersInterface = bikersInterface;
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<List<Biker>>>> CreateBiker(Biker newBiker)
            {
            return Ok(await _bikersInterface.CreateBiker(newBiker));
            }
        [HttpPut]
        public async Task<ActionResult<ServiceResponse<List<Biker>>>> UpdateBikerCnh(int id, byte[] cnhImage)
        {
            return Ok(await _bikersInterface.UpdateBikerCnh(id, cnhImage));
        }
    }

