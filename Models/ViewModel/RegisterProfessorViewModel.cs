namespace plataforma_educacional.Models.ViewModels
{
    public class RegisterProfessorViewModel
    {
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public int InstituicaoId { get; set; }
        public string CodigoValidacao { get; set; } // Adicione esta linha
    }
}