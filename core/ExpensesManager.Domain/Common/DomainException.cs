namespace ExpensesManager.Domain.Common;

/// <summary>
/// O propósito desta classe é retornar uma exceção quando alguma das regras
/// de negócio definidas no domínio da aplicação for violada.
/// </summary>
/// <param name="message"></param>
public sealed class DomainException(string message) : Exception(message)
{
}
