using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Managers.DataManagment;
using UnityEngine;

namespace Managers.Data_Management
{
    public class FileHandler : MonoBehaviour
    {
        public static void SaveToJson<T>(List<T> toSave, string filename)
        {
            Debug.Log(GetPath(filename));
            var content = JsonHelper.ToJson<T>(toSave.ToArray());
            WriteFile(GetPath(filename), content);
        }

        public static void SaveToJson<T>(T toSave, string filename)
        {
            var content = JsonUtility.ToJson(toSave);
            WriteFile(GetPath(filename), content);
        }

        public static List<T> ReadListFromJson<T>(string filename)
        {
            var content = ReadFile(GetPath(filename));

            if (string.IsNullOrEmpty(content) || content == "{}")
            {
                return new List<T>();
            }

            var res = JsonHelper.FromJson<T>(content).ToList();

            return res;

        }

        public static T ReadFromJson<T>(string filename)
        {
            string content = ReadFile(GetPath(filename));

            if (string.IsNullOrEmpty(content) || content == "{}")
            {
                return default(T);
            }

            T res = JsonUtility.FromJson<T>(content);

            return res;

        }

        private static string GetPath(string filename)
        {
            return Application.dataPath + "/" + filename;
        }

        private static void WriteFile(string path, string content)
        {
            try
            {
                var fileStream = new FileStream(path, FileMode.Create);
            
                StreamWriter writer = new StreamWriter(fileStream);
                writer.Write(content);
            }
            catch (Exception e)
            {
                Debug.Log("Access is denied! Cant save score");
            }
            
        }

        private static string ReadFile(string path)
        {
            if (!File.Exists(path)) return string.Empty;

            var reader = new StreamReader(path);
            var content = reader.ReadToEnd();
            return content;
        }
    }
}