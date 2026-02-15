using ExpensesManager.Application.Contracts;
using ExpensesManager.Application.DTO;
using ExpensesManager.Domain.Entities;
using ExpensesManager.Domain.Enum;

namespace ExpensesManager.Application.Services;

public sealed class CategoriesService(ICategoryRepository categories)
{
    private readonly ICategoryRepository _categories = categories;

    public async Task<CategoryResponse> CreateAsync(CreateCategoryRequest request, CancellationToken token)
    {
        var category = new Category(request.Description, request.Purpose);
        await _categories.AddAsync(category, token);

        return new CategoryResponse(category.Id, category.Description, category.Purpose);
    }

    public async Task<CategoryResponse?> GetAsync(Guid id, CancellationToken token)
    {
        var category = await _categories.GetByIdAsync(id, token);
        return category is null ? null : new CategoryResponse(category.Id, category.Description, category.Purpose);
    }

    public async Task<(IReadOnlyCollection<CategoryResponse> Items, int totalItems)> ListAsync(
        string? description, CategoryPurpose? purpose, int page, int pageSize, CancellationToken token)
    {
        var paged = await _categories.ListAsync(description, purpose, page, pageSize, token);
        return (paged.Items.Select(cat =>
            new CategoryResponse(cat.Id, cat.Description, cat.Purpose)
        ).ToList(), paged.TotalItems);
    }
}
