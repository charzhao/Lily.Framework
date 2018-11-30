using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Lily.Framework.Plugin.Module.Helpers
{
    internal class ModuleDependencyDepthHelper
    {
        internal static int FindMaxDependencyIndex(Type moduleType, int startIndex)
        {
            var dependsOnAttr = (DependsOnAttribute)moduleType.GetCustomAttribute(typeof(DependsOnAttribute));
            // if is already root, return currentIndex;
            if (dependsOnAttr?.DependedModuleTypes == null
                || dependsOnAttr.DependedModuleTypes.Length == 0)
            {
                return startIndex;
            }

            var maxIndex = 0;
            foreach (var parentModuleType in dependsOnAttr.DependedModuleTypes)
            {
                var tempIndex = FindMaxDependencyIndex(parentModuleType, 1);
                if (tempIndex > maxIndex)
                {
                    maxIndex = tempIndex;
                }
            }
            return maxIndex + startIndex;
        }
    }
}
