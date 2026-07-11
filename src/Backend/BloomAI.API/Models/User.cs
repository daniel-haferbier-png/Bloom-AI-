using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BloomAI.API.Models;

public class User
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required]
    [MaxLength(100)]
    public string FirstName { get; set; } = string.Empty;

    [Required]
    [MaxLength(100)]
    public string LastName { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    [MaxLength(255)]
    public string Email { get; set; } = string.Empty;

    [Required]
    public string PasswordHash { get; set; } = string.Empty;

    [Required]
    public string Role { get; set; } = "Employee";

    [MaxLength(10)]
    public string LanguagePreference { get; set; } = "en";

    [ForeignKey(nameof(Company))]
    public Guid? CompanyId { get; set; }

    public virtual Company? Company { get; set; }

    public bool IsActive { get; set; } = true;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    public virtual ICollection<Project> AssignedProjects { get; set; } = new List<Project>();
    public virtual ICollection<TaskItem> AssignedTasks { get; set; } = new List<TaskItem>();
}
