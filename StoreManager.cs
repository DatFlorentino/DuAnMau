using System.Diagnostics;
using System.IO;
using UnityEngine;

namespace DefaultNameSpace
{
    public class StorageManager
    {
        public static bool SaveToFile(string filename, string json)
        {
            try
            {
                var FileStream = new FileStream(filename, FileMode.Create);
                using (var writer = new StreamWriter(fileStream))
                {
                    writer.Write(json);
                }
                return true;
            }
            catch (System.Exception e)
            {
                Debug.Log("Error saving file: " + e.Message);
                return false;
            }
        }
        public static string LoadFromFile(string filename)
        {
            try
            {
                if (File.Exists(filename))
                {
                    var fileStream = new FileStream(filename, FileMode.Open);
                    using (var reader = new StreamReader(fileStream))
                    {
                        return reader.ReadToEnd();
                    }
                }
                else
                {
                    Debug.Log("File loading file: " + filename);
                    return null;
                }
            }
            catch (System.Exception e)
            {
                Debug.Log("Error loading file: " + e.Message);
                return null;
            }
        }
    }
}