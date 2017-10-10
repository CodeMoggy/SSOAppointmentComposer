﻿//Copyright (c) CodeMoggy. All rights reserved. Licensed under the MIT license.
//See LICENSE in the project root for license information.

using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;

namespace ComposeAppointmentWeb.Models
{
    public static class JsonTokenDecoder
    {
        public static DecodedJsonToken Decode(AppointmentProxyRequest rawToken)
        {
            string[] tokenParts = rawToken.token.Split('.');

            if (tokenParts.Length != 3)
            {
                throw new ApplicationException("Token must have three parts separated by '.' characters.");
            }

            string encodedHeader = tokenParts[0];
            string encodedPayload = tokenParts[1];
            string signature = tokenParts[2];

            string decodedHeader = Base64UrlEncoder.Decode(encodedHeader);
            string decodedPayload = Base64UrlEncoder.Decode(encodedPayload);

            JavaScriptSerializer serializer = new JavaScriptSerializer();

            Dictionary<string, string> header = serializer.Deserialize<Dictionary<string, string>>(decodedHeader);
            Dictionary<string, string> payload = serializer.Deserialize<Dictionary<string, string>>(decodedPayload);

            return new DecodedJsonToken(header, payload, signature);
        }
    }
}

