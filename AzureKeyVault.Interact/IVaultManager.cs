namespace AzureKeyVault.Interact
{
    public interface IVaultManager
    {
        Task<string> GetSecret(string secretName);
        Task<string> CreateSecret(string secretName, string secretValue);
        Task DeleteSecret(string secretName);
        Task<string> Encrypt(string value);
        Task<string> Decrypt(string cipherText);
    }
}
