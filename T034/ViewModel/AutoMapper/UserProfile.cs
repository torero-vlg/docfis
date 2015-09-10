using AutoMapper;
using Db.Entity.Administration;

namespace T034.ViewModel.AutoMapper
{
    public class UserProfile : Profile
    {
        protected override void Configure()
        {
            Mapper.CreateMap<User, UserViewModel>()
                  .ForMember(dest => dest.Roles, opt => opt.MapFrom(src => DepartmentProfile.IdsToString(src.UserRoles)));

            Mapper.CreateMap<UserViewModel, User>()
                  .ForMember(dest => dest.UserRoles,
                             opt => opt.MapFrom(src => DepartmentProfile.StringToCollection<Role>(src.Roles)));
        }
    }
}