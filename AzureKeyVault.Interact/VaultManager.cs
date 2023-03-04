using Azure.Identity;
using Azure.Security.KeyVault.Secrets;

namespace AzureKeyVault.Interact
{
    public class VaultManager : IVaultManager
    {
        readonly string _endpoint;
        readonly SecretClient _secretClient;

        public VaultManager(KeyVaultConfig config)
        {
            _endpoint = config.Endpoint;

            _secretClient = new SecretClient(new Uri(_endpoint), GetCredential());
        }

        private DefaultAzureCredential GetCredential()
        {
            return new DefaultAzureCredential();
        }

        public async Task<string> CreateSecret(string secretName, string secretValue)
        {
            if (secretName == null) throw new ArgumentNullException(nameof(secretName));

            var result = await _secretClient.SetSecretAsync(secretName, secretValue);

            return result.Value.Value;
        }

        public async Task DeleteSecret(string secretName)
        {
            if (secretName == null) throw new ArgumentNullException(nameof(secretName));

            await _secretClient.StartDeleteSecretAsync(secretName);
        }

        public async Task<string> GetSecret(string secretName)
        {
            if (secretName == null) throw new ArgumentNullException(nameof(secretName));

            try
            {
                var value = await _secretClient.GetSecretAsync(secretName);

                return value.Value.Value;
            }
            catch (Exception)
            {
                return string.Empty;
            }

        }
    }
}
