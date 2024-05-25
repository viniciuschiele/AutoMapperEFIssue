namespace AutoMapperEFIssue;

public class User
{
    public Guid Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public IEnumerable<Post> Posts { get; set; }
}
