using System;
using System.Collections.Generic;
using System.Text;
using Application.Common.Interfaces;
using Application.Common.Models;
using Application.TodoItems.Models;
using Domain.Common;
using Mediator;

namespace Application.TodoItems.Queries;

/// <summary>
/// Represents a paginated query for retrieving todos items.
/// </summary>
public class GetTodoItemsQuery : PaginatedRequest, IRequest<PaginatedList<TodoItemDto>>
{
}

/// <summary>
/// Handles the retrieval of paginated todoz items based on the specified query parameters.
/// </summary>
public record GetTodoItemsQueryHandler : IRequestHandler<GetTodoItemsQuery, PaginatedList<TodoItemDto>>
{
    private readonly IApplicationDbContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetTodoItemsQueryHandler"/> class.
    /// </summary>
    /// <param name="context">The application database context.</param>
    public GetTodoItemsQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Handles the request to retrieve a paginated list of <see cref="TodoItemDto"/> items based on the specified <paramref name="request"/> parameters.
    /// </summary>
    /// <param name="request">The query parameters for retrieving todos items.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a paginated list of <see cref="TodoItemDto"/>.</returns>
    public Task<PaginatedList<TodoItemDto>> Handle(GetTodoItemsQuery request, CancellationToken cancellationToken)
    {
        var query = from todoItem in _context.TodoItems
                    select new TodoItemDto(todoItem);
        return PaginatedList<TodoItemDto>.CreateAsync(query, request.Page, request.Total);
    }
}
