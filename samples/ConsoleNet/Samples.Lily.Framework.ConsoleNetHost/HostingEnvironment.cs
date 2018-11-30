using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;

namespace Samples.Lily.Framework.ConsoleNetHost
{
    public class HostingEnvironment: IHostingEnvironment
    {
        public string EnvironmentName { get; set; }
        public string ApplicationName { get; set; }
        public string ContentRootPath { get; set; }=System.IO.Path.GetDirectoryName(
        System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase).Substring(6);
        public IFileProvider ContentRootFileProvider { get; set; }
    }
}
