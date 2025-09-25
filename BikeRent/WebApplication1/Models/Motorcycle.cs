using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BikeRent.Models
{
    public class Motorcycle
    {
        [Key]
        public int Id { get; set; }

        public int Year { get; set; }
        public string Model { get; set; }

        [Required]
        
        public string Plate { get; set; }

        public Motorcycle(int year, string model, string plate)
        {
            if (string.IsNullOrWhiteSpace(plate))
                throw new ArgumentException("A placa não pode ser vazia.");

            Year = year;
            Model = model;
            Plate = plate;
        }
    }
}
