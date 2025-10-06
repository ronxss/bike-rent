using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BikeRent.Models
{
    public class Biker
    {
        public Biker(string name, string cnpj, DateTime birthDate, string cnh, string cnhType, byte[]? cnhImage = null)
        {
            Name = name;
            Cnpj = cnpj;
            BirthDate = birthDate;
            Cnh = cnh;
            CnhType = cnhType;
            CnhImage = cnhImage;
        }

        public DateTime BirthDate { get; set; }

        [MaxLength(20)]
        public string Cnh { get; set; }

        public byte[] CnhImage { get; set; }

        [MaxLength(3)]
        public string CnhType { get; set; }

        [MaxLength(14)]
        public string Cnpj { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public bool PossuiCategoriaA()
        {
            return CnhType == "A" || CnhType == "A+B";
        }
    }
}
