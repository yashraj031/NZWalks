using AutoMapper;

namespace NZWalksAPI.Profiles
{
    public class RegionsProfile: Profile
    {
        public RegionsProfile()
        {
            // Map for our Model
            CreateMap<Model.Domain.Region, Model.DTO.Region>();

             /*
              .ReverseMap(); and  --> For Reverse Map
             .ForMember(dest => dest.Id, option => option.MapFrom(src => src.RegionId));  --> If properties are Not Same
             */
              
        }
    }
}
