using System;
using StackExchange.Redis;
public class RedisCacheService : IRedisCacheService
{
    private readonly IDatabase _database;

    public RedisCacheService(IConnectionMultiplexer connectionMultiplexer)
    {
        _database = connectionMultiplexer.GetDatabase();
    }

    public async Task<bool> SaveMarksAsync(string userId, int marks)
    {
        return await _database.StringSetAsync(userId, marks);
    }

    public async Task<int?> GetMarksAsync(string userId)
    {
        var marks = await _database.StringGetAsync(userId);
        return marks.HasValue ? (int)marks : (int?)null;
    }
}