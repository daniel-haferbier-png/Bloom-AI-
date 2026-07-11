using BloomAI.API.DTOs;
using BloomAI.API.Models;

namespace BloomAI.API.Services;

public interface ICompanyService
{
    Task<Company> GetCompanyByIdAsync(Guid id);
    Task<Company> CreateCompanyAsync(Company company);
    Task<Company> UpdateCompanyAsync(Guid id, Company company);
    Task<List<Company>> GetAllCompaniesAsync();
}
