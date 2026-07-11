using System.ComponentModel.DataAnnotations;

namespace BloomAI.API.Models;

public class Company
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required]
    [MaxLength(255)]
    public string Name { get; set; } = string.Empty;

    [MaxLength(50)]
    public string? TaxId { get; set; }

    [MaxLength(100)]
    public string? Industry { get; set; }

    [MaxLength(100)]
    public string? Country { get; set; }

    [MaxLength(50)]
    public string? Phone { get; set; }

    [MaxLength(255)]
    public string? Address { get; set; }

    public bool IsActive { get; set; } = true;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    public virtual ICollection<User> Employees { get; set; } = new List<User>();
    public virtual ICollection<Project> Projects { get; set; } = new List<Project>();
    public virtual ICollection<Location> Locations { get; set; } = new List<Location>();
}
