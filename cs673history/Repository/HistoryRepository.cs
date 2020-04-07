using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.Azure.Cosmos;

namespace cs673history.Repository
{
    public class HistoryRepository : IHistoryRepository
    {
        private readonly HttpStatusCode[] _httpStatusCodes =
        {
            HttpStatusCode.Created, HttpStatusCode.OK
        };

        private readonly CosmosClient _cosmosClient;

        public HistoryRepository(CosmosClient cosmosClient)
        {
            _cosmosClient = cosmosClient;
        }

        public async Task CreateDatabase()
        {
            var databaseResponse = await _cosmosClient.CreateDatabaseIfNotExistsAsync("history");
            if (!_httpStatusCodes.Contains(databaseResponse.StatusCode))
            {
                throw new Exception("Create Database Issue");
            }

            var containerResponse = await databaseResponse.Database.CreateContainerIfNotExistsAsync(new ContainerProperties("historyitems", "/user"));
            if (!_httpStatusCodes.Contains(containerResponse.StatusCode))
            {
                throw new Exception("Create Container Issue");
            }

        }
    }
}
