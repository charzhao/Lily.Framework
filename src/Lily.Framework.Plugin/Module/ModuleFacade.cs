using System.Collections.Generic;
using Lily.Framework.Plugin.Module.Plugin;
using Lily.Framework.Plugin.Module.Providers;

namespace Lily.Framework.Plugin.Module
{
    public class ModuleFacade
    {
        #region constructor
        private readonly IModuleProvider _moduleProvider;
        private  ModuleFacade(IModuleProvider moduleProvidevider)
        {
            _moduleProvider = moduleProvidevider;
        }


        public static ModuleFacade Create(IModuleProvider moduleProvidevider)
        {
            return new ModuleFacade(moduleProvidevider);
        }
        #endregion

        public void InitAllModules()
        {
            _moduleProvider.Excute();
            foreach (var modules in _moduleProvider.AllModuleList)
            {
                foreach (var module in modules)
                {
                    module.Config();
                }
            }
            foreach (var modules in _moduleProvider.AllModuleList)
            {
                foreach (var module in modules)
                {
                    module.Use();
                }
            }
        }

        public IList<IPlugin> GetAllPlugins()
        {
            IList<IPlugin> plugins = new List<IPlugin>();
            foreach (var modules in _moduleProvider.AllModuleList)
            {
                foreach (var module in modules)
                {
                    if(module is IPlugin item) plugins.Add(item);
                }
            }
            return plugins;
        }

        public IList<IModule> GetAllModules()
        {
            IList<IModule> plugins = new List<IModule>();
            foreach (var modules in _moduleProvider.AllModuleList)
            {
                foreach (var module in modules)
                {
                     plugins.Add(module);
                }
            }
            return plugins;
        }

        public T GetContainerByPlugin<T>(PluginData pluginData)
        {
            return default(T);
        }
    }
}