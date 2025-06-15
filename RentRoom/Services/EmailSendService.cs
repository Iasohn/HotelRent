using System.Net;
using System.Net.Mail;

namespace RentRoom.Services
{
    public class EmailSendService : IEmailSendService
    {

        private readonly IConfiguration _configuration;
        public EmailSendService(IConfiguration configuration)
        {
            _configuration = configuration;
        }



        public async Task sendAsync(string Email, string code)
        {
            var userName = _configuration.GetValue<string>("EmailSettings:Username");
            var password = _configuration.GetValue<string>("EmailSettings:Password");
            var SmtpServer = _configuration.GetValue<string>("EmailSettings:SmtpServer");
            var Port = _configuration.GetValue<int>("EmailSettings:Port");
            var EmailSender = _configuration.GetValue<string>("EmailSettings:From");

            var smtpClient = new SmtpClient(SmtpServer, Port);

            smtpClient.EnableSsl = true;
            smtpClient.UseDefaultCredentials = false;

            smtpClient.Credentials = new NetworkCredential(userName, password);

            var message = new MailMessage(userName, Email, "Confirmation", code);
            await smtpClient.SendMailAsync(message);
        }


    }
}
