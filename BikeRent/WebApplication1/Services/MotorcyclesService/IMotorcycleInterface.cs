using BikeRent.Models;

namespace BikeRent.Services.MotorcyclesService
{
    public interface IMotorcycleInterface
    {
        Task<ServiceResponse<List<Motorcycle>>> CreateMotorcycle(Motorcycle newMotorcycle);

        Task<ServiceResponse<List<Motorcycle>>> DeleteMotorcycle(int id);

        Task<ServiceResponse<List<Motorcycle>>> GetMotorcycle();
        Task<ServiceResponse<List<Motorcycle>>> GetMotorcycleById(int id);
        Task<ServiceResponse<Motorcycle>> UpdateMotorcyclePlate(int id, string plate);
    }
}
