using BikeRent.Data;
using BikeRent.Events;
using BikeRent.Models;
using Microsoft.EntityFrameworkCore;

namespace BikeRent.Services.MotorcyclesService
{
    public class MotorcyclesService : IMotorcycleInterface
    {
        private readonly BikeRentDb _context;
        private readonly RegisteredMotorcycle _publisher;


        public MotorcyclesService(BikeRentDb context, RegisteredMotorcycle publisher)
        {
            _context = context;
            _publisher = publisher;
        }

        public async Task<ServiceResponse<List<Motorcycle>>> CreateMotorcycle(Motorcycle newMotorcycle)
        {
            ServiceResponse<List<Motorcycle>> serviceResponse = new ServiceResponse<List<Motorcycle>>();

            try
            {

                if (newMotorcycle == null)
                {
                    serviceResponse.Dados = null;
                    serviceResponse.Mensagem = "Informe os dados";
                    serviceResponse.Sucesso = false;

                }
                _context.Add(newMotorcycle);
                await _context.SaveChangesAsync();

                _publisher.Publish(newMotorcycle);
                serviceResponse.Dados = _context.Motorcycles.ToList();
            }
            catch (Exception ex)
            {
                serviceResponse.Mensagem = ex.Message;
                serviceResponse.Sucesso = false;
            }

            return serviceResponse;
        }
        public async Task<ServiceResponse<List<Motorcycle>>> DeleteMotorcycle(int id)
        {
            ServiceResponse<List<Motorcycle>> serviceResponse = new ServiceResponse<List<Motorcycle>>();

            Motorcycle motorcycle = await _context.Motorcycles.FirstOrDefaultAsync(m => m.Id == id);

            try
            {
                if (motorcycle == null)
                {
                    serviceResponse.Dados = null;
                    serviceResponse.Mensagem = "Nenhuma moto Encontrada";
                    serviceResponse.Sucesso = false;

                    return serviceResponse;
                }

                _context.Motorcycles.Remove(motorcycle);
                await _context.SaveChangesAsync();

                serviceResponse.Dados = _context.Motorcycles.ToList();

            }
            catch (Exception ex)
            {
                serviceResponse.Mensagem = ex.Message;
                serviceResponse.Sucesso = false;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<Motorcycle>>> GetMotorcycle()
        {
            ServiceResponse<List<Motorcycle>> serviceResponse = new ServiceResponse<List<Motorcycle>>();
            try
            {
                serviceResponse.Dados = _context.Motorcycles.ToList();
                if (serviceResponse.Dados.Count == 0)
                {
                    serviceResponse.Mensagem = "Nenhuma Tarefa Encontrada";
                }

            }
            catch (Exception ex)
            {
                serviceResponse.Mensagem = ex.Message;
                serviceResponse.Sucesso = false;
            }

            return serviceResponse;
        }
        public async Task<ServiceResponse<List<Motorcycle>>> GetMotorcycleById(int id)
        {
            ServiceResponse<List<Motorcycle>> serviceResponse = new ServiceResponse<List<Motorcycle>>();

            try
            {
                serviceResponse.Dados = await _context.Motorcycles.Where(m => m.Id == id).ToListAsync();
            }
            catch (Exception ex)
            {
                serviceResponse.Mensagem = ex.Message;
                serviceResponse.Sucesso = false;
            }

            return serviceResponse;
        }
        public async Task<ServiceResponse<Motorcycle>> UpdateMotorcyclePlate(int id, string plate)
        {

            ServiceResponse<Motorcycle> serviceResponse = new ServiceResponse<Motorcycle>();

            try
            {
                Motorcycle motorcycle = _context.Motorcycles.FirstOrDefault(m => m.Id == id);

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
