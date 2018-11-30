using Nop.Core.Configuration;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Lily.Framework.Plugin.PerfomDeploy
{
    public interface IPerformDeploy
    {
        Assembly PerformFileDeploy(string plug, NopConfig config, string shadowCopyPath = "");
    }
}
