using System.Collections.Generic;
using System.Threading.Tasks;
using cs673history.Repository.Models;
using Microsoft.Azure.Cosmos;

namespace cs673history.Repository
{
    public class HistoryRepository : IHistoryRepository
    {
        private const string DatabaseId = "history";
        private const string ContainerId = "historyitem";
        private readonly CosmosClient _cosmosClient;

        public HistoryRepository(CosmosClient cosmosClient)
        {
            _cosmosClient = cosmosClient;
        }

        public async Task CreateDatabase()
        {
            var databaseResponse = await _cosmosClient.CreateDatabaseIfNotExistsAsync(DatabaseId);
            await databaseResponse.Database.CreateContainerIfNotExistsAsync(new ContainerProperties(ContainerId, "/user"));
        }

        public async Task SaveHistory(HistoryItem historyItem)
        {
            var container = _cosmosClient.GetContainer(DatabaseId, ContainerId);
            await container.CreateItemAsync(historyItem);
        }

        public async Task<IEnumerable<HistoryItem>> GetHistory(string user)
        {
            var historyItems = new List<HistoryItem>();
            var container = _cosmosClient.GetContainer(DatabaseId, ContainerId);
            var query = new QueryDefinition($"SELECT * FROM c where c.user = @user ORDER BY c.date DESC")
                .WithParameter("@user", user);

            var queryResult = container.GetItemQueryIterator<HistoryItem>(query);
            while (queryResult.HasMoreResults)
            {
                historyItems.AddRange((await queryResult.ReadNextAsync()).Resource);
            }
            return historyItems;
        }

        public async Task DeleteHistoryItem(string id, string user)
        {
            var container = _cosmosClient.GetContainer(DatabaseId, ContainerId);
            await container.DeleteItemAsync<HistoryItem>(id, new PartitionKey(user));
        }

    }
}
