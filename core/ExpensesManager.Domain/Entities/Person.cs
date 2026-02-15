using ExpensesManager.Domain.Common;

namespace ExpensesManager.Domain.Entities;

public sealed class Person : BaseEntity
{
    public string Name { get; private set; } = string.Empty;
    public int Age { get; private set; }

    private Person() { }

    public Person(string name, int age)
    {
        Name = ValidationHelper.SetName(name);
        Age = ValidationHelper.SetAge(age);
    }

    public void Update(string name, int age)
    {
        Name = ValidationHelper.SetName(name);
        Age = ValidationHelper.SetAge(age);
        UpdatedAt = DateTimeOffset.UtcNow;
    }
}
