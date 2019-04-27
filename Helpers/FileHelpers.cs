using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Xml.Serialization;

namespace Helpers
{
    /// <summary>
    /// Contains methods related to the reading or manipulation of files and/or folders on a drive.
    /// </summary>
    public class FileHelpers
    {
        /// <summary>
        /// Checks to see if a given path is referencing a directory or a file.
        /// </summary>
        /// <param name="path">Full path to the object.</param>
        /// <returns>Returns true if the object in the path is a folder, false if the object is a file, and null if the path is invalid.</returns>
        public static bool? IsDirectory(string path)
        {
            if (Directory.Exists(path)) return true; // is a directory 
            else if (File.Exists(path)) return false; // is a file 
            else return null; // is a nothing 
        }

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

        /// <summary>
        /// Gets all the files in a directory and returns them in the form of a FileInfo list collection.
        /// </summary>
        /// <param name="directoryPath">Full path to the directory.</param>
        /// <param name="searchPattern">Optional seach pattern to be used.</param>
        /// <returns>List collection of FileInfo objects for all the files found.</returns>
        public static List<FileInfo> GetFileInfoList(string directoryPath, string searchPattern = "*.*")
        {
            if (!Directory.Exists(directoryPath)) throw new DirectoryNotFoundException();
            List<FileInfo> fileInfos = new List<FileInfo>();

            string[] files = Directory.GetFiles(directoryPath, searchPattern);
            foreach(string file in files)
            {
                fileInfos.Add(new FileInfo(file));
            }
            return fileInfos;
        }
        
        /// <summary>
        /// Deserialize a file with XML contents into a .net object.
        /// </summary>
        /// <param name="filePath">Full path to the file containing valid XML.</param>
        /// <param name="objType">Type class for the object being deserialized.</param>
        /// <returns>Object deserialized (will need to be cast).</returns>
        public static object DeserializeObject(string filePath, Type objType)
        {
            object obj = null;

            XmlSerializer serializer = new XmlSerializer(objType);

            using (FileStream fileStream = new FileStream(filePath, FileMode.Open))
            {
                obj = serializer.Deserialize(fileStream);
            }
             
            return obj;
        }

        /// <summary>
        /// Serialize an object to a file.
        /// </summary>
        /// <param name="objToSerialize">The object that is to be serialized.</param>
        /// <param name="objType">The Type information for the object.</param>
        /// <param name="filePath">Full path to the file to be written to (will create if non existent).</param>
        public static void SerializeObject(object objToSerialize, Type objType, string filePath)
        {
            XmlSerializer serializer = new XmlSerializer(objType);

            using (FileStream fStream = new FileStream(filePath, FileMode.OpenOrCreate))
            {
                serializer.Serialize(fStream, objToSerialize);
            }
        }

    }
}
