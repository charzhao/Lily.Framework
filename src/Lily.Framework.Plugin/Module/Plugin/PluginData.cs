using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Lily.Framework.Plugin.Module.Plugin
{
    public class PluginData:ModuleData
    {

        public string DescriptorFilePath { get; set; }

        public PluginDescriptor PluginDescriptor { get; set; }

    }
}
