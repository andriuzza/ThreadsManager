using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThreadsManager.Contract.Models;

namespace ThreadsManager.Contract.Interfaces
{
    public interface IDatabaseManager
    {
        void InitializeDatabase();
        string InsertInformationToDb(ThreadInformation data);
        void OpenConnection();
        void CloseConnection();
    }
}
