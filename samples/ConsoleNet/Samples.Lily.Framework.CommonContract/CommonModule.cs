using System;
using Lily.Framework.Plugin.Module;
using Unity;

namespace Samples.Lily.Framework.CommonContract
{
    public class CommonModule:ModuleOfBase<IUnityContainer>
    {
        protected override IUnityContainer InitDefanltContainer()
        {
            return new UnityContainer();
        }
    }
}
