using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BloomAI.API.Models;

public class Project
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required]
    [MaxLength(255)]
    public string Name { get; set; } = string.Empty;

    [MaxLength(1000)]
    public string? Description { get; set; }

    [ForeignKey(nameof(Company))]
    public Guid CompanyId { get; set; }

    public virtual Company Company { get; set; } = null!;

    [ForeignKey(nameof(Customer))]
    public Guid? CustomerId { get; set; }

    public virtual User? Customer { get; set; }

    [ForeignKey(nameof(Location))]
    public Guid? LocationId { get; set; }

    public virtual Location? Location { get; set; }

    [MaxLength(50)]
    public string Status { get; set; } = "Open";

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public decimal? Budget { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    public virtual ICollection<TaskItem> Tasks { get; set; } = new List<TaskItem>();
}
