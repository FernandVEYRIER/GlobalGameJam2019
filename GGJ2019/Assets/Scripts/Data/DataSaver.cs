using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace Assets.Scripts.Data
{
    /// <summary>
    /// Handles the data persistence.
    /// </summary>
    public static class DataSaver
    {
        private const string savePath = "/save.dat";

        private static Dictionary<string, object> _data = new Dictionary<string, object>();

        static DataSaver()
        {
            LoadData();
        }

        private static void LoadData()
        {
            if (File.Exists(Application.persistentDataPath + savePath))
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Open(Application.persistentDataPath + savePath, FileMode.Open);
                if (file.Length <= 0)
                {
                    file.Close();
                    SaveData();
                }
                else
                {
                    _data = (Dictionary<string, object>)bf.Deserialize(file);
                }

                file.Close();
                file.Dispose();
            }
            else
            {
                File.Create(Application.persistentDataPath + savePath);
                SaveData();
            }
        }

        public static void SaveData()
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Create(Application.persistentDataPath + savePath);
            bf.Serialize(file, _data);
            file.Close();
            file.Dispose();
        }

        public static void SetValue<T>(string key, T value)
        {
            if (_data.ContainsKey(key))
                _data[key] = value;
            else
                _data.Add(key, value);
        }

        public static T GetValue<T>(string key)
        {
            if (!_data.ContainsKey(key))
            {
                _data.Add(key, default(T));
            }
            return (T)_data[key];
        }
    }
}