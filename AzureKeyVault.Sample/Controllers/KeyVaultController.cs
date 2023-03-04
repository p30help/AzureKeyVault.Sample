using AzureKeyVault.Interact;
using Microsoft.AspNetCore.Mvc;

namespace AzureKeyVault.Sample.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class KeyVaultController : ControllerBase
    {
        private readonly ILogger<KeyVaultController> _logger;
        private readonly IConfiguration _configuration;
        private readonly IVaultManager _vaultManager;

        public KeyVaultController(ILogger<KeyVaultController> logger, IConfiguration configuration, IVaultManager vaultManager)
        {
            _logger = logger;
            _configuration = configuration;
            _vaultManager = vaultManager;
        }

        [HttpPost]
        public async Task<IActionResult> SetSecret([FromBody] string key, [FromBody] string vault)
        {
            await _vaultManager.CreateSecret(key, vault);

            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetSecret(string key)
        {
            var result = await _vaultManager.GetSecret(key);

            return Ok(result);
        }

        [HttpGet]
        public IActionResult GetSqlConnection()
        {
            var cs = _configuration.GetSection("ConnectionStrings:SqlConnectionString").Get<string>();

            return Ok(cs);
        }
    }
}