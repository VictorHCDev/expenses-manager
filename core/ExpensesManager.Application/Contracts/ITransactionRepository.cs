using ExpensesManager.Application.Common;
using ExpensesManager.Domain.Entities;
using ExpensesManager.Domain.Enum;

namespace ExpensesManager.Application.Contracts;

public interface ITransactionRepository
{
    Task<PagedResult<Transaction>> ListAsync(
        Guid? personId, Guid? categoryId,
        TransactionType? type,
        string? description,
        decimal? minAmount, decimal? maxAmount,
        int page, int pageSize,
        CancellationToken token
    );

    Task AddAsync(Transaction transaction, CancellationToken token);
}
