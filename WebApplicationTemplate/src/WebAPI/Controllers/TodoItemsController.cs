using Application.Common.Models;
using Application.TodoItems.Commands;
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
[Route("api/[controller]")]
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
    public async Task<BaseResponse<PaginatedEnumerable<TodoItemDto>>> GetTodoItems([FromQuery] GetTodoItemsQuery query)
        => await _mediator.Send(query);

    /// <summary>
    /// Submits a request to create a new Todos item.
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<BaseResponse<TodoItemDto>> CreateTodoItem([FromBody] CreateTodoItemCommand command)
        => await _mediator.Send(command);

    /// <summary>
    /// Updates an existing Todos item.
    /// </summary>
    [HttpPatch]
    public async Task<BaseResponse<TodoItemDto>> UpdateTodoItem([FromBody] UpdateTodoItemComand command)
        => await _mediator.Send(command);

    /// <summary>
    /// Deletes a Todos item by its identifier.
    /// </summary>
    /// <param name="id">The identifier of the Todos item to delete.</param>
    /// <returns>A response indicating the result of the delete operation.</returns>
    [HttpDelete]
    [Route("{id:long}")]
    public async Task<BaseResponse<object>> DeleteTodoItem(long id)
        => await _mediator.Send(new DeleteTodoItemCommand(id));
}
