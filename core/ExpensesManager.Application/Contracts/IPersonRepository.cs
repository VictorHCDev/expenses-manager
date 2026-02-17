using ExpensesManager.Application.Common;
using ExpensesManager.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExpensesManager.Application.Contracts;

public interface IPersonRepository
{
    Task<Person?> GetByIdAsync(Guid id, CancellationToken token);
    Task<PagedResult<Person>> ListAsync(string? name, int? minAge, int? maxAge, int page, int pageSize, CancellationToken token);
    Task AddAsync(Person person, CancellationToken token);
    Task UpdateAsync(Person person, CancellationToken token);
    void Remove(Person person);
}
