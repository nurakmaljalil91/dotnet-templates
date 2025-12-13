using System;
using System.Collections.Generic;
using System.Text;
using Application.Common.Interfaces;
using Application.Common.Models;
using Application.TodoItems.Models;
using Domain.Common;
using Mediator;

namespace Application.TodoItems.Queries;

public class GetTodoItemsQuery : PaginatedRequest, IRequest<PaginatedList<TodoItemDto>>
{
}

public record GetTodoItemsQueryHandler : IRequestHandler<GetTodoItemsQuery, PaginatedList<TodoItemDto>>
{
    private readonly IApplicationDbContext _context;

    public GetTodoItemsQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public Task<PaginatedList<TodoItemDto>> Handle(GetTodoItemsQuery request, CancellationToken cancellationToken)
    {
        var query = from todoItem in _context.TodoItems
                    select new TodoItemDto(todoItem);
        return PaginatedList<TodoItemDto>.CreateAsync(query, request.Page, request.Total);
    }
}
