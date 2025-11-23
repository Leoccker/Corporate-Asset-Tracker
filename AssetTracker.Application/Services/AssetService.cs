using AssetTracker.Application.Interfaces;
using AssetTracker.Domain;

namespace AssetTracker.Application.Services
{
    public class AssetService : IAssetService
    {
        private readonly IAssetRepository _repository;

        // Injeção de Dependência: O Service não sabe que o banco é SQL Server.
        // Ele só conhece a interface do repositório.
        public AssetService(IAssetRepository repository)
        {
            _repository = repository;
        }

        public async Task<Asset> RegisterAssetAsync(string name, string serialNumber, string tagNumber)
        {
            // REGRA DE NEGÓCIO 1: Não pode existir dois ativos com o mesmo Serial
            var existingAsset = await _repository.GetBySerialAsync(serialNumber);
            if (existingAsset != null)
            {
                throw new InvalidOperationException($"An asset with Serial Number '{serialNumber}' already exists.");
            }

            // Criação da Entidade (A Entidade se valida sozinha no construtor)
            var newAsset = new Asset(name, serialNumber, tagNumber);

            // Persistência
            await _repository.AddAsync(newAsset);
            await _repository.SaveChangesAsync();

            return newAsset;
        }

        public async Task<List<Asset>> GetAllAssetsAsync()
        {
             return await _repository.GetAllAsync();
        }
    }
}