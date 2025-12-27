using Domain.Entities;
using Domain.ValueObjects;

namespace Domain.UnitTests.Entities;

public class TodoListTests
{
    [Fact]
    public void Defaults_AreInitialized()
    {
        var list = new TodoList();

        Assert.Equal(Colour.White, list.Colour);
        Assert.NotNull(list.Items);
        Assert.Empty(list.Items);
        Assert.Null(list.Title);
    }

    [Fact]
    public void Properties_CanBeSet()
    {
        var list = new TodoList
        {
            Title = "My list",
            Colour = Colour.Red
        };

        list.Items.Add(new TodoItem { Title = "Item 1" });

        Assert.Equal("My list", list.Title);
        Assert.Equal(Colour.Red, list.Colour);
        Assert.Single(list.Items);
    }
}
