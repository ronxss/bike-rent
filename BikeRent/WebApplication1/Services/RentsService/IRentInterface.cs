using BikeRent.Models;

namespace BikeRent.Services.RentsService
{
    public interface IRentInterface
    {
        Task<ServiceResponse<List<Rent>>> CreateRent(Rent newRent);

        Task<ServiceResponse<Rent>> GetRentById(int id);
        Task<ServiceResponse<Rent>> UpdateRent(int id);
    }
}