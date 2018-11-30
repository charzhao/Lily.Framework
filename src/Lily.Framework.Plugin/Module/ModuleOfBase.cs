using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Text;

namespace Lily.Framework.Plugin.Module
{
    public abstract class ModuleOfBase<T>: IModule
    {
        protected T IocContainerT
        {
            get
            {
                if (IocContainer == null)
                {
                    IocContainer= InitDefanltContainer();
                }

                return (T)IocContainer;
            }
        }

        protected abstract T InitDefanltContainer();

        #region IModule
        public object IocContainer { get; set; }

        public int OrderIndex { get; set; }
        #endregion

        #region IModuleFunc
        public virtual void Config()
        {
            
        }

        public virtual void Use()
        {
           
        }
        #endregion
    }
}
