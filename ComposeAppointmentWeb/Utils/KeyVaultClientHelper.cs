using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Azure.Services.AppAuthentication;
using Microsoft.Azure.KeyVault;
using System.Web.Configuration;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System.Threading.Tasks;

namespace ComposeAppointmentWeb.Utils
{
    public class KeyVaultClientHelper
    {
        /// <summary>
        /// secretKey is the key used to lookup the value in the Azure Key Vault
        /// uses Azure Managed Service Identity to authenticate with Azure Key Vault
        /// https://docs.microsoft.com/en-us/azure/app-service/app-service-managed-service-identity
        /// Note: once you have registered this app with Azure Active Directory you should stop and start your app
        /// to ensure the MSI_Endpoint and MSI_Secret are initialized properly: https://stackoverflow.com/questions/46307365/azure-msi-on-app-service
        /// </summary>
        /// <param name="secretKey"></param>
        /// <returns>secret value</returns>
        public static async Task<string> GetDomainSecret(string secretKey)
        {
            var tokenProvider = new AzureServiceTokenProvider();
            var client = new KeyVaultClient(new KeyVaultClient.AuthenticationCallback(tokenProvider.KeyVaultTokenCallback));
            var vaultUrl = WebConfigurationManager.AppSettings["KEY_VAULT_URL"];
            var secret = await client.GetSecretAsync(vaultUrl, secretKey);

            return secret.Value;
        }
    }
}