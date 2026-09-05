using System.Text.Json;
using Confluent.Kafka;
using Consumer.Models;
using Consumer.Services;

var bootstrapServers =
    Environment.GetEnvironmentVariable("KAFKA_BOOTSTRAP_SERVERS")
    ?? "localhost:9092";

var topic =
    Environment.GetEnvironmentVariable("KAFKA_TOPIC")
    ?? "processed-survey-topic";

var groupId =
    Environment.GetEnvironmentVariable("KAFKA_GROUP_ID")
    ?? "csharp-consumer";

var consumerConfig = new ConsumerConfig
{
    BootstrapServers = bootstrapServers,
    GroupId = groupId,
    AutoOffsetReset = AutoOffsetReset.Earliest
};

using var consumer =
    new ConsumerBuilder<Ignore, string>(consumerConfig).Build();

consumer.Subscribe(topic);

var dataService = new DataService();

while (true)
{
    var result = consumer.Consume();

    var developer =
        JsonSerializer.Deserialize<DeveloperLearning>(
            result.Message.Value);

    if (developer != null)
    {
        await dataService.CreateAsync(developer);

        Console.WriteLine(
            $"Saved ResponseId: {developer.ResponseId}");
    }
}