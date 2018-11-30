using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Lily.Framework.Plugin.Module.Helpers;
using Lily.Framework.Plugin.Module.Plugin;
using Newtonsoft.Json;

namespace Lily.Framework.Plugin.Module.Providers
{
    public abstract class ModuleProviderOfBase<TStartModule> : IModuleProvider
    {

        protected string PluginsPath
        {
            get
            {
                var pluginsBasePath = GetPluginsBasePath();
                return Path.Combine(pluginsBasePath, Const.PluginRelativePath);
            }
        }

        protected string PluginsShadowCopyPath
        {
            get
            {
                var pluginsBasePath = GetPluginsBasePath();
                return Path.Combine(pluginsBasePath, Const.PluginShadowCopyRelativePath);
            }
        }

        protected readonly object IocContainer;
        protected ModuleProviderOfBase(object iocContainer)
        {
            IocContainer = iocContainer;
        }

        #region common method
        protected IList<IModule> GetOrderedDependOnModuleList(Assembly assembly)
        {
            var moduleList = new List<IModule>();
            var assemblyNameReferenced = assembly.GetReferencedAssemblies();
            foreach (var assemblyName in assemblyNameReferenced)
            {
                var assemblyLoaded = Assembly.Load(assemblyName);
                CollectModuleInstanceFromAssembly(assemblyLoaded, moduleList);
            }
            CollectModuleInstanceFromAssembly(assembly, moduleList);
            return moduleList.OrderByDescending(item => item.OrderIndex).ToList();

            void CollectModuleInstanceFromAssembly(Assembly assemblyTemp, List<IModule> moduleListTemp)
            {
                var types = assemblyTemp.GetTypes();
                foreach (var type in types)
                {

                    if (!typeof(IModule).IsAssignableFrom(type) || type.IsAbstract || type.IsInterface) continue;

                    var instanceModule = (IModule)Activator.CreateInstance(type);
                    instanceModule.IocContainer = IocContainer;
                    var instanceType = instanceModule.GetType();
                    instanceModule.OrderIndex = ModuleDependencyDepthHelper.FindMaxDependencyIndex(instanceType, 0);
                    moduleListTemp.Add(instanceModule);
                }
            }
        }
        #endregion

        #region IModuleProvider

        public IList<IList<IModule>> AllModuleList { get; } = new List<IList<IModule>>();

        public virtual void Excute()
        {
            ModuleInitializeForStartApplication();
            ModuleInitializeForPlugins();
        }


        #endregion

        #region startupModules
        protected virtual void ModuleInitializeForStartApplication()
        {
            var hostAssembly = Assembly.GetAssembly(typeof(TStartModule));
            var moduleListOfReferences = GetOrderedDependOnModuleList(hostAssembly);
            AllModuleList.Add(moduleListOfReferences);
        }
        #endregion


        #region plugins

        protected virtual void ModuleInitializeForPlugins()
        {
            var pluginDatas = LoadPluginsDescriptors();
            LoadPluginDlls(pluginDatas);
            foreach (var pluginData in pluginDatas)
            {
                var moduleListOfPluginRefenced = GetOrderedDependOnModuleList(pluginData.Assembly);
                AllModuleList.Add(moduleListOfPluginRefenced);
            }
        }

        protected virtual string GetPluginsBasePath()
        {
            var pluginsBasePath = AppDomain.CurrentDomain.BaseDirectory;
            return pluginsBasePath;
        }

        protected virtual IList<PluginData> LoadPluginsDescriptors()
        {
            var allDescriptionFilePaths = Directory.GetFiles(PluginsPath, Const.PluginDescriptionFileName, SearchOption.AllDirectories);

            IList<PluginData> pluginDatas = new List<PluginData>();
            foreach (var descriptionFilePath in allDescriptionFilePaths)
            {
                var content = File.ReadAllText(descriptionFilePath, Encoding.UTF8);
                //get plugin descriptor from the JSON file
                var descriptor = JsonConvert.DeserializeObject<PluginDescriptor>(content);
                pluginDatas.Add(new PluginData()
                {
                    DescriptorFilePath = descriptionFilePath,
                    PluginDescriptor = descriptor
                });
            }

            return pluginDatas;
        }

        protected void LoadPluginDlls(IList<PluginData> pluginDatas)
        {
            foreach (var pluginData in pluginDatas)
            {
                var loadedAssembly = Assembly.LoadFrom(pluginData.GetPluginDllPath());
                pluginData.Assembly = loadedAssembly;
            }
        }
        #endregion


    }
}
