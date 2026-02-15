using ExpensesManager.Domain.Common;
using ExpensesManager.Domain.Enum;

namespace ExpensesManager.Domain.Entities;

public sealed class Transaction : BaseEntity
{
    public string Description { get; private set; } = string.Empty;
    public decimal Amount { get; private set; }
    public TransactionType Type { get; private set; }
    public Guid CategoryId { get; private set; }
    public Guid PersonId { get; private set; }

    public Person? Person { get; private set; }
    public Category? Category { get; private set; }
    private Transaction() { }

    public Transaction(string description, decimal amount, TransactionType type, Guid personId, Guid categoryId)
    {
        Description = ValidationHelper.SetDescription(description);
        Amount = ValidationHelper.SetAmount(amount);
        Type = type;
        PersonId = personId;
        CategoryId = categoryId;
    }
}
