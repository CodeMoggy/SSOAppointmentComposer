//Copyright (c) CodeMoggy. All rights reserved. Licensed under the MIT license.
//See LICENSE in the project root for license information.

namespace ComposeAppointmentWeb.Models
{
    public class AppointmentProxyRequest
    {
        public string token { get; set; }
        public string organizerEmailAddress { get; set; }
        public string thirdPartyEventId { get; set; }
    }
}

