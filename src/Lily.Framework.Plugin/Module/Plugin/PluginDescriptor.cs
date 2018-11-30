using Newtonsoft.Json;

namespace Lily.Framework.Plugin.Module.Plugin
{
    public class PluginDescriptor
    {
        /// <summary>
        /// Gets or sets the name of the assembly file
        /// </summary>
        [JsonProperty(PropertyName = "PluginAssemblyFileName")]
        public string PluginAssemblyFileName { get; set; }
    }
}
