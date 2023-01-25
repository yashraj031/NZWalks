﻿using AutoMapper;

namespace NZWalksAPI.Profiles
{
    public class WalksProfile: Profile
    {
        public WalksProfile() 
        {
            CreateMap<Model.Domain.Walk, Model.DTO.Walk>()
                .ReverseMap();

            CreateMap<Model.Domain.WalkDifficulty, Model.DTO.WalkDifficulty>()
                .ReverseMap();
        }
    }
}
