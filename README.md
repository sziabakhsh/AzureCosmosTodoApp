Azure Cosmos DB Todo MVC App

A lightweight ASP.NET Core 8 MVC Todo app using Azure Cosmos DB (NoSQL) for CRUD operations. Perfect for learning, free-tier friendly, and ready for Azure deployment.

🚀 Features
✅ Create, Read, Update, Delete Todo items
✅ Repository Pattern for clean architecture
✅ MVC pattern (Controllers, Views, Models)
✅ Cosmos DB integration with secure keys
✅ Easy to deploy to Azure App Service
🛠 Prerequisites
.NET 8 SDK
Azure account for Cosmos DB
Visual Studio 2022, VS Code, or any C# IDE
⚡ Setup & Run
1️⃣ Clone the repo
git clone https://github.com/sziabakhsh/AzureCosmosTodoApp.git
cd AzureCosmosTodoApp
2️⃣ Configure Azure Cosmos DB
Database: TodoDB
Container: Todos
Partition Key: /id

Make sure names match appsettings.json.

3️⃣ Set Cosmos DB keys
dotnet user-secrets set "CosmosDb:Account" "https://your-account.documents.azure.com:443/"
dotnet user-secrets set "CosmosDb:Key" "YOUR_PRIMARY_KEY"
dotnet user-secrets set "CosmosDb:DatabaseName" "TodoDB"
dotnet user-secrets set "CosmosDb:ContainerName" "Todos"
4️⃣ Run
dotnet build
dotnet run
Visit: http://localhost:5122/Todo
🗂 Architecture
ITodoRepository → Interface for CRUD
TodoRepository → Cosmos DB implementation
Controllers only interact with Repository → Clean separation
⚠ Notes
Ensure Todo items exist in Cosmos DB to avoid 404 on Edit/Delete
Partition Key must be /id
Key must be Base64 string from Azure (no extra ;)
🌟 Next Steps
Deploy to Azure App Service (free-tier)
Enable HTTPS & environment-based config
Add authentication for multi-user Todo lists
📄 License

MIT License – free to use, modify, and share.