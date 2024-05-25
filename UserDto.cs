namespace AutoMapperEFIssue;

public class UserDto
{
    public Guid Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public PostDto? MostRecentPost { get; set; }
}