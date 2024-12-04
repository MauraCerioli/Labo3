using EmailSenderInterfaces;
using Microsoft.Extensions.DependencyInjection;

namespace AggregationRoot {
    internal class Program {
        static void Main(string[] args) {
            Console.WriteLine("Hello, World!");
            using var serviceProvider = new ServiceCollection()
                .AddSingleton<IEmailSender, EmailSenderImplementation1.EmailSenderImplementation1>()
                .BuildServiceProvider();
            var mailSender = serviceProvider.GetService<IEmailSender>()!;
            mailSender.SendEmail("TAP", "abbiamo quasi finito le lezioni!!!");


            //versione alternativa di using
            using (var serviceProvider1 = new ServiceCollection()
                       .AddSingleton<IEmailSender, EmailSenderImplementation1.EmailSenderImplementation1>()
                       .BuildServiceProvider()) {
                var mailSender1 = serviceProvider1.GetService<IEmailSender>()!;
                mailSender1.SendEmail("TAP", "abbiamo quasi finito le lezioni!!!");
            }//qui viene chiamata mailSender1.Dispose
        }//qui viene chiamata mailSender.Dispose
    }
}
