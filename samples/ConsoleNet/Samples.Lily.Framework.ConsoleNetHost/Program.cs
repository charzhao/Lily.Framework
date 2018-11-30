using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lily.Framework.Plugin.Module;
using Lily.Framework.Plugin.Module.Providers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Samples.Lily.Framework.CommonContract;
using Unity;

namespace Samples.Lily.Framework.ConsoleNetHost
{
    class Program
    {
        static void Main(string[] args)
        {
            IUnityContainer container = new UnityContainer();

            var moduleFacade = ModuleFacade.Create(new ModuleProviderOfConsole<MainModule>(container));
            moduleFacade.InitAllModules();
            
            var plugins = moduleFacade.GetAllPlugins();
            var modules = moduleFacade.GetAllModules();

            var test1 = container.Resolve<ITest1>();
            var test2 = container.Resolve <ITest2>();
            Console.ReadLine();
        }

    }
}
