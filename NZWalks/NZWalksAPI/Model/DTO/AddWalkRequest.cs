﻿namespace NZWalksAPI.Model.DTO
{
    public class AddWalkRequest
    {
        public string Name { get; set; }
        public double Length { get; set; }
        public Guid RegionId { get; set; }
        public Guid WalkDifficultyID { get; set; }
    }
}
