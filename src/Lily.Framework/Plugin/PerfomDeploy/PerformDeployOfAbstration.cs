using Nop.Core;
using Nop.Core.Infrastructure;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace Lily.Framework.Plugin.PerfomDeploy
{
    public class PerformDeployOfAbstration
    {
        /// <summary>
        /// Gets the path to plugins shadow copies folder
        /// </summary>
        public static string ShadowCopyPath => "~/Plugins/bin";
        private const string RESERVE_SHADOW_COPY_FOLDER_NAME = "reserve_bin_";

        protected static readonly INopFileProvider _fileProvider;
        protected static string _shadowCopyFolder;
        protected static string _reserveShadowCopyFolder;

        static PerformDeployOfAbstration()
        {
            _fileProvider = CommonHelper.DefaultFileProvider;
            _shadowCopyFolder = _fileProvider.MapPath(ShadowCopyPath);
            _reserveShadowCopyFolder = _fileProvider.Combine(_fileProvider.MapPath(ShadowCopyPath), $"{RESERVE_SHADOW_COPY_FOLDER_NAME}{DateTime.Now.ToFileTimeUtc()}");
        }
        /// <summary>
        /// Copy the plugin file to shadow copy directory
        /// </summary>
        /// <param name="pluginFilePath">Plugin file path</param>
        /// <param name="shadowCopyPlugFolder">Path to shadow copy folder</param>
        /// <returns>File path to shadow copy of plugin file</returns>
        protected static string ShadowCopyFile(string pluginFilePath, string shadowCopyPlugFolder)
        {
            var shouldCopy = true;
            var shadowCopiedPlug = _fileProvider.Combine(shadowCopyPlugFolder, _fileProvider.GetFileName(pluginFilePath));

            //check if a shadow copied file already exists and if it does, check if it's updated, if not don't copy
            if (_fileProvider.FileExists(shadowCopiedPlug))
            {
                //it's better to use LastWriteTimeUTC, but not all file systems have this property
                //maybe it is better to compare file hash?
                var areFilesIdentical = _fileProvider.GetCreationTime(shadowCopiedPlug).ToUniversalTime().Ticks >= _fileProvider.GetCreationTime(pluginFilePath).ToUniversalTime().Ticks;
                if (areFilesIdentical)
                {
                    Debug.WriteLine("Not copying; files appear identical: '{0}'", _fileProvider.GetFileName(shadowCopiedPlug));
                    shouldCopy = false;
                }
                else
                {
                    //delete an existing file

                    //More info: https://www.nopcommerce.com/boards/t/11511/access-error-nopplugindiscountrulesbillingcountrydll.aspx?p=4#60838
                    Debug.WriteLine("New plugin found; Deleting the old file: '{0}'", _fileProvider.GetFileName(shadowCopiedPlug));
                    _fileProvider.DeleteFile(shadowCopiedPlug);
                }
            }

            if (!shouldCopy)
                return shadowCopiedPlug;

            try
            {
                _fileProvider.FileCopy(pluginFilePath, shadowCopiedPlug, true);
            }
            catch (IOException)
            {
                Debug.WriteLine(shadowCopiedPlug + " is locked, attempting to rename");
                //this occurs when the files are locked,
                //for some reason devenv locks plugin files some times and for another crazy reason you are allowed to rename them
                //which releases the lock, so that it what we are doing here, once it's renamed, we can re-shadow copy
                try
                {
                    var oldFile = shadowCopiedPlug + Guid.NewGuid().ToString("N") + ".old";
                    _fileProvider.FileMove(shadowCopiedPlug, oldFile);
                }
                catch (IOException exc)
                {
                    throw new IOException(shadowCopiedPlug + " rename failed, cannot initialize plugin", exc);
                }
                //OK, we've made it this far, now retry the shadow copy
                _fileProvider.FileCopy(pluginFilePath, shadowCopiedPlug, true);
            }

            return shadowCopiedPlug;
        }

    }
}
