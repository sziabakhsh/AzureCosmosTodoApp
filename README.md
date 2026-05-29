# Azure Cosmos DB Todo MVC App

A modern ASP.NET Core 8 MVC web application built with Azure Cosmos DB for scalable NoSQL data storage and CRUD operations.

This project demonstrates backend-focused development practices using clean architecture principles, repository pattern implementation, and cloud-ready application design with Microsoft Azure services.

## 🚀 Features

* Create, Read, Update, and Delete Todo items
* Azure Cosmos DB integration
* ASP.NET Core 8 MVC architecture
* Repository Pattern implementation
* Clean and maintainable code structure
* Cloud-ready deployment support
* Responsive and lightweight UI

## 🛠 Technologies Used

* ASP.NET Core 8
* C#
* Azure Cosmos DB
* MVC Pattern
* Entity Framework Core
* Azure App Service

## 📦 Prerequisites

* .NET 8 SDK
* Azure account with Cosmos DB enabled
* Visual Studio 2022 / VS Code

## ⚡ Setup & Run

### 1️⃣ Clone the repository

```bash
git clone https://github.com/sziabakhsh/AzureCosmosTodoApp.git
cd AzureCosmosTodoApp
```

### 2️⃣ Configure Azure Cosmos DB

* Database: `TodoDB`
* Container: `Todos`
* Partition Key: `/id`


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
