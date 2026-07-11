using BloomAI.API.Models;

namespace BloomAI.API.Data;

public static class BloomAIDbContextSeeder
{
    public static void Seed(BloomAIDbContext context)
    {
        if (context.Companies.Any())
            return;

        var company = new Company
        {
            Id = Guid.NewGuid(),
            Name = "BuildCraft Solutions",
            TaxId = "DE123456789",
            Industry = "Construction",
            Country = "Germany",
            Phone = "+49 123 456789",
            Address = "123 Construction Ave, Berlin, Germany",
            IsActive = true,
            CreatedAt = DateTime.UtcNow
        };

        context.Companies.Add(company);
        context.SaveChanges();

        var adminUser = new User
        {
            Id = Guid.NewGuid(),
            FirstName = "Admin",
            LastName = "User",
            Email = "admin@bloomai.com",
            PasswordHash = BCrypt.Net.BCrypt.HashPassword("Admin@123456"),
            Role = "Administrator",
            LanguagePreference = "en",
            CompanyId = company.Id,
            IsActive = true,
            CreatedAt = DateTime.UtcNow
        };

        context.Users.Add(adminUser);

        var employee = new User
        {
            Id = Guid.NewGuid(),
            FirstName = "John",
            LastName = "Worker",
            Email = "john@bloomai.com",
            PasswordHash = BCrypt.Net.BCrypt.HashPassword("Worker@123456"),
            Role = "Employee",
            LanguagePreference = "en",
            CompanyId = company.Id,
            IsActive = true,
            CreatedAt = DateTime.UtcNow
        };

        context.Users.Add(employee);

        var location = new Location
        {
            Id = Guid.NewGuid(),
            Name = "Berlin Construction Site",
            Address = "456 Work Street, Berlin, Germany",
            CompanyId = company.Id,
            Latitude = 52.5200,
            Longitude = 13.4050,
            RadiusMeters = 100,
            Notes = "Main construction site in Berlin",
            IsActive = true,
            CreatedAt = DateTime.UtcNow
        };

        context.Locations.Add(location);

        context.SaveChanges();

        var project = new Project
        {
            Id = Guid.NewGuid(),
            Name = "Residential Building Renovation",
            Description = "Complete renovation of a 5-story residential building",
            CompanyId = company.Id,
            CustomerId = null,
            LocationId = location.Id,
            Status = "InProgress",
            StartDate = DateTime.UtcNow.AddDays(-10),
            EndDate = DateTime.UtcNow.AddMonths(3),
            Budget = 500000,
            CreatedAt = DateTime.UtcNow
        };

        context.Projects.Add(project);
        context.SaveChanges();

        var tasks = new[]
        {
            new TaskItem
            {
                Id = Guid.NewGuid(),
                Title = "Electrical Installation - Phase 1",
                Description = "Install electrical wiring in floors 1-2",
                ProjectId = project.Id,
                Status = "InProgress",
                Priority = "High",
                AssignedToId = employee.Id,
                LocationId = location.Id,
                DueDate = DateTime.UtcNow.AddDays(7),
                CreatedAt = DateTime.UtcNow
            },
            new TaskItem
            {
                Id = Guid.NewGuid(),
                Title = "Plumbing Installation",
                Description = "Install plumbing for bathrooms and kitchen",
                ProjectId = project.Id,
                Status = "Open",
                Priority = "High",
                AssignedToId = null,
                LocationId = location.Id,
                DueDate = DateTime.UtcNow.AddDays(14),
                CreatedAt = DateTime.UtcNow
            }
        };

        context.Tasks.AddRange(tasks);
        context.SaveChanges();
    }
}
