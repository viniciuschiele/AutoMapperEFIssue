using AutoMapper;
using AutoMapper.QueryableExtensions;
using AutoMapperEFIssue;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(x =>
{
    x.UseNpgsql("host=localhost;port=5432;database=automapper;user id=postgres;password=postgres");
});

builder.Services.AddAutoMapper(x =>
{
    x.AddProfile(new PostDtoProfile());
    x.AddProfile(new UserDtoProfile());
});

var host = builder.Build();

var dbContext = host.Services.GetRequiredService<ApplicationDbContext>();
dbContext.Database.EnsureCreated();

var mapper = host.Services.GetRequiredService<IMapper>();

// Produces wrong SQL
dbContext.Users.ProjectTo<UserDto>(mapper.ConfigurationProvider).ToList();

// Produces the correct SQL
dbContext.Users.Select(user => new UserDto
{
    Id = user.Id,
    FirstName = user.FirstName,
    MostRecentPost = user.Posts.OrderByDescending(post => post.CreatedAt).Select(post => new PostDto
    {
        Id = post.Id,
        Title = post.Title,
        Body = post.Body,
        CreatedAt = post.CreatedAt
    }).FirstOrDefault()
}).ToList();
