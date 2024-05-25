using AutoMapper;

namespace AutoMapperEFIssue;

public sealed class UserDtoProfile : Profile
{
    public UserDtoProfile()
    {
        CreateProjection<User, UserDto>()
            .ForMember(dto => dto.MostRecentPost,
                opt => opt.MapFrom(user => user.Posts.OrderByDescending(post => post.CreatedAt).FirstOrDefault()));
    }
}