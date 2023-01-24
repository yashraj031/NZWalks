namespace NZWalksAPI.Model.Domain
{
    public class Walk
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public double Length { get; set; }
        public Guid RegionId { get; set; }
        public Guid WalkDifficultyID { get; set; }

        // Navigation Property

        public Region Region { get; set; }
        public WalkDifficulty WlkDifficulty { get; set; }

    }
}
