using System;
using System.Collections.Generic;
using System.Text;

namespace Lily.Framework.Plugin.Module
{
   
    public interface IModule
    {
        object IocContainer { get; set; }
        int OrderIndex { get; set; }
        void Config();
        void Use();
    }
}
