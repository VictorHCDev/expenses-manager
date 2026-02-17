using ExpensesManager.Application.DTO;
using ExpensesManager.Application.Services;
using ExpensesManager.Domain.Enum;
using Microsoft.AspNetCore.Mvc;

namespace ExpensesManager.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoriesController(CategoriesService category) : ControllerBase
{
    private readonly CategoriesService _category = category;

    [HttpPost]
    public async Task<ActionResult<CategoryResponse>> Create([FromBody] CreateCategoryRequest request, CancellationToken token)
    {
        var created = await _category.CreateAsync(request, token);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<CategoryResponse>> GetById(Guid id, CancellationToken token)
    {
        var category = await _category.GetAsync(id, token);
        return category is null ? NotFound() : Ok(category);
    }

    [HttpGet]
    public async Task<ActionResult> List(
        [FromQuery] string? description,
        [FromQuery] CategoryPurpose? purpose,
        [FromQuery] int page,
        [FromQuery] int pageSize,
        CancellationToken token)
    {
        var (items, total) = await _category.ListAsync(description, purpose, page, pageSize, token);
        return Ok(new { items, page, pageSize, totalItems = total});
    }
}
