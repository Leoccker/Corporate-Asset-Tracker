using AssetTracker.Application.Interfaces;
using AssetTracker.Domain;
using AssetTracker.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace AssetTracker.Infrastructure.Repositories
{
    public class AssetRepository : IAssetRepository
    {
        private readonly AppDbContext _context;

        public AssetRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Asset?> GetByIdAsync(int id)
        {
            return await _context.Assets.FindAsync(id);
        }

        // AQUI ESTÁ A IMPLEMENTAÇÃO DO MÉTODO
        public async Task<Asset?> GetBySerialAsync(string serialNumber)
        {
            return await _context.Assets
                .FirstOrDefaultAsync(a => a.SerialNumber == serialNumber);
        }

        public async Task<List<Asset>> GetAllAsync()
        {
            return await _context.Assets.ToListAsync();
        }

        public async Task AddAsync(Asset asset)
        {
            await _context.Assets.AddAsync(asset);
        }

        public Task UpdateAsync(Asset asset)
        {
            _context.Assets.Update(asset);
            return Task.CompletedTask;
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}