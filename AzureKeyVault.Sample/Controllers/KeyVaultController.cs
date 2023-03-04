using Microsoft.AspNetCore.Mvc;

namespace AzureKeyVault.Sample.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class KeyVaultController : ControllerBase
    {
        private readonly ILogger<KeyVaultController> _logger;
        private readonly IConfiguration _configuration;

        public KeyVaultController(ILogger<KeyVaultController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        [HttpGet]
        public IActionResult GetSqlConnection()
        {
            var cs = _configuration.GetSection("ConnectionStrings:SqlConnectionString").Get<string>();

            return Ok(cs);
        }
    }
}