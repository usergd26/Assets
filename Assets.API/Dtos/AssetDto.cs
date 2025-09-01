namespace Assets.API.Dtos
{
    public class AssetDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int AssetTypeId { get; set; }
    }
}