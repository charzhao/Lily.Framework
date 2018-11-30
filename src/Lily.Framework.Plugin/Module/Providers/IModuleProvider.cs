using System.Collections.Generic;

namespace Lily.Framework.Plugin.Module.Providers
{
    public interface IModuleProvider
    {
         IList<IList<IModule>> AllModuleList { get; }
        void Excute();
    }
}
