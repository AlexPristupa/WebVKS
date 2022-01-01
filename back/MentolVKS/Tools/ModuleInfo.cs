using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MentolVKS.Tools
{
    public static class ModuleInfo
    {
        private static string _version;

        public static string Version
        {
            get
            {
                if (string.IsNullOrEmpty(_version))
                {
                    try
                    {
                        var entryAssemply = System.Reflection.Assembly.GetEntryAssembly().GetName();
                        var version = entryAssemply.Version;

                        _version = $"{version.Major}.{version.Minor}.{version.Build}";
                    }
                    catch (Exception)
                    {
                        System.Diagnostics.Debug.WriteLine("Warning! Error getting application version!");
                    }
                }

                return _version;
            }
        }
    }
}
