using BikeRent.Data;
using BikeRent.Models;
using BikeRent.Enums;
using Microsoft.EntityFrameworkCore;


namespace BikeRent.Services.RentsService
{
    public class RentsService
    {
        private readonly BikeRentDb _context;

        public RentsService(BikeRentDb context)
        {
            _context = context;
        }

        public async Task<ServiceResponse<List<Rent>>> CreateRent(Rent newRent)
        {
            ServiceResponse<List<Rent>> serviceResponse = new ServiceResponse<List<Rent>>();

            Biker biker = await _context.Bikers.FirstAsync(b => b.Id == newRent.BikerId);

            try
            {

                if (newRent == null)
                {
                    serviceResponse.Dados = null;
                    serviceResponse.Mensagem = "Informe os dados";
                    serviceResponse.Sucesso = false;

                }

                if (biker.CnhType != "A" && biker.CnhType != "A+B")
                {
                    serviceResponse.Dados = null;
                    serviceResponse.Mensagem = "Entregador não habilitado para categoria A.";
                    serviceResponse.Sucesso = false;
                }

                decimal dailyPrice = newRent.Plan switch
                {
                    PlansEnum.SeteDias => 30,
                    PlansEnum.QuinzeDias => 28,
                    PlansEnum.TrintaDias => 22,
                    PlansEnum.QuarentaCincoDias => 20,
                    PlansEnum.CinquentaDias => 18,
                    _ => throw new ArgumentOutOfRangeException()
                };

                newRent.StartDate = DateTime.Today.AddDays(1);
                newRent.PreviewEndDate = newRent.StartDate.AddDays((int)newRent.Plan);
                newRent.DailyValue = (int)dailyPrice * (int)newRent.Plan;

                _context.Add(newRent);
                
                await _context.SaveChangesAsync();

                serviceResponse.Dados = _context.Rents.ToList();
            }
            catch (Exception ex)
            {
                serviceResponse.Mensagem = ex.Message;
                serviceResponse.Sucesso = false;
            }

            return serviceResponse;
        }
       
        public async Task<ServiceResponse<List<Rent>>> GetRentById(int id)
        {
            ServiceResponse<List<Rent>> serviceResponse = new ServiceResponse<List<Rent>>();

            try
            {
                serviceResponse.Dados = await _context.Rents.Where(r => r.Id == id).ToListAsync();
            }
            catch (Exception ex)
            {
                serviceResponse.Mensagem = ex.Message;
                serviceResponse.Sucesso = false;
            }

            return serviceResponse;
        }
        public async Task<ServiceResponse<Motorcycle>> UpdateRent(int id, string plate)
        {

            ServiceResponse<Motorcycle> serviceResponse = new ServiceResponse<Motorcycle>();

            try
            {
                Motorcycle motorcycle = _context.Motorcycles.FirstOrDefault(r => r.Id == id);

                if (motorcycle == null)
                {
                    serviceResponse.Dados = null;
                    serviceResponse.Mensagem = "Nenhum entregador encontrado";
                    serviceResponse.Sucesso = false;
                }

                motorcycle.Plate = plate;

                _context.Motorcycles.Update(motorcycle);
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
