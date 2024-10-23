namespace calculadora.Models
{
    public class Calculo
    {
        public int Id { get; set; }
        public string Operacion { get; set; }
        public string Resultado { get; set; }
        public DateTime FechaHora { get; set; }
    }
}
