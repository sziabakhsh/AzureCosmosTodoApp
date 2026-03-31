using Azure.Identity;
using Microsoft.Azure.Cosmos;
using AzureCosmosTodoApp.Repositories.Interfaces;
using AzureCosmosTodoApp.Repositories.Implementations;

var builder = WebApplication.CreateBuilder(args);

// --------------------------------------------------
// 1. Configuration (Key Vault for Production)
// --------------------------------------------------
if (builder.Environment.IsProduction())
{
    var keyVaultName = builder.Configuration["KeyVault:Name"];

    if (!string.IsNullOrEmpty(keyVaultName))
    {
        var keyVaultUri = new Uri($"https://{keyVaultName}.vault.azure.net/");

        builder.Configuration.AddAzureKeyVault(
            keyVaultUri,
            new DefaultAzureCredential()
        );
    }
}

// --------------------------------------------------
// 2. Bind Cosmos DB Settings
// --------------------------------------------------
var cosmosSection = builder.Configuration.GetSection("CosmosDb");

string? account = cosmosSection["Account"];
string? key = cosmosSection["Key"];
string? databaseName = cosmosSection["DatabaseName"];
string? containerName = cosmosSection["ContainerName"];

// Console.WriteLine($"Account: {account}");
// Console.WriteLine($"Key: {key}");

// Basic validation (fail fast)
if (string.IsNullOrEmpty(account))
    throw new Exception("CosmosDb:Account is not configured.");

if (string.IsNullOrEmpty(key))
    throw new Exception("CosmosDb:Key is not configured.");

if (string.IsNullOrEmpty(databaseName))
    throw new Exception("CosmosDb:DatabaseName is not configured.");

if (string.IsNullOrEmpty(containerName))
    throw new Exception("CosmosDb:ContainerName is not configured.");

// --------------------------------------------------
// 3. Register Cosmos Client (Singleton)
// --------------------------------------------------
builder.Services.AddSingleton(s =>
{
    return new CosmosClient(account, key, new CosmosClientOptions
    {
        ConnectionMode = ConnectionMode.Gateway,
        SerializerOptions = new CosmosSerializationOptions
        {
            PropertyNamingPolicy = CosmosPropertyNamingPolicy.CamelCase
        }
    });
});

// --------------------------------------------------
// 4. Register Services
// --------------------------------------------------
builder.Services.AddScoped<ITodoRepository, TodoRepository>();

// --------------------------------------------------
// 5. MVC Services
// --------------------------------------------------
builder.Services.AddControllersWithViews();

// --------------------------------------------------
// 6. Build App
// --------------------------------------------------
var app = builder.Build();

// --------------------------------------------------
// 7. Middleware Pipeline
// --------------------------------------------------
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

// --------------------------------------------------
// 8. Routing
// --------------------------------------------------
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Todo}/{action=Index}/{id?}"
);

// --------------------------------------------------
app.Run();



// using Microsoft.Azure.Cosmos;

// var builder = WebApplication.CreateBuilder(args);

// builder.Configuration.AddUserSecrets<Program>();
// string connectionString = builder.Configuration["CosmosDb:ConnectionString"];
// // =============================================
// // Services Registration
// // =============================================

// // Cosmos DB Client (using Connection String - Recommended)
// builder.Services.AddSingleton<CosmosClient>(sp =>
// {
//     var configuration = sp.GetRequiredService<IConfiguration>();
    
//     var connectionString = configuration["CosmosDb:ConnectionString"];

//     if (string.IsNullOrWhiteSpace(connectionString))
//     {
//         throw new InvalidOperationException(
//             "CosmosDb:ConnectionString is missing or empty in appsettings.json. " +
//             "Please add your full Azure Cosmos DB Primary Connection String.");
//     }

//     return new CosmosClient(connectionString);
// });

// // CosmosDbService
// builder.Services.AddSingleton<ICosmosDbService>(sp =>
// {
//     var cosmosClient = sp.GetRequiredService<CosmosClient>();
//     var configuration = sp.GetRequiredService<IConfiguration>();

//     var databaseName = configuration["CosmosDb:DatabaseName"] ?? "TodoDb";
//     var containerName = configuration["CosmosDb:ContainerName"] ?? "Items";

//     return new CosmosDbService(cosmosClient, databaseName, containerName);
// });

// // Add support for Controllers (if you're using them) + Minimal APIs
// builder.Services.AddControllers();

// // =============================================
// // Build the app
// // =============================================
// var app = builder.Build();

// // =============================================
// // Middleware Configuration
// // =============================================
// if (!app.Environment.IsDevelopment())
// {
//     app.UseExceptionHandler("/Home/Error");
//     app.UseHsts();
// }

// app.UseHttpsRedirection();
// app.UseStaticFiles();
// app.UseRouting();
// app.UseAuthorization();

// // =============================================
// // Endpoints
// // =============================================

// // 1. MVC Controller Route (if using Controllers)
// app.MapControllerRoute(
//     name: "default",
//     pattern: "{controller=Home}/{action=Index}/{id?}");

// // 2. Minimal API Endpoints for Todo (Recommended for this app)
// app.MapGet("/todos", async (ICosmosDbService db) => 
//     await db.GetAllAsync());

// app.MapGet("/todos/{id}", async (ICosmosDbService db, string id) =>
// {
//     var item = await db.GetAsync(id);
//     return item is not null ? Results.Ok(item) : Results.NotFound();
// });

// app.MapPost("/todos", async (ICosmosDbService db, TodoItem item) =>
// {
//     if (string.IsNullOrEmpty(item.Id))
//         item.Id = Guid.NewGuid().ToString();

//     await db.AddAsync(item);
//     return Results.Created($"/todos/{item.Id}", item);
// });

// app.MapPut("/todos/{id}", async (ICosmosDbService db, string id, TodoItem item) =>
// {
//     if (id != item.Id)
//         return Results.BadRequest("ID mismatch");

//     await db.UpdateAsync(item);
//     return Results.NoContent();
// });

// app.MapDelete("/todos/{id}", async (ICosmosDbService db, string id) =>
// {
//     await db.DeleteAsync(id);
//     return Results.NoContent();
// });

// app.Run();