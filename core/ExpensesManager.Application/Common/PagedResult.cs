using System;
using System.Collections.Generic;
using System.Text;

namespace ExpensesManager.Application.Common;

public sealed class PagedResult<T>
{
    public required IReadOnlyCollection<T> Items { get; init; }
    public required int Page { get; init; }
    public required int PageSize { get; init; }
    public required int TotalItems { get; init; }

    public int TotalPages => (int)Math.Ceiling((double)TotalItems / PageSize);
}
