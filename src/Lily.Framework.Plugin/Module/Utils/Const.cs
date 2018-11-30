using System;
using System.Collections.Generic;
using System.Text;

namespace Lily.Framework.Plugin.Module
{
    public class Const
    {

        /// <summary>
        /// Gets the plugins folder name
        /// </summary>
        public static string PluginRelativePath => "Plugins";

        /// <summary>
        /// Gets the path to plugins shadow copies folder
        /// </summary>
        public static string PluginShadowCopyRelativePath => "Plugins/bin";

        /// <summary>
        /// Gets the name of the plugin description file
        /// </summary>
        public static string PluginDescriptionFileName => "*.plugin.json";
    }
}
