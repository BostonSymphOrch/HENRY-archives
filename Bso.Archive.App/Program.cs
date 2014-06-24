using Bso.Archive.BusObj;
using System;

namespace Bso.Archive.App
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(DateTime.Now);
            var opasData = new ImportOPASData();
            opasData.Import();
            Console.WriteLine(DateTime.Now);
            Console.Read();
        }
    }
}
