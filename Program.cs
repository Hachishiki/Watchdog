using System;
        
namespace WatchDog
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 1)
            {
                Watcher watcher = new Watcher();
                watcher.Watch(args[0]);
            }
            else
            {
                Console.WriteLine("Invalid syntax.\n" +
                    "Usage: WatchDog.exe <path>");
            }
        }
    }
}
