using EmailSenderInterfaces;

namespace EmailSenderImplementation2 {
    public class EmailSenderImplementation2 : IEmailSender {
        public bool SendEmail(string to, string body) {
            Console.WriteLine($"{nameof(EmailSenderImplementation2)} sending mail to {to} with body = {body}");
            return true;
        }
    }
}