
namespace FlightAppFrontend.Shared.Services
{
    public interface IJsInteropService
    {
        void CreateAlert(string message);
        Task<string> GetItem(string key);
        Task RemoveItem(string key);
        Task SetItem(string key, string value);
    }
}