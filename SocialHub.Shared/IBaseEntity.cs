namespace SocialHub.Shared;

public class BaseEntity
{
    public Guid Id { get; set; }
    
    public BaseEntity()
    {
        Id = Guid.NewGuid();
    }
}