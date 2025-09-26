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
        public async Task<ServiceResponse<string>> UpdateRent(int id)
        {

            ServiceResponse<string> serviceResponse = new ServiceResponse<string>();

            try
            {
                Rent rent =  _context.Rents.FirstOrDefault(r => r.Id == id);
                var returnDate = DateTime.Today.Date;

                if (rent == null)
                {
                    serviceResponse.Dados = null;
                    serviceResponse.Mensagem = "Nenhuma locação encontrada";
                    serviceResponse.Sucesso = false;
                }

                decimal dailyPrice = rent.Plan switch
                {
                    PlansEnum.SeteDias => 30,
                    PlansEnum.QuinzeDias => 28,
                    PlansEnum.TrintaDias => 22,
                    PlansEnum.QuarentaCincoDias => 20,
                    PlansEnum.CinquentaDias => 18,
                    _ => throw new ArgumentOutOfRangeException()
                };

                int expectedDays = (int)rent.Plan;
                int effectiveDays = (rent.PreviewEndDate - rent.StartDate).Days;

                if (effectiveDays <= 0)
                {
                    serviceResponse.Dados = null;
                    serviceResponse.Mensagem = "Data de devolução inválida";
                    serviceResponse.Sucesso = false;
                }

                decimal baseValue = effectiveDays * dailyPrice;
                decimal finalValue = baseValue;
                

                if (returnDate < rent.PreviewEndDate)
                {
                    int unusedDays = expectedDays - effectiveDays;
                    decimal penalty = rent.Plan switch
                    {
                        PlansEnum.SeteDias => unusedDays * dailyPrice * 0.20m,
                        PlansEnum.QuinzeDias => unusedDays * dailyPrice * 0.40m,
                        _ => 0
                    };
                    finalValue += penalty;
                }

                if (returnDate > rent.PreviewEndDate)
                {
                    int extraDays = (returnDate - rent.PreviewEndDate).Days;
                    finalValue += extraDays * 50;
                    
                }

                rent.EndDate = returnDate;

                _context.Rents.Update(rent);
                await _context.SaveChangesAsync();
                serviceResponse.Dados = rent.EndDate.ToString();

            }
            catch (Exception ex)
            {
                serviceResponse.Mensagem = ex.Message;
                serviceResponse.Sucesso = false;
            }

            serviceResponse.Sucesso = true;

            return serviceResponse;
            
            
        }
    }
}
