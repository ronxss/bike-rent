using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BikeRent.Enums;

namespace BikeRent.Models
{
    public class Rent
    {
        [Key]
        public int Id { get; set; }

        public int DailyValue { get; set; }

        [ForeignKey("BikerId")]
        public int BikerId { get; set; }

        public Biker Biker { get; set;   }
        public int MotorcycleId { get; set; }

        [ForeignKey("MotorcycleId")]
        public Motorcycle Motorcycle { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public DateTime PreviewEndDate { get; set; }

        public PlansEnum Plan { get; set; }

    }
}
