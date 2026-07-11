using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BloomAI.API.Models;

public class AuditLog
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    [ForeignKey(nameof(User))]
    public Guid UserId { get; set; }

    public virtual User User { get; set; } = null!;

    [Required]
    [MaxLength(100)]
    public string Action { get; set; } = string.Empty;

    [Required]
    [MaxLength(100)]
    public string EntityType { get; set; } = string.Empty;

    [Required]
    public Guid EntityId { get; set; }

    [MaxLength(1000)]
    public string? OldValues { get; set; }

    [MaxLength(1000)]
    public string? NewValues { get; set; }

    [MaxLength(45)]
    public string? IpAddress { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
