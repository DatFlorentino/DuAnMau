using System.IO;
using UnityEngine;

namespace DefaultNameSpace
    {
        public class StorageManager1
        {
            public static bool SaveToFile(string fileName, string json)
            {
                try
                {
                    var fileStream = new FileStream(fileName, FileMode.Create);
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
        public static string LoadFromFile(string fileName)
        {
            try
            {
                if (File.Exists(fileName))
                {
                    var fileStream = new FileStream(fileName, FileMode.Open);
                    using (var reader = new StreamReader(fileStream))
                    {
                        return reader.ReadToEnd();
                    }
                }
                else
                {
                    Debug.Log("File not found: " + fileName);
                    return null;
                }
            }
            catch (System.IO.IOException e)
            {
                Debug.Log("Error Loading file: " + e.Message);
                return null;
            }
        }
    }
    }