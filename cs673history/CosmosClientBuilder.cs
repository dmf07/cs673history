using Microsoft.Azure.Cosmos;

namespace cs673history
{
    public static class CosmosClientBuilder
    {
        public static CosmosClient BuildCosmosClient(string connectionString)
        {
            var options = new CosmosClientOptions
            {
                SerializerOptions = new CosmosSerializationOptions()
                {
                    PropertyNamingPolicy = CosmosPropertyNamingPolicy.CamelCase
                }
            };
            return new CosmosClient(connectionString, options);
        }
    }
}
