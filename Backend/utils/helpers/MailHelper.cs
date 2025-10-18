using MailKit.Net.Smtp;
using MimeKit;

namespace Backend.Utils.Helpers
{
    public class MailHelper
    {
        private static string GetTemplatePath(Template template)
        {
            return template switch
            {
                Template.SEND_OTP => Path.Combine(Directory.GetCurrentDirectory(), "templates/mails", "sendOtpTemplate.html"),
                _ => throw new ExceptionCustom("Template not found"),
            };
        }

        private static string LoadTemplate(Template path, Dictionary<string, string> replacements)
        {
            string templatePath = GetTemplatePath(path);
            var html = File.ReadAllText(templatePath);

            foreach (var kv in replacements)
            {
                html = html.Replace($"{{{{{kv.Key}}}}}", kv.Value);
            }

            return html;
        }

        public enum Template
        {
            SEND_OTP
        }

        public static async Task SendMail(string to, string subject, Template templatePath, Dictionary<string, string> data)
        {
            try
            {
                var body = LoadTemplate(templatePath, data);

                var message = new MimeMessage();
                message.From.Add(new MailboxAddress("Spot247", Variable.Enviroments.SMTP_FROM));
                message.To.Add(new MailboxAddress(to, to));
                message.Subject = subject;

                message.Body = new TextPart("html")
                {
                    Text = body
                };

                using var client = new SmtpClient();
                await client.ConnectAsync(Variable.Enviroments.SMTP_HOST, int.Parse(Variable.Enviroments.SMTP_PORT), MailKit.Security.SecureSocketOptions.StartTls);
                await client.AuthenticateAsync(Variable.Enviroments.SMTP_USER, Variable.Enviroments.SMTP_PASS);
                await client.SendAsync(message);
                await client.DisconnectAsync(true);
            }
            catch (Exception ex)
            {
                throw new ExceptionCustom(ex.Message);
            }
        }
    }
}