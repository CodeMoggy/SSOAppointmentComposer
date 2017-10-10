//Copyright (c) CodeMoggy. All rights reserved. Licensed under the MIT license.
//See LICENSE in the project root for license information.

using ComposeAppointmentWeb.Models;
using ComposeAppointmentWeb.Utils;
using System;
using System.Web.Http;

namespace ComposeAppointmentWeb.Controllers
{
    public class ServiceProxyController : ApiController
    {
        public async System.Threading.Tasks.Task<AppointmentProxyResponse> PostServiceRequest(AppointmentProxyRequest proxyRequest)
        {
            AppointmentProxyResponse proxyResponse = new AppointmentProxyResponse();

            try
            {
                IdentityToken identityToken = null;
                var organizerDomain = string.Empty;
                var thirdPartyEventId = (string.IsNullOrEmpty(proxyRequest.thirdPartyEventId)) ? string.Empty : proxyRequest.thirdPartyEventId;
                var thirdPartySecretKey = string.Empty;

                using (DecodedJsonToken decodedToken = JsonTokenDecoder.Decode(proxyRequest))
                {
                    if (decodedToken.IsValid)
                    {
                        identityToken = new IdentityToken(proxyRequest, decodedToken.Audience, decodedToken.AuthMetadataUri);

                        // todo - add whatever logic you need to do here
                        if (!string.IsNullOrEmpty(proxyRequest.organizerEmailAddress))
                            organizerDomain = proxyRequest.organizerEmailAddress.Split('@')[1];

                        if (string.IsNullOrEmpty(thirdPartyEventId))
                            thirdPartyEventId = $"KF{Guid.NewGuid().ToString()}";

                        var secretKey = organizerDomain.Replace('.', '-');

                        thirdPartySecretKey = await KeyVaultClientHelper.GetDomainSecret(secretKey);
                    }
                }

                proxyResponse.token = identityToken;
                proxyResponse.organizerTenant = organizerDomain;
                proxyResponse.thirdPartyEventId = thirdPartyEventId;
                proxyResponse.thirdPartySecretKey = thirdPartySecretKey; 
            }
            catch (Exception ex)
            {
                proxyResponse.errorMessage = ex.Message;
            }

            return proxyResponse;
        }
    }
}
