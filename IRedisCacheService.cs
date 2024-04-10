using System;

public interface IRedisCacheService
{
    Task<bool> SaveMarksAsync(string userId, int marks);
    Task<int?> GetMarksAsync(string userId);
}