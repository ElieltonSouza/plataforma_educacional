namespace plataforma_educacional.Models
{
    public class Curso : Entity
    {
        public string Nome { get; set; }
        public int InstituicaoId { get; set; }
        public Instituicao Instituicao { get; set; }
    }
}