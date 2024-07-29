using Microsoft.AspNetCore.Identity;

namespace plataforma_educacional.Models
{
    public class Usuario : IdentityUser
    {
        public string Nome { get; set; }
        public int InstituicaoId { get; set; }
        public Instituicao Instituicao { get; set; }
        public string Role { get; set; }
        public string CodigoValidacao { get; set; }
    }
}