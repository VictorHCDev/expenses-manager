using ExpensesManager.Application.Contracts;
using ExpensesManager.Application.DTO;
using ExpensesManager.Domain.Entities;

namespace ExpensesManager.Application.Services;

public sealed class PeopleService(IPersonRepository people)
{
    private readonly IPersonRepository _people = people;

    public async Task<PersonResponse> CreateAsync(CreatePersonRequest request, CancellationToken token)
    {
        var person = new Person(request.Name, request.Age);
        await _people.AddAsync(person, token);

        return new PersonResponse(person.Id, person.Name, person.Age);
    }

    public async Task<PersonResponse?> GetAsync(Guid id, CancellationToken token)
    {
        var person = await _people.GetByIdAsync(id, token);
        return person is null ? null : new PersonResponse(person.Id, person.Name, person.Age);
    }

    public async Task<(IReadOnlyCollection<PersonResponse> Items, int totalItems)> ListAsync(string? name, int? minAge, int? maxAge,
        int page, int pageSize, CancellationToken token)
    {
        var paged = await _people.ListAsync(name, minAge, maxAge, page, pageSize, token);
        return (paged.Items.Select(p => new PersonResponse(p.Id, p.Name, p.Age)).ToList(), paged.TotalItems);
    }

    public async Task<bool> UpdateAsync(Guid id, UpdatePersonRequest request, CancellationToken token)
    {
        var person = await _people.GetByIdAsync(id, token);
        if (person is null)
            return false;

        person.Update(request.Name, request.Age);
        await _people.UpdateAsync(person, token);

        return true;
    }

    public async Task<bool> DeleteAsync(Guid id, CancellationToken token)
    {
        var person = await _people.GetByIdAsync(id, token);
        if (person is null) 
            return false;

        _people.Remove(person);
        return true;
    }
}
