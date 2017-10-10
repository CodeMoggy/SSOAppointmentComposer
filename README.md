# Appointment Composer

This code repository was created as a result of the following github repo https://github.com/OfficeDev/Outlook-Add-in-JavaScript-ValidateIdentityToken.

I decided to take that repo and simplify it so that the service and add-in were in the same project and therefore the same web app. 

Based on partner feedback I then extended the code to demonstrate how to use Azure Key Vault to store credentials based on the tenancy / domain of the user, thus enabling a user not to have to logon each time they use the add-in. 

<a name="codedescription"></a>
##Description of the code
This sample shows you how to create a .NET Framework service that validates an Exchange client access token. The Exchange server issues a token that is unique to the mailbox on the server. You can use this token to associate a mailbox with services that you provide to a mail add-in for Outlook.

The sample is divided into two parts:  
- A mail add-in for Outlook that runs in your email client. It requests an identity token from the Exchange server and sends this token to the web service.
- A web service (ServiceProxyController.cs) that validates the token from the client. The web service responds with the contents of the token, which the add-in then displays.

The web service uses the following steps to process the token:  
1. Decodes the identity token to get the URL for the Exchange server's authentication metadata document. During this step, the service also checks whether the token has expired and checks the version number on the token.  
2. If the identity token passes the first step, the service uses the information in the authentication metadata document to get the certificate that was used to sign the token from the server.  
3. If the token is valid, the service returns it to the mail add-in for Outlook for display.

The service does not use the token in any way. It responds with the information contained in the token, or with an error message if the token is not valid. 


  | File name | Description |
  |------|------|
  | AuthClaimTypes.cs | The static object that provides identifiers for the parts of the client identity token. |
  | AuthMetadata.cs |  The object that represents the authentication metadata document retrieved from the location specified in the client identity token. |
  | Base64UrlEncoder.cs |  The static object that decodes a URL that has been base-64 URL-encoded, as specified in RFC 4648. |
  | Config.cs |  Provides string values that must be matched in the client identity token. Also provides a certificate validation callback suitable for test use. |
  | DecodedJSONToken.cs |  Represents a valid JSON Web Token (JWT) decoded from the base-64 URL-encoded client identity token. If the token is not valid, the constructor for the **DecodedJSONToken** object will throw an **ApplicationException** error. | 
  | IdentityToken.cs |  The object that represents the decoded and validated client identity token. | 
  | IdentityTokenRequest.cs |  The object that represents the REST request from the add-in. | 
  | IdentityTokenResponse.cs |  The object that represents the REST response from the web service. | 
  | JsonAuthMetadataDocument.cs |  The object that represents the authentication metadata document sent from the Exchange server. |
  | JsonTokenDecoder.cs |  The static object that decodes the base-64 URL-encoded client identity token from the mail add-in for Outlook. |



