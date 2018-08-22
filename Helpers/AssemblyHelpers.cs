using System;
using System.IO;
using System.Reflection;

namespace Helpers
{
    /// <summary>
    /// Contains various methods related to getting information and embeded resources out of the calling or specified assembly.
    /// </summary>
    public class AssemblyHelpers
    {
        /// <summary>
        /// Gets the embedded resource by name from the calling assembly and writes it into the file path specified.
        /// </summary>
        /// <param name="resourceName">Name of the resource being requested.</param>
        /// <param name="fileName">Full path and filename to write the resource to.</param>
        public static void WriteResourceToFile(string resourceName, string fileName)
        {
            using (var resource = Assembly.GetCallingAssembly().GetManifestResourceStream(resourceName))
            {
                using (var file = new FileStream(fileName, FileMode.Create, FileAccess.Write))
                {
                    resource.CopyTo(file);
                }
            }
        }
        /// <summary>
        /// Gets the embedded resource by name from the specified assembly and writes it into the file path specified.
        /// </summary>
        /// <param name="resourceName">Name of the resource being requested.</param>
        /// <param name="assembly">Type object for the assembly being queried.</param>
        /// <param name="fileName">Full path and filename to write the resource to.</param>
        public static void WriteResourceToFile(string resourceName, Type assembly, string fileName)
        {
            using (var resource = Assembly.GetAssembly(assembly).GetManifestResourceStream(resourceName))
            {
                using (var file = new FileStream(fileName, FileMode.Create, FileAccess.Write))
                {
                    resource.CopyTo(file);
                }
            }
        }
        /// <summary>
        /// Gets the embedded resource by name from the calling assembly and returns it as a stream.
        /// </summary>
        /// <param name="resourceName">Name of the resource being requested.</param>
        /// <returns>Returns system IO stream of the resource object.</returns>
        public static Stream GetResourceStream(string resourceName)
        {
            return Assembly.GetCallingAssembly().GetManifestResourceStream(resourceName);
        }
        /// <summary>
        /// Gets the embedded resource by name from the calling assembly and returns it as a string.
        /// </summary>
        /// <param name="resourceName">Name of the resource being requested.</param>
        /// <returns>Returns string value of the resource object.</returns>
        public static string GetResourceString(string resourceName)
        {
            string res = "";
            using (Stream stream = GetResourceStream(resourceName))
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    res = reader.ReadToEnd();
                }
            }
            return res;
        }
        /// <summary>
        /// Gets the version information for the calling assembly.
        /// </summary>
        /// <returns></returns>
        public static string GetVersion()
        {
            var assembly = Assembly.GetCallingAssembly().GetName();
            return $"{assembly.Version.Major}.{assembly.Version.Minor}.{assembly.Version.Build}.{assembly.Version.Revision}";
        }
    }
}
