using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThreadsManager.DataSql.DbManager;

namespace ThreadsManager.TableCreate
{
    class Program
    {
        static void Main(string[] args)
        {
            var db = new DbManager();

            db.InitializeDatabase();
        }
    }
}
