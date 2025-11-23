using AssetTracker.Domain;

namespace AssetTracker.Application.Interfaces
{
    public interface IAssetRepository
    {
        Task<Asset?> GetByIdAsync(int id);
        
        // --- O ERRO ESTÁ AQUI ---
        // Certifique-se de que esta linha exata existe no arquivo:
        Task<Asset?> GetBySerialAsync(string serialNumber);
        Task<List<Asset>> GetAllAsync();
        // ------------------------

        Task AddAsync(Asset asset);
        Task UpdateAsync(Asset asset);
        Task SaveChangesAsync(); 
    }
}