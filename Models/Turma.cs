namespace plataforma_educacional.Models
{
    public class Turma : Entity
    {
        public string Nome { get; set; }
        public int CursoId { get; set; }
        public Curso Curso { get; set; }
    }
}