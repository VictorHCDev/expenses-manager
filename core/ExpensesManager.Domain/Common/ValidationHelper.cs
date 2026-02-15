namespace ExpensesManager.Domain.Common;

public class ValidationHelper
{
    public static string SetDescription(string description)
    {
        if (string.IsNullOrWhiteSpace(description))
            throw new DomainException("A descrição é obrigatória.");

        if (description.Length > 400)
            throw new DomainException("A descrição deve conter no máximo 400 caracteres.");

        return description.Trim();
    }

    public static int SetAge(int age)
    {
        if (age < 0)
            throw new DomainException("A idade deve ser um número positivo.");

        return age;
    }

    public static decimal SetAmount(decimal amount)
    {
        if (amount < 0)
            throw new DomainException("O valor deve ser um número positivo.");

        return amount;
    }

    public static string SetName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new DomainException("O nome é deve ser informado para continuar!");

        if (name.Length > 100)
            throw new DomainException("O nome deve conter no máximo 200 caracteres.");

        return name.Trim();
    }
}
