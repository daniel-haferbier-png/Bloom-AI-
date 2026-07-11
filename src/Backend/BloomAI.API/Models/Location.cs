using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BloomAI.API.Models;

public class Location
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required]
    [MaxLength(255)]
    public string Name { get; set; } = string.Empty;

    [MaxLength(1000)]
    public string? Address { get; set; }

    [ForeignKey(nameof(Company))]
    public Guid CompanyId { get; set; }

    public virtual Company Company { get; set; } = null!;

    public double? Latitude { get; set; }

    public double? Longitude { get; set; }

    public double? RadiusMeters { get; set; } = 100;

    [MaxLength(500)]
    public string? Notes { get; set; }

    public bool IsActive { get; set; } = true;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    public virtual ICollection<Project> Projects { get; set; } = new List<Project>();
    public virtual ICollection<TaskItem> Tasks { get; set; } = new List<TaskItem>();
}
