using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Helpers
{
    public class FileHelpers
    {
        /// <summary>
        /// Gets the MD5 checksum for the file specified.
        /// </summary>
        /// <param name="filePath">Full path to the file</param>
        /// <returns>Returns MD5 checksum (hash) for the file</returns>
        public static string HashFile(string filePath)
        {
            using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                return HashFile(fs);
            }
        }
        /// <summary>
        /// Gets the MD5 checksum for the file stream specified.
        /// </summary>
        /// <param name="stream">Stream that you want the hash for</param>
        /// <returns>Returns MD5 checksum (hash) for the file</returns>
        public static string HashFile(Stream stream)
        {
            using (var md5 = new MD5CryptoServiceProvider())
            {
                var buffer = md5.ComputeHash(stream);
                var sb = new StringBuilder();

                for (int i = 0; i < buffer.Length; i++)
                {
                    sb.Append(buffer[i].ToString("x2"));
                }
                return sb.ToString();
            }
        }
    }
}
