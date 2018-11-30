using Lily.Framework.Plugin.Module.Plugin;
using Samples.Lily.Framework.CommonContract;
using Unity;

namespace Samples.Lily.Framework.Plugin.ClassLibraryStandard
{
    public class Plugin: ModuleOfPluginBase<IUnityContainer>
    {
        protected override IUnityContainer InitDefanltContainer()
        {
            return new UnityContainer();
        }

        public override void Use()
        {
            IocContainerT.RegisterSingleton<ITest2, ClassLibraryStandard>();
            base.Use();
        }
    }
}
