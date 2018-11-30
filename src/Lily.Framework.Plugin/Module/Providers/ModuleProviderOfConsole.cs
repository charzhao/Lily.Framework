using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using Lily.Framework.Plugin.Module.Helpers;
using Lily.Framework.Plugin.Module.Plugin;
using Newtonsoft.Json;

namespace Lily.Framework.Plugin.Module.Providers
{
    public class ModuleProviderOfConsole<TStartModule> : ModuleProviderOfBase<TStartModule>
        where TStartModule : IModule
    {
        #region constructor

        public ModuleProviderOfConsole(object iocContainer) : base(iocContainer){}

        #endregion


        #region override ModuleProviderOfBase

        protected override string GetPluginsBasePath()
        {
            var pluginsBasePath = AppDomain.CurrentDomain.BaseDirectory;
            return pluginsBasePath;
        }
        #endregion

    }
}
