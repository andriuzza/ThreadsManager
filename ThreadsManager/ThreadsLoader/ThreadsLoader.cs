using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ThreadsManager.Contract.Interfaces;
using ThreadsManager.Contract.Models;
using ThreadsManager.Helper;

namespace ThreadsManager.ThreadsLoader
{
    public class ThreadsLoader : IThreadsHelper
    {
        public int Number { get; set; }
        public string ExceptionMessage { get; private set; }

        private static Random Random = new Random();
        private const string Chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

        public ThreadsLoader()
        {
            Random = new Random();
        }

        public ThreadsLoader InitiLoader(string input)
        {
            CheckForInput(input);

            return null;
        }

        private void CheckForInput(string input)
        {
            try
            {
                Number = input.ValidateInput();
            }
            catch (ArgumentException ex)
            {
                ExceptionMessage = ex.ToString();
            }
        }

        public ThreadInformation GetPairOfIdAndSequence()
        {
            var intervalNumber = GetRandomTime(0.5, 2);
            Thread.Sleep(intervalNumber);
            var sequence = RandomString();
            var threadId = Thread.CurrentThread.ManagedThreadId;

            return new ThreadInformation
            {
                ThreadId = threadId,
                Sequence = sequence,
                DateTime = DateTime.Now
            };
        }

        public int GetRandomTime(double minimum, double maximum)
        {
            var random = new Random();
            var number = random.NextDouble() * (maximum - minimum) + minimum;
            return Convert.ToInt32(number * 1000);

        }

        public string RandomString()
        {

            var randomLength = Random.Next(5, 10);

            return new string(Enumerable.Repeat(Chars, randomLength)
                .Select(s => s[Random.Next(s.Length)]).ToArray());
        }
    }
}
