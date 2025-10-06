using BikeRent.Models;

namespace BikeRent.Services.RentsService
{
    public interface IRentInterface
    {
        Task<ServiceResponse<List<Rent>>> CreateRent(Rent newRent);

        Task<ServiceResponse<List<Rent>>> GetRentById(int id);
        Task<ServiceResponse<string>> UpdateRent(int id);
    }
}