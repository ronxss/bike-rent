namespace BikeRent.Models
{
    public class Motorcycle
    {
        public int Id { get; set; }

        public int Ano { get; set; }
        public string Modelo { get; set; }

        public string Placa { get; set; }

        public Motorcycle(int ano, string modelo, string placa)
        {
            if (string.IsNullOrWhiteSpace(placa))
                throw new ArgumentException("A placa não pode ser vazia.");

            Ano = ano;
            Modelo = modelo;
            Placa = placa.ToUpper();
        }
    }
}
