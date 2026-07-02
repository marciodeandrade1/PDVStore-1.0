using System;
using System.Data.Common;
using System.Linq;
namespace PDVStore.Helpers
{
    public static class ConnectionHelper
    {
        public static string GetConnectionString()
        {
            // Default localdb instance - change if using SQL Server/Express
            return @"Server=(localdb)\MSSQLLocalDB;Database=PDV_StoreDB;Trusted_Connection=True;";
        }

        // Returns a human-readable breakdown of the connection string components.
        public static string GetReadableConnectionStringInfo()
        {
            var cs = GetConnectionString();
            try
            {
                var builder = new DbConnectionStringBuilder { ConnectionString = cs };
                return string.Join(Environment.NewLine,
                    builder.Keys.Cast<string>().Select(k => $"{k}: {builder[k]}"));
            }
            catch
            {
                return "Invalid connection string: " + cs;
            }
        }
    }
}
