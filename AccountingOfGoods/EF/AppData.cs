using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingOfGoods.EF
{
    class AppData
    {
        public static Entities Context { get; } = new Entities();
        public static void ResetIdentity(string tableName)
        {
            string commandText = $"DBCC CHECKIDENT ('{tableName}', RESEED, 0)";
            Context.Database.ExecuteSqlCommand(commandText);
        }
    }
}
