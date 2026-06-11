namespace HotelManagement.Application.Interfaces.Services
{
    public interface ITokenCache
    {
        Task SetAsync(string key, string value, TimeSpan expiry);
        Task<string?> GetAsync(string key);
        Task RemoveAsync(string key);
    }
}
