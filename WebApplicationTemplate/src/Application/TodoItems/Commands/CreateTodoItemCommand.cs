using Application.Common.Interfaces;
using Application.TodoItems.Models;
using Domain.Common;
using Domain.Entities;
using FluentValidation;
using Mediator;

namespace Application.TodoItems.Commands;

/// <inheritdoc />
public class CreateTodoItemCommand: IRequest<BaseResponse<TodoItemDto>>
{
    /// <summary>
    /// Gets or sets the title of the to-do item.
    /// </summary>
    public string? Title { get; set; }
}

/// <inheritdoc />
public class CreateTodoItemCommandHandler : IRequestHandler<CreateTodoItemCommand, BaseResponse<TodoItemDto>>
{
    private readonly IApplicationDbContext _context;

    /// <summary>
    /// Creates a new instance of the <see cref="CreateTodoItemCommandHandler"/> class.
    /// </summary>
    /// <param name="context"></param>
    public CreateTodoItemCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    /// <inheritdoc />
    public async Task<BaseResponse<TodoItemDto>> Handle(CreateTodoItemCommand request, CancellationToken cancellationToken)
    {
        var entity = new TodoItem
        {
            Title = request.Title
        };

        _context.TodoItems.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        var dto = new TodoItemDto(entity);

        return  BaseResponse<TodoItemDto>.Ok(dto, "Todo item created successfully.");
    }
}

/// <summary>
/// Validator for <see cref="CreateTodoItemCommand"/>.
/// </summary>
public class CreateTodoItemCommandValidator : AbstractValidator<CreateTodoItemCommand>
{

    /// <summary>
    /// Initializes a new instance of the <see cref="CreateTodoItemCommandValidator"/> class.
    /// </summary>
    public CreateTodoItemCommandValidator()
    {
        RuleFor(m => m.Title)
            .MaximumLength(200)
            .WithMessage("Title cannot exceed 200 characters")
            .NotEmpty().WithMessage("Title is required.");
    }
}
