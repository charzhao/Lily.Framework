using System;
using Lily.Framework.Modularization;

namespace Samples.Modularization.CoreConsoleHost
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            DefaultAssemblyProvider  provider =new DefaultAssemblyProvider();
           var assemblies= provider.GetAssemblies(@"C:\0\1MyProgram\0DesertFish\Lily.Framework\samples\Modularization\CoreConsole\Samples.Modularization.CoreConsoleHost\bin\Debug\Plugins", true);
            Console.ReadLine();
        }
    }
}
