using ExpensesManager.Application.DTO;
using ExpensesManager.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace ExpensesManager.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PeopleController(PeopleService people) : ControllerBase
{
    private readonly PeopleService _people = people;

    [HttpPost]
    public async Task<ActionResult<CategoryResponse>> Create([FromBody] CreatePersonRequest request, CancellationToken token)
    {
        var created = await _people.CreateAsync(request, token);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<PersonResponse>> GetById(Guid id, CancellationToken token)
    {
        var person = await _people.GetAsync(id, token);
        return person is null ? NotFound() : Ok();
    }

    [HttpGet]
    public async Task<ActionResult> List(
        [FromQuery] string? name,
        [FromQuery] int? minAge,
        [FromQuery] int? maxAge,
        [FromQuery] int page,
        [FromQuery] int pageSize = 10,
        CancellationToken token = default
    )
    {
        var (items, total) = await _people.ListAsync(name, minAge, maxAge, page, pageSize, token);
        return Ok(new { items, page, pageSize, totalItems = total });
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult> Update(Guid id, [FromBody] UpdatePersonRequest request, CancellationToken token)
    {
        var updated = await _people.UpdateAsync(id, request, token);
        return updated ? NoContent() : NotFound();
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> Delete(Guid id, CancellationToken token)
    {
        var deleted = await _people.DeleteAsync(id, token);
        return deleted ? NoContent() : NotFound();
    }
}
