using System;

namespace AssetTracker.Domain
{
    public class Asset
    {
        public int Id { get; private set; }

        public string Name { get; private set; } = string.Empty;
        
        public string SerialNumber { get; private set; } = string.Empty;
        
        public string TagNumber { get; private set; } = string.Empty;

        public AssetStatus Status { get; private set; }

        public DateTime CreatedAt { get; private set; }

        protected Asset() { }

        public Asset(string name, string serialNumber, string tagNumber)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name cannot be empty");

            Name = name;
            SerialNumber = serialNumber;
            TagNumber = tagNumber;
            Status = AssetStatus.Available;
            CreatedAt = DateTime.UtcNow;
        }

        public void MarkAsLoaned()
        {
            if (Status != AssetStatus.Available)
                throw new InvalidOperationException("Asset is not available for loan.");
            
            Status = AssetStatus.InUse;
        }

        public void ReturnAsset()
        {
            Status = AssetStatus.Available;
        }
    }

    public enum AssetStatus
    {
        Available,
        InUse,
        Maintenance,
        Retired
    }
}