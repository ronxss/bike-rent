using BikeRent.Models;

namespace BikeRent.Services.BikersService
{
    public interface IBikersInterface
    {
        Task<ServiceResponse<List<Biker>>> CreateBiker(Biker newBiker);
        Task<ServiceResponse<Biker>> UpdateBikerCnh(int id, byte[] imageCnh);

    }
}
