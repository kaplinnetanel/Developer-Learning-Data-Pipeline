using MongoDB.Driver;

namespace Api.Services;

public class DeveloperService
{
    private readonly IMongoCollection<DeveloperLearning> _collection;

    public DeveloperService(IConfiguration configuration)
    {
        var connectionString =
            configuration["MongoDb:ConnectionString"];

        var databaseName =
            configuration["MongoDb:DatabaseName"];

        var collectionName =
            configuration["MongoDb:CollectionName"];

        var client = new MongoClient(connectionString);

        var database = client.GetDatabase(databaseName!);

        _collection =
            database.GetCollection<DeveloperLearning>(collectionName!);
    }

    public async Task<List<DeveloperLearning>>
        GetDocumentationUsersAsync()
    {
        return await _collection
            .Find(x => x.UsesDocumentation == true)
            .ToListAsync();
    }

    public async Task<List<DeveloperLearning>>
        GetDocumentationAndAIUsersAsync()
    {
        return await _collection
            .Find(x =>
                x.UsesDocumentation == true &&
                x.UsesAIForLearning == true)
            .ToListAsync();
    }

    public async Task<List<DeveloperLearning>>
        GetByAITrustAsync(string trust)
    {
        return await _collection
            .Find(x => x.AITrust == trust)
            .ToListAsync();
    }

    public async Task<List<DeveloperLearning>>
        GetByExperienceLevelAsync(string level)
    {
        return await _collection
            .Find(x => x.ExperienceLevel == level)
            .ToListAsync();
    }

    public async Task<List<DeveloperLearning>>
        GetBackendAIUsersAsync()
    {
        return await _collection
            .Find(x =>
                x.DevType == "Developer, back-end" &&
                x.UsesAIForLearning == true)
            .SortByDescending(x => x.YearsCode)
            .Limit(20)
            .ToListAsync();
    }
}