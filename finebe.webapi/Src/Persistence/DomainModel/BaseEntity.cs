using Microsoft.EntityFrameworkCore;

namespace finebe.webapi.Src.Persistence.DomainModel;

[PrimaryKey(nameof(Id))]
public class BaseEntity
{    
    public Guid Id { get; set; } = Guid.NewGuid();
    public string LastModifiedBy { get; set;}
    public DateTime LastModifiedAt { get; set; }
    public string CreatedBy { get; set;}
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public bool IsDeleted { get; set; } = false;
}
