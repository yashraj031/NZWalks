namespace NZWalksAPI.Model.DTO
{
    public class UpdateWalkRequest
    {
        public string Name { get; set; }
        public double Length { get; set; }
        public Guid RegionId { get; set; }
        public Guid WalkDifficultyID { get; set; }
    }
}
