namespace Application.Common.Interfaces;

public interface ICurrentUserService
{
    string? Username { get; }
    List<string> Roles { get; }
}
