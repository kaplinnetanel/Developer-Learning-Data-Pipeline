using Confluent.Kafka;
using Consumer.Models;
using MongoDB.Driver;
using System.Text.Json;

var config = new ConsumerConfig
{
    BootstrapServers = "localhost:9092",
    GroupId = "csharp-consumer",
    AutoOffsetReset = AutoOffsetReset.Earliest
};

var mongoClient = new MongoClient("mongodb://localhost:27017");
var database = mongoClient.GetDatabase("DeveloperLearningDb");
var collection = database.GetCollection<DeveloperLearning>("Developers");

using var consumer = new ConsumerBuilder<Ignore, string>(config).Build();

consumer.Subscribe("processed-survey-topic");

try
{
    while (true)
    {
        var result = consumer.Consume();

        var developer = JsonSerializer.Deserialize<DeveloperLearning>(
            result.Message.Value
        );

        if (developer != null)
        {
            await collection.InsertOneAsync(developer);

            Console.WriteLine(
                $"Saved ResponseId: {developer.ResponseId}"
            );
        }
    }
}
catch (OperationCanceledException)
{
    consumer.Close();
}