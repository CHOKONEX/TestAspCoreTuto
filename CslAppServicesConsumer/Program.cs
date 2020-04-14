using System;
using CslAppServicesConsumer.Tests.Test1;

namespace CslAppServicesConsumer
{
    static class Program
    {
        private static void Main(string[] args)
        {
            UserService.Run();
            DapperService.Run();
            Console.ReadLine();
        }
    }
}
