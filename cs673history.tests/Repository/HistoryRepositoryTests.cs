﻿using System;
using System.Threading.Tasks;
using cs673history.Repository;
using cs673history.Repository.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace cs673history.tests.Repository
{
    [TestClass]
    [TestCategory("Integration")]
    //[Ignore]
    public class HistoryRepositoryTests
    {
        private IHistoryRepository _historyRepository;
        private const string ConnectionString =
            "AccountEndpoint=https://localhost:8081/;AccountKey=C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw==";

        [TestInitialize]
        public void Init()
        {
            _historyRepository = new HistoryRepository(CosmosClientBuilder.BuildCosmosClient(ConnectionString));
        }

        [TestMethod]
        public async Task CreateDatabase()
        {
            //arrange
            //act
            await _historyRepository.CreateDatabase();

            //assert
            //check db
        }

        [TestMethod]
        public async Task SaveHistory()
        {
            //arrange
            var historyItem = new HistoryItem
            {
                Date = DateTimeOffset.Now,
                Title = "title",
                Upc = "upc",
                User = "user"
            };

            //act
            await _historyRepository.SaveHistory(historyItem);

            //assert
            //check db
        }

        [TestMethod]
        public async Task GetHistory()
        {
            //arrange
            var historyQuery = new HistoryQuery
            {
                User = "user",
                Take = 1
            };

            //act
            var result = await _historyRepository.GetHistory(historyQuery);
            //assert
            //check db
        }
    }
}
