namespace plataforma_educacional.Models.ViewModels
{
    public class RegisterAlunoViewModel
    {
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public string ConfirmarSenha { get; set; }
        public string CodigoValidacao { get; set; }
    }
}