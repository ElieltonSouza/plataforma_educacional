using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace plataforma_educacional.Services
{
    public class EmailService
    {
        public async Task SendValidationCodeAsync(string email, string code)
        {
            var smtpClient = new SmtpClient("smtp.example.com")
            {
                Port = 587,
                Credentials = new NetworkCredential("username", "password"),
                EnableSsl = true,
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress("no-reply@plataformaeducacional.com"),
                Subject = "Código de Validação",
                Body = $"Seu código de validação é: {code}",
                IsBodyHtml = false,
            };

            mailMessage.To.Add(email);

            await smtpClient.SendMailAsync(mailMessage);
        }
    }
}