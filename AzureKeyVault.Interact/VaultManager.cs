using Azure.Identity;
using Azure.Security.KeyVault.Keys;
using Azure.Security.KeyVault.Keys.Cryptography;
using Azure.Security.KeyVault.Secrets;
using System.Text;

namespace AzureKeyVault.Interact
{
    public class VaultManager : IVaultManager
    {
        readonly string _endpoint;
        readonly SecretClient _secretClient;
        readonly string _keyName = "KeyDemo";
        readonly KeyClient _keyClient;

        public VaultManager(KeyVaultConfig config)
        {
            _endpoint = config.Endpoint;

            var secretOptions = new SecretClientOptions()
            {
                Retry =
                {
                    Delay = TimeSpan.FromSeconds(2),
                    MaxDelay= TimeSpan.FromSeconds(16),
                    MaxRetries = 5,
                    Mode = Azure.Core.RetryMode.Exponential
                }
            };
            _secretClient = new SecretClient(new Uri(_endpoint), GetCredential(), secretOptions);

            var keyOptions = new KeyClientOptions()
            {
                Retry =
                {
                    Delay = TimeSpan.FromSeconds(2),
                    MaxDelay= TimeSpan.FromSeconds(16),
                    MaxRetries = 5,
                    Mode = Azure.Core.RetryMode.Exponential
                }
            };
            _keyClient = new KeyClient(new Uri(_endpoint), GetCredential(), keyOptions);
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

        public async Task<string> Encrypt(string value)
        {
            var byteData = Encoding.Unicode.GetBytes(value);
            var encrypted = await _keyClient.GetCryptographyClient(_keyName).EncryptAsync(EncryptionAlgorithm.RsaOaep, byteData);
            var encryptedText = Convert.ToBase64String(encrypted.Ciphertext);

            return encryptedText;
        }

        public async Task<string> Decrypt(string cipherText)
        {
            var byteData = Convert.FromBase64String(cipherText);
            var decrypted = await _keyClient.GetCryptographyClient(_keyName).DecryptAsync(EncryptionAlgorithm.RsaOaep, byteData);
            var decryptedText = Encoding.Unicode.GetString(decrypted.Plaintext);

            return decryptedText;
        }
    }
}
