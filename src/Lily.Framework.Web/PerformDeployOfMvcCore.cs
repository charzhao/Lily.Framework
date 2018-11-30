using Lily.Framework.Plugin.PerfomDeploy;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Nop.Core.Configuration;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.Loader;
using System.Text;

namespace Lily.Framework.Web
{
    public class PerformDeployOfMvcCore : PerformDeployOfAbstration, IPerformDeploy
    {
        private ApplicationPartManager _applicationPartManager;
        public PerformDeployOfMvcCore(ApplicationPartManager applicationPartManager)
        {
            _applicationPartManager = applicationPartManager;
        }
        /// <summary>
        /// Perform file deploy
        /// </summary>
        /// <param name="plug">Plugin file info</param>
        /// <param name="applicationPartManager">Application part manager</param>
        /// <param name="config">Config</param>
        /// <param name="shadowCopyPath">Shadow copy path</param>
        /// <returns>Assembly</returns>
        public  Assembly PerformFileDeploy(string plug, NopConfig config, string shadowCopyPath = "")
        {
            var parent = string.IsNullOrEmpty(plug) ? string.Empty : _fileProvider.GetParentDirectory(plug);

            if (string.IsNullOrEmpty(parent))
                throw new InvalidOperationException($"The plugin directory for the {_fileProvider.GetFileName(plug)} file exists in a folder outside of the allowed nopCommerce folder hierarchy");

            if (!config.UsePluginsShadowCopy)
                return RegisterPluginDefinition(config, _applicationPartManager, plug);

            //in order to avoid possible issues we still copy libraries into ~/Plugins/bin/ directory
            if (string.IsNullOrEmpty(shadowCopyPath))
                shadowCopyPath = _shadowCopyFolder;

            _fileProvider.CreateDirectory(shadowCopyPath);
            var shadowCopiedPlug = ShadowCopyFile(plug, shadowCopyPath);

            Assembly shadowCopiedAssembly = null;

            try
            {
                shadowCopiedAssembly = RegisterPluginDefinition(config, _applicationPartManager, shadowCopiedPlug);
            }
            catch (FileLoadException)
            {
                if (!config.CopyLockedPluginAssembilesToSubdirectoriesOnStartup || !shadowCopyPath.Equals(_shadowCopyFolder))
                    throw;
            }

            return shadowCopiedAssembly ?? PerformFileDeploy(plug, config, _reserveShadowCopyFolder);
        }

        /// <summary>
        /// Register the plugin definition
        /// </summary>
        /// <param name="config">Config</param>
        /// <param name="applicationPartManager">Application part manager</param>
        /// <param name="plug">Plugin file info</param>
        /// <returns>Assembly</returns>
        private static Assembly RegisterPluginDefinition(NopConfig config, ApplicationPartManager applicationPartManager, string plug)
        {
            //we can now register the plugin definition
            Assembly pluginAssembly;
            try
            {
                pluginAssembly = AssemblyLoadContext.Default.LoadFromAssemblyPath(plug);
            }
            catch (FileLoadException)
            {
                if (config.UseUnsafeLoadAssembly)
                {
                    //if an application has been copied from the web, it is flagged by Windows as being a web application,
                    //even if it resides on the local computer.You can change that designation by changing the file properties,
                    //or you can use the<loadFromRemoteSources> element to grant the assembly full trust.As an alternative,
                    //you can use the UnsafeLoadFrom method to load a local assembly that the operating system has flagged as
                    //having been loaded from the web.
                    //see http://go.microsoft.com/fwlink/?LinkId=155569 for more information.
                    pluginAssembly = Assembly.UnsafeLoadFrom(plug);
                }
                else
                {
                    throw;
                }
            }

            Debug.WriteLine("Adding to ApplicationParts: '{0}'", pluginAssembly.FullName);
            applicationPartManager.ApplicationParts.Add(new AssemblyPart(pluginAssembly));

            return pluginAssembly;
        }

    }
}
