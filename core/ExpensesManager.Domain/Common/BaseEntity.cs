using System;
using System.Collections.Generic;
using System.Text;

namespace ExpensesManager.Domain.Common;

public abstract class BaseEntity
{
    public Guid Id { get; protected set; } = Guid.CreateVersion7();
    public DateTimeOffset CreatedAt { get; protected set; } = DateTimeOffset.UtcNow;
    public DateTimeOffset UpdatedAt { get; protected set; } = DateTimeOffset.UtcNow;
}
