using System;
using System.Threading.Tasks;

namespace Workshop.WebJobs
{
    internal class Program
    {
        private static void Main()
        {
            MainAsync().GetAwaiter().GetResult();
        }

        private static async Task MainAsync()
        {
            while (true)
            {
                Console.WriteLine($"Current date is {DateTime.Now}");
                await Task.Delay(100);
            }
        }
    }
}
