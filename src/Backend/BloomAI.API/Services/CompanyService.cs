using BloomAI.API.Data;
using BloomAI.API.Models;

namespace BloomAI.API.Services;

public class CompanyService : ICompanyService
{
    private readonly BloomAIDbContext _context;
    private readonly IAuditService _auditService;

    public CompanyService(BloomAIDbContext context, IAuditService auditService)
    {
        _context = context;
        _auditService = auditService;
    }

    public async Task<Company> GetCompanyByIdAsync(Guid id)
    {
        var company = await _context.Companies
            .Include(c => c.Employees)
            .Include(c => c.Projects)
            .FirstOrDefaultAsync(c => c.Id == id);
        return company ?? throw new InvalidOperationException("Company not found");
    }

    public async Task<Company> CreateCompanyAsync(Company company)
    {
        company.Id = Guid.NewGuid();
        company.CreatedAt = DateTime.UtcNow;
        _context.Companies.Add(company);
        await _context.SaveChangesAsync();
        return company;
    }

    public async Task<Company> UpdateCompanyAsync(Guid id, Company company)
    {
        var existing = await _context.Companies.FindAsync(id)
            ?? throw new InvalidOperationException("Company not found");

        existing.Name = company.Name;
        existing.TaxId = company.TaxId;
        existing.Industry = company.Industry;
        existing.Country = company.Country;
        existing.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();
        return existing;
    }

    public async Task<List<Company>> GetAllCompaniesAsync()
    {
        return await _context.Companies
            .Where(c => c.IsActive)
            .ToListAsync();
    }
}
