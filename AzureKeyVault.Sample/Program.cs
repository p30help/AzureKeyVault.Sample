using Azure.Identity;
using AzureKeyVault.Interact;

var builder = WebApplication.CreateBuilder(args);

var vaultConfig = builder.Configuration.GetSection("KeyVault").Get<KeyVaultConfig>();

// read more https://github.com/Azure/azure-sdk-for-net/blob/Azure.Extensions.AspNetCore.Configuration.Secrets_1.2.2/sdk/extensions/Azure.Extensions.AspNetCore.Configuration.Secrets/README.md
builder.Configuration.AddAzureKeyVault(new Uri(vaultConfig.Endpoint), new DefaultAzureCredential());

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<KeyVaultConfig>(vaultConfig);
builder.Services.AddSingleton<IVaultManager, VaultManager>();

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
