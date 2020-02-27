using AutoMapper;
using Core.Common.DTOs.Team;
using SimpleCRUDExample.Models.Team;

namespace SimpleCRUDExample.Profiles
{
    public class TeamProfile : Profile
    {
        public TeamProfile()
        {
            CreateMap<TeamListItemDto, TeamListViewModel>();
            CreateMap<CreateViewModel, TeamDto>();
            CreateMap<TeamDto, EditViewModel>();
            CreateMap<EditViewModel, TeamDto>();
        }
    }
}
