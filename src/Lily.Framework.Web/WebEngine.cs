
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Nop.Core;
using Nop.Core.Configuration;
using Nop.Core.Infrastructure;
using Nop.Core.Plugins;
using System.Net;

namespace Lily.Framework.Web
{
    public class WebEngine: NopEngine
    {
        public override void Initialize(IServiceCollection services)
        {
            //most of API providers require TLS 1.2 nowadays
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            var provider = services.BuildServiceProvider();
            var hostingEnvironment = provider.GetRequiredService<IHostingEnvironment>();
            CommonHelper.DefaultFileProvider = new NopFileProvider(hostingEnvironment);

            //initialize plugins
            var nopConfig = provider.GetRequiredService<NopConfig>();
            var mvcCoreBuilder = services.AddMvcCore();
            var performDeploy = new PerformDeployOfMvcCore(mvcCoreBuilder.PartManager);
            PluginManager.Initialize(performDeploy, nopConfig);
        }
    }
}
