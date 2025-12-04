using Npgsql;

namespace Application.Common.Interfaces;

public interface INpgsqlConnectionFactory
{
    NpgsqlConnection CreateConnection();
}
