using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BikeRent.Enums;

namespace BikeRent.Models
{
    public class Rent
    {
        public Biker Biker { get; set; }

        [ForeignKey("BikerId")]
        public int BikerId { get; set; }

        public int DailyValue { get; set; }
        [Required]
        public DateTime PreviewEndDate { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }

        [Key]
        public int Id { get; set; }
        [ForeignKey("MotorcycleId")]
        public Motorcycle Motorcycle { get; set; }

        public int MotorcycleId { get; set; }
        public PlansEnum Plan { get; set; }
    }
}
