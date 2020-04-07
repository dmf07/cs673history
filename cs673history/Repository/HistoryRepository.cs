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
    }
}
