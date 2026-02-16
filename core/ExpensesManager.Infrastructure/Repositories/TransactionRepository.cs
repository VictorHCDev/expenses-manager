using ExpensesManager.Application.Common;
using ExpensesManager.Application.Contracts;
using ExpensesManager.Domain.Entities;
using ExpensesManager.Domain.Enum;
using ExpensesManager.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace ExpensesManager.Infrastructure.Repositories;

public sealed class TransactionRepository(AppDbContext dbContext) : ITransactionRepository
{
    private readonly AppDbContext _dbContext = dbContext;

    public async Task<PagedResult<Transaction>> ListAsync(
        Guid? personId, Guid? categoryId,
        TransactionType? type, string? description,
        decimal? minAmount, decimal? maxAmount,
        int page, int pageSize, CancellationToken token
    )
    {
        page = Math.Max(page, 1);
        pageSize = Math.Clamp(pageSize, 1, 200);

        var query = _dbContext.Transactions
            .AsNoTracking()
            .Include(transaction => transaction.Person)
            .Include(transaction => transaction.Category)
            .AsQueryable();

        if (personId is not null)
            query = query.Where(x => x.PersonId == personId);

        if (categoryId is not null)
            query = query.Where(x => x.CategoryId == categoryId);

        if (type is not null)
            query = query.Where(x => x.Type == type);

        if (!string.IsNullOrWhiteSpace(description))
            query = query.Where(x => x.Description.Contains(description.Trim(), StringComparison.CurrentCultureIgnoreCase));

        if (minAmount is not null)
            query = query.Where(x => x.Amount >= minAmount);

        if (maxAmount is not null)
            query = query.Where(x => x.Amount <= maxAmount);

        var total = await query.CountAsync(token);
        var items = await query.OrderByDescending(x => x.Id)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(token);

        return new PagedResult<Transaction> { 
            Items = items,
            Page = page,
            PageSize = pageSize,
            TotalItems = total
        };
    }

    public async Task AddAsync(Transaction transaction, CancellationToken token)
    {
        await _dbContext.Transactions.AddAsync(transaction, token).AsTask();
        _dbContext.SaveChanges();
    }
}
