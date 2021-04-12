using AutoMapper;

namespace C3.Features.Users
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Domain.Person, User>(MemberList.None);
        }
    }
}
