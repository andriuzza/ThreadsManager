using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThreadsManager.Contract.Models;

namespace ThreadsManager.Contract.Interfaces
{
    public interface IThreadsHelper
    {
        int GetRandomTime(double minimum, double maximum);
        string RandomString();
        ThreadInformation GetPairOfIdAndSequence();
    }
}
