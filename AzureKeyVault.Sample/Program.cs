using Azure.Identity;
using AzureKeyVault.Sample;

var builder = WebApplication.CreateBuilder(args);

var vaultConfig = builder.Configuration.GetSection("KeyVault").Get<KeyVaultConfig>();

// read more https://github.com/Azure/azure-sdk-for-net/blob/Azure.Extensions.AspNetCore.Configuration.Secrets_1.2.2/sdk/extensions/Azure.Extensions.AspNetCore.Configuration.Secrets/README.md
builder.Configuration.AddAzureKeyVault(new Uri(vaultConfig.Endpoint), new ClientSecretCredential(vaultConfig.TenantId, vaultConfig.ClientId, vaultConfig.ClientSecret));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
