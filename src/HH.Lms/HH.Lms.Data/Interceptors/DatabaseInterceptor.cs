using System.Data.Common;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace HH.Lms.Data.Interceptors;

public class DatabaseInterceptor : DbCommandInterceptor
{
    public override InterceptionResult<DbDataReader> ReaderExecuting(
        DbCommand command,
        CommandEventData eventData,
        InterceptionResult<DbDataReader> result)
    {
        LogQuery(command.CommandText);
        return base.ReaderExecuting(command, eventData, result);
    }

    public override InterceptionResult<object> ScalarExecuting(
        DbCommand command,
        CommandEventData eventData,
        InterceptionResult<object> result)
    {
        LogQuery(command.CommandText);
        return base.ScalarExecuting(command, eventData, result);
    }

    public override InterceptionResult<int> NonQueryExecuting(
        DbCommand command,
        CommandEventData eventData,
        InterceptionResult<int> result)
    {
        LogQuery(command.CommandText);
        return base.NonQueryExecuting(command, eventData, result);
    }

    private void LogQuery(string query)
    {
        // You can log the query here using your preferred logging mechanism
        Console.WriteLine($"Executing query: {query}");
        Debug.WriteLine($"Executing query: {query}");
    }
}
