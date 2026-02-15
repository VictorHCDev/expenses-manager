using ExpensesManager.Application.Common;
using ExpensesManager.Application.Contracts;
using ExpensesManager.Application.DTO;
using ExpensesManager.Domain.Common;
using ExpensesManager.Domain.Entities;
using ExpensesManager.Domain.Enum;

namespace ExpensesManager.Application.Services;

public sealed class TransactionService(
    ITransactionRepository transactions, IPersonRepository people,
    ICategoryRepository category)
{
    private readonly ITransactionRepository _transactions = transactions;
    private readonly IPersonRepository _people = people;
    private readonly ICategoryRepository _categories = category;

    public async Task<TransactionResponse> CreateAsync(CreateTransactionRequest request, CancellationToken token)
    {
        var person = await _people.GetByIdAsync(request.PersonId, token)
            ?? throw new DomainException("Pessoa não encontrada");

        var category = await _categories.GetByIdAsync(request.CategoryId, token)
            ?? throw new DomainException("Categoria não encontrada");

        if (person.Age < 18 && request.Type != TransactionType.Expense)
            throw new DomainException("Menores de idade só podem registrar despesas.");

        var isAllowed = category.Purpose switch
        {
            CategoryPurpose.Expense => request.Type == TransactionType.Expense,
            CategoryPurpose.Income => request.Type == TransactionType.Income,
            CategoryPurpose.Both => request.Type == TransactionType.Expense || request.Type == TransactionType.Income,
            _ => false
        };

        if (!isAllowed)
            throw new DomainException("Tipo de transação não é compatível com a categoria informada.");

        var transaction = new Transaction(
            request.Description,
            request.Amount,
            request.Type,
            request.PersonId,
            request.CategoryId
        );

        await _transactions.AddAsync(transaction, token);

        return new TransactionResponse(
            transaction.Id,
            transaction.Description,
            transaction.Amount,
            transaction.Type,
            person.Id,
            person.Name,
            category.Id,
            category.Description
        );
    }

    public Task<PagedResult<Transaction>> ListRawAsync(
        Guid? personId, Guid? categoryId,
        TransactionType? type,
        string? description,
        decimal? minAmount, decimal? maxAmount,
        int page, int pageSize,
        CancellationToken token
    )
    => _transactions.ListAsync(personId, categoryId, type, description, minAmount, maxAmount, page, pageSize, token);
}
