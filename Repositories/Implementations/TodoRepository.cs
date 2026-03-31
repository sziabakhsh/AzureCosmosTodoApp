using Microsoft.Azure.Cosmos;
using AzureCosmosTodoApp.Models;
using AzureCosmosTodoApp.Repositories.Interfaces;

namespace AzureCosmosTodoApp.Repositories.Implementations
{
    public class TodoRepository : ITodoRepository
    {
        private readonly Container _container;

        public TodoRepository(CosmosClient client, IConfiguration config)
        {
            var databaseName = config["CosmosDb:DatabaseName"];
            var containerName = config["CosmosDb:ContainerName"];

            _container = client.GetContainer(databaseName, containerName);
        }

        public async Task<List<TodoItem>> GetAllAsync()
        {
            var query = "SELECT * FROM c";
            var iterator = _container.GetItemQueryIterator<TodoItem>(query);

            var results = new List<TodoItem>();

            while (iterator.HasMoreResults)
            {
                var response = await iterator.ReadNextAsync();
                results.AddRange(response);
            }

            return results;
        }

        public async Task<TodoItem?> GetByIdAsync(string id)
        {
            try
            {
                var response = await _container.ReadItemAsync<TodoItem>(id, new PartitionKey(id));
                return response.Resource;
            }
            catch (CosmosException)
            {
                return null;
            }
        }

        public async Task AddAsync(TodoItem item)
        {
            await _container.CreateItemAsync(item, new PartitionKey(item.Id));
        }

        public async Task UpdateAsync(TodoItem item)
        {
            await _container.UpsertItemAsync(item, new PartitionKey(item.Id));
        }

        public async Task DeleteAsync(string id)
        {
            await _container.DeleteItemAsync<TodoItem>(id, new PartitionKey(id));
        }
    }
}