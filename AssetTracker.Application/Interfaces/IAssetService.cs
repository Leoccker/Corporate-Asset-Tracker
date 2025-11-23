using AssetTracker.Domain;

namespace AssetTracker.Application.Interfaces
{
    public interface IAssetService
    {
        Task<Asset> RegisterAssetAsync(string name, string serialNumber, string tagNumber);
        Task<List<Asset>> GetAllAssetsAsync(); 
    }
}