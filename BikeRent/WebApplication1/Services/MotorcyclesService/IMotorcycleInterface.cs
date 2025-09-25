using BikeRent.Models;

namespace BikeRent.Services.MotorcyclesService
{
    public interface IMotorcycleInterface
    {
        Task<ServiceResponse<List<Motorcycle>>> GetMotorcycle();
        Task<ServiceResponse<List<Motorcycle>>> GetMotorcycleById(int id);
        Task<ServiceResponse<List<Motorcycle>>> CreateMotorcycle(Motorcycle newMotorcycle);
        Task<ServiceResponse<Motorcycle>> UpdateMotorcyclePlate(int id, string plate);
        Task<ServiceResponse<List<Motorcycle>>> DeleteMotorcycle(int id);
    }
}
