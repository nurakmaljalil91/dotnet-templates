using Application.Common.Models;
using Application.TodoItems.Models;
using Application.TodoItems.Queries;
using Domain.Common;
using Mediator;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

/// <summary>
/// API controller for managing Todos items.
/// </summary>
[ApiController]
[Route("[controller]")]
public class TodoItemsController : ControllerBase
{
    private readonly IMediator _mediator;

    /// <summary>
    /// Initializes a new instance of the <see cref="TodoItemsController"/> class.
    /// </summary>
    /// <param name="mediator">The mediator instance.</param>
    public TodoItemsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Gets a paginated list of Todos items.
    /// </summary>
    /// <param name="query">The query parameters for retrieving Todos items.</param>
    /// <returns>A paginated list of Todos items.</returns>
    [HttpGet]
    public async Task<BaseResponse<PaginatedList<TodoItemDto>>> GetTodoItems([FromQuery] GetTodoItemsQuery query)
        => await _mediator.Send(query);

}
