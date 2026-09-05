using Consumer.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consumer.Services;

public class DataService
{
    private readonly IMongoCollection<DeveloperLearning> _collection;

   public DataService()
{
    var connectionString =
        Environment.GetEnvironmentVariable("MONGO_CONNECTION_STRING")
        ?? "mongodb://localhost:27017";

    var databaseName =
        Environment.GetEnvironmentVariable("MONGO_DATABASE")
        ?? "DeveloperLearningDb";

    var collectionName =
        Environment.GetEnvironmentVariable("MONGO_COLLECTION")
        ?? "Developers";

    var client = new MongoClient(connectionString);

    var database = client.GetDatabase(databaseName);

    _collection =
        database.GetCollection<DeveloperLearning>(collectionName);
}

    public async Task CreateAsync(DeveloperLearning data)
    {
        await _collection.InsertOneAsync(data);
    }
}