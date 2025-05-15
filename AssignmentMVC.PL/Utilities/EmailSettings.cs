using System.Net;
using System.Net.Mail;

namespace AssignmentMVC.PL.Utilities
{
    public static class EmailSettings
    {
        public static void SendEmail(Email email)
        {
            var client = new SmtpClient("smtp.gmail.com", 587);
            client.EnableSsl = true;
            client.Credentials = new NetworkCredential("omaresamaboamar@gmail.com", "eqhs lotw gpas ftli");
            client.Send("omaresamaboamar@gmail.com", email.To, email.Subject, email.Body);
        }
    }
}
