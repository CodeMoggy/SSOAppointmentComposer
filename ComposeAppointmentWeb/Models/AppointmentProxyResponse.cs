
namespace ComposeAppointmentWeb.Models
{
    public class AppointmentProxyResponse
    {
        public string errorMessage { get; set; }
        public IdentityToken token { get; set; }
        public string organizerTenant { get; set; }
        public string thirdPartyEventId { get; set; }
        public string thirdPartySecretKey { get; internal set; }
    }
}