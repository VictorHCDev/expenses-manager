using ExpensesManager.Domain.Common;
using ExpensesManager.Domain.Enum;

namespace ExpensesManager.Domain.Entities;

public sealed class Category : BaseEntity
{
    public string Description { get; private set; } = string.Empty;
    public CategoryPurpose Purpose { get; private set; }


    private Category() { }

    public Category(string description, CategoryPurpose purpose)
    {
        ValidationHelper.SetDescription(description);
        Purpose = purpose;
    }

    public void Update(string description, CategoryPurpose purpose)
    {
        Description = ValidationHelper.SetDescription(description);
        Purpose = purpose;
        UpdatedAt = DateTimeOffset.UtcNow;
    }
}
