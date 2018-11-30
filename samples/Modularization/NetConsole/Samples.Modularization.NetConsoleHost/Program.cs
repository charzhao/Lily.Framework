using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Lily.Framework.Modularization;
using Microsoft.Extensions.DependencyModel;

namespace Samples.Modularization.NetConsoleHost
{
    class Program
    {
        static void Main(string[] args)
        {
            var assemble = Assembly.GetEntryAssembly();
            var dependencyContext = DependencyContext.Load(assemble);
            var aaa= dependencyContext.CompileLibraries;
            DefaultAssemblyProvider provider = new DefaultAssemblyProvider();
            var assemblies = provider.GetAssemblies(@"C:\0\1MyProgram\0DesertFish\Lily.Framework\samples\Modularization\NetConsole\Samples.Modularization.NetConsoleHost\bin\Debug\Plugins", true);
            Console.ReadLine();
        }
    }
}
