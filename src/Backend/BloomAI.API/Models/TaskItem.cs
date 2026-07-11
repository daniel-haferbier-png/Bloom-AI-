using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BloomAI.API.Models;

public class TaskItem
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required]
    [MaxLength(255)]
    public string Title { get; set; } = string.Empty;

    [MaxLength(1000)]
    public string? Description { get; set; }

    [ForeignKey(nameof(Project))]
    public Guid ProjectId { get; set; }

    public virtual Project Project { get; set; } = null!;

    [MaxLength(50)]
    public string Status { get; set; } = "Open";

    [MaxLength(50)]
    public string Priority { get; set; } = "Medium";

    [ForeignKey(nameof(AssignedTo))]
    public Guid? AssignedToId { get; set; }

    public virtual User? AssignedTo { get; set; }

    [ForeignKey(nameof(Location))]
    public Guid? LocationId { get; set; }

    public virtual Location? Location { get; set; }

    public DateTime? DueDate { get; set; }

    public DateTime? CompletedAt { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}
