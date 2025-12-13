using Application.Common.Models;
using Application.TodoItems.Models;
using Application.TodoItems.Queries;
using Mediator;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class TodoItemsController : ControllerBase
{
    private readonly IMediator _mediator;

    public TodoItemsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<PaginatedList<TodoItemDto>> GetTodoItems([FromQuery] GetTodoItemsQuery query)
        => await _mediator.Send(query);

}
