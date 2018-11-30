
using Lily.Framework.Plugin.Module;
using Unity;

namespace Samples.Lily.Framework.ConsoleNetHost
{
    public class MainModule: ModuleOfBase<IUnityContainer>
    {
        protected override IUnityContainer InitDefanltContainer()
        {
            return new UnityContainer();
        }
    }
}
