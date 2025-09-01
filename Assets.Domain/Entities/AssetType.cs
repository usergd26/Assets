namespace Assets.Domain.Entities
{
    public class AssetType
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public ICollection<Asset> Assets { get; set; } = new List<Asset>();
    }
}