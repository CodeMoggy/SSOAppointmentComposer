namespace ComposeAppointmentWeb.Models
{
    public class AppointmentProxyRequest
    {
        public string token { get; set; }
        public string organizerEmailAddress { get; set; }
        public string thirdPartyEventId { get; set; }
    }
}

