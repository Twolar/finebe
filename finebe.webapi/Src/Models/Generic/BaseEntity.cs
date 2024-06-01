using Microsoft.EntityFrameworkCore;

namespace finebe.webapi;

[PrimaryKey(nameof(Id))]
public class BaseEntity
{    
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid LastModifiedBy { get; set;}
    public DateTime LastModifiedAt { get; set; }
    public Guid CreatedBy { get; set;}
    public DateTime CreatedAt { get; set; } = DateTime.Now;
}
