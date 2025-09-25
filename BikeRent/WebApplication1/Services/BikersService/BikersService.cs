using BikeRent.Data;
using BikeRent.Models;
using Microsoft.EntityFrameworkCore;

namespace BikeRent.Services.BikersService
{
    public class BikersService
    {
        private readonly BikeRentDb _context;

        public BikersService(BikeRentDb context)
        {
            _context = context;
        }

        public async Task<ServiceResponse<List<Biker>>> CreateBiker(Biker newBiker)
        {
            ServiceResponse<List<Biker>> serviceResponse = new ServiceResponse<List<Biker>>();

            try
            {

                if (newBiker == null)
                {
                    serviceResponse.Dados = null;
                    serviceResponse.Mensagem = "Informe os dados";
                    serviceResponse.Sucesso = false;

                }
                _context.Add(newBiker);
                await _context.SaveChangesAsync();

                serviceResponse.Dados = _context.Bikers.ToList();
            }
            catch (Exception ex)
            {
                serviceResponse.Mensagem = ex.Message;
                serviceResponse.Sucesso = false;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<Biker>> UpdateBikerCnh(int id, byte[] cnhImage)
        {

            ServiceResponse<Biker> serviceResponse = new ServiceResponse<Biker>();

            try
            {
                Biker biker = _context.Bikers.FirstOrDefault(b => b.Id == id);
                if (biker == null)
                {
                    serviceResponse.Dados = null;
                    serviceResponse.Mensagem = "Nenhum entregador encontrado";
                    serviceResponse.Sucesso = false;
                }

                biker.CnhImage = cnhImage;

                _context.Bikers.Update(biker);
                await _context.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                serviceResponse.Mensagem = ex.Message;
                serviceResponse.Sucesso = false;
            }

            return serviceResponse;

        }
    }
}
