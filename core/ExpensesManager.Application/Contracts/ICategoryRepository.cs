using ExpensesManager.Application.Common;
using ExpensesManager.Domain.Entities;
using ExpensesManager.Domain.Enum;

namespace ExpensesManager.Application.Contracts;

public interface ICategoryRepository
{
    Task<Category?> GetByIdAsync(Guid id, CancellationToken token);
    Task<PagedResult<Category>> ListAsync(string? description, CategoryPurpose? purpose, int page, int pageSize, CancellationToken token);
    Task AddAsync(Category category, CancellationToken token);
}
