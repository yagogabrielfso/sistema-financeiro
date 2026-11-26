using System;

namespace Db
{
    public class Connection
    {

        private static readonly string server = "localhost\\SQLEXPRESS";
        private static readonly string database = "SoNFinanceiro";
        private static readonly string user = "sa";
        private static readonly string password = "SUA_SENHA";


        public static string GetStringConnection() =>
            $"Server={server};Database={database};User Id={user};Password={password}";
    }
}
