using AutoMapper;

namespace AutoMapperEFIssue;

public class PostDtoProfile : Profile
{
    public PostDtoProfile()
    {
        CreateProjection<Post, PostDto>();
    }
}
