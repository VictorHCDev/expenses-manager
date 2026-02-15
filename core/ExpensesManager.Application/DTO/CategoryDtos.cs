using ExpensesManager.Domain.Enum;

namespace ExpensesManager.Application.DTO;

public sealed record CreateCategoryRequest(string Description, CategoryPurpose Purpose);

public sealed record CategoryResponse(Guid Id, string Description, CategoryPurpose Purpose);