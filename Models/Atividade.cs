namespace plataforma_educacional.Models
{
    public class Atividade : Entity
    {
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public DateTime DataEntrega { get; set; }
    }
}