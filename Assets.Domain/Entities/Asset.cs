using static Assets.Infrastructure.Enums;

namespace Assets.Domain.Entities
{
    public class Asset
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string ModelNo { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
        public Status Status { get; set; }
        public int AssetTypeId { get; set; }
        public AssetType AssetType { get; set; } = null!;
    }
}