using System.Linq;
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

        public async Task<PageResult<HistoryItem>> GetHistory(HistoryQuery historyQuery)
        {
            var pageResult = new PageResult<HistoryItem>();
            var container = _cosmosClient.GetContainer(DatabaseId, ContainerId);
            var countQuery = new QueryDefinition("SELECT value count(1) FROM c where c.user = @user")
                .WithParameter("@user", historyQuery.User);

            var countQueryResult = container.GetItemQueryIterator<long>(countQuery);
            while (countQueryResult.HasMoreResults)
            {
                pageResult.Total = (await countQueryResult.ReadNextAsync()).FirstOrDefault();
            }

            var query = new QueryDefinition($"SELECT * FROM c where c.user = @user ORDER BY c.date DESC OFFSET {historyQuery.Skip} LIMIT {historyQuery.Take}")
                .WithParameter("@user", historyQuery.User);

            var queryResult = container.GetItemQueryIterator<HistoryItem>(query);
            while (queryResult.HasMoreResults)
            {
                pageResult.Results.AddRange((await queryResult.ReadNextAsync()).Resource);
            }

            return pageResult;

        }
    }
}
