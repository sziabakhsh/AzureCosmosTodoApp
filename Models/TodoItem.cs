namespace AzureCosmosTodoApp.Models
{
    public class TodoItem
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public required string Title { get; set; }

        public bool IsCompleted { get; set; }

        public string PartitionKey { get; set; } = "todos";  // Fixed partition for simple demo (use userId for multi-tenant)
    }
}