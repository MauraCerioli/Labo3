using EmailSenderInterfaces;

namespace EmailSenderImplementation1 {
    public class EmailSenderImplementation1: IEmailSender {
        public bool SendEmail(string to, string body) {
            Console.WriteLine($"{nameof(EmailSenderImplementation1)} sending mail to {to} with body = {body}");
            return true;
        }
    }
}
