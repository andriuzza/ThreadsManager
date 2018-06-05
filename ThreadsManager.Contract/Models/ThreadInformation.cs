using System;

namespace ThreadsManager.Contract.Models
{
    public class ThreadInformation
    {
        public int ThreadId { get; set; }

        public string Sequence { get; set; }

        public DateTime DateTime { get; set; }
    }
}
