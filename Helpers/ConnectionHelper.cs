using System;
using System.Collections.Generic;
using System.Text;

namespace PDVStore.Helpers
{
    public class ConnectionHelper
    {
        public static string GetConnectionString()
        {
            // Return your connection string here
            return @"Server=(localdb)\\mssqllocaldb;Database=PDV_StoreDB;Trusted_Connection=True;";
        }
    }
}
