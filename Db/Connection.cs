using System;

namespace Db
{
    public class Connection
    {

        private static readonly string server = "DESKTOP-MHUELB2\\SQLEXPRESS";
        private static readonly string database = "SoNFinanceiro";
        private static readonly string user = "sa";
        private static readonly string password = "dev123";


        public static string GetStringConnection() =>
            $"Server={server};Database={database};User Id={user};Password={password}";
    }
}
