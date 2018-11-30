using System;
using System.IO;

namespace Lily.Framework.Plugin.Module.Plugin
{
    public static class PluginDataExtensions
    {
        public static string GetPluginDllPath(this PluginData pluginData)
        {
            if (pluginData.PluginDescriptor == null)
            {
                throw new ArgumentException("PluginDescriptor can not be null", nameof(PluginDescriptor));
            }

            if (string.IsNullOrEmpty(pluginData.DescriptorFilePath) || pluginData.PluginDescriptor == null)
            {
                throw new ArgumentException("DescriptorFilePath is not valid", nameof(pluginData.DescriptorFilePath));
            }

            var pluginDirectory = Path.GetDirectoryName(pluginData.DescriptorFilePath);
            if (pluginDirectory == null)
            {
                throw new ArgumentException(nameof(pluginData.DescriptorFilePath));
            }
            return Path.Combine(pluginDirectory, pluginData.PluginDescriptor.PluginAssemblyFileName);
        }
    }
}