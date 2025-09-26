using BikeRent.Models;
using BikeRent.Services.MotorcyclesService;
using Microsoft.AspNetCore.Mvc;

namespace BikeRent.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MotorcyclesController : ControllerBase
{
    public readonly IMotorcycleInterface _motorcyclesInterface;
    public MotorcyclesController(IMotorcycleInterface motorcyclesInterface)
    {
        _motorcyclesInterface = motorcyclesInterface;
    }


    [HttpPost]
    public async Task<ActionResult<ServiceResponse<List<Motorcycle>>>> CreateMotorcycle(Motorcycle newMotorcycle)
    {
        return Ok(await _motorcyclesInterface.CreateMotorcycle(newMotorcycle));
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<ServiceResponse<Motorcycle>>> DeleteMotorcycle(int id)
    {
        return Ok(await _motorcyclesInterface.DeleteMotorcycle(id));
    }

    [HttpGet]
    public async Task<ActionResult<ServiceResponse<List<Motorcycle>>>> Get()
    {
        return Ok(await _motorcyclesInterface.GetMotorcycle());
    }
    [HttpGet("{id}")]
    public async Task<ActionResult<ServiceResponse<List<Motorcycle>>>> GetMotorcycleById(int id)
    {
        return Ok(await _motorcyclesInterface.GetMotorcycleById(id));
    }
    [HttpPut("{id}, {plate}")]
    public async Task<ActionResult<ServiceResponse<Motorcycle>>> UpdateMotorcyclePlate(int id, string plate)
    {
        return Ok(await _motorcyclesInterface.UpdateMotorcyclePlate(id, plate));
    }
}
