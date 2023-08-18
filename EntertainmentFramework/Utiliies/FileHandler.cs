using Newtonsoft.Json;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace EntertainmentFramework.Utilities
{
    public class FileHandler : MonoBehaviour
    {
        public static string FileStorageBasePath = Application.persistentDataPath + "/";

        /// <summary>
        /// Reads a object from file in specified path
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filePath"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static bool ReadFromFile<T>(string filePath, out T data)
        {
            data = default;
            string targetFileLocation = FileStorageBasePath + filePath;
            if (File.Exists(targetFileLocation))
            {
                BinaryFormatter targetBinaryFormatter = new BinaryFormatter();
                FileStream targetFile = File.Open(targetFileLocation, FileMode.Open);
                string targetReadString = (string)targetBinaryFormatter.Deserialize(targetFile);
                targetFile.Close();
                data = JsonConvert.DeserializeObject<T>(targetReadString);
                return true;
            }
            else
                return false;
        }

        /// <summary>
        /// Reads a string from file in specified path
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static bool ReadFromFile(string filePath, out string data)
        {
            data = default;
            string targetFileLocation = FileStorageBasePath + filePath;
            if (File.Exists(targetFileLocation))
            {
                BinaryFormatter targetBinaryFormatter = new BinaryFormatter();
                FileStream targetFile = File.Open(targetFileLocation, FileMode.Open);
                data = (string)targetBinaryFormatter.Deserialize(targetFile);
                targetFile.Close();
                return true;
            }
            else
                return false;
        }

        /// <summary>
        /// Saves a file with given string content to specified path
        /// </summary>
        /// <param name="data"></param>
        /// <param name="filePath"></param>
        /// <param name="fileMode"></param>
        public static void SaveToFile(string data, string filePath, FileMode fileMode = FileMode.OpenOrCreate)
        {
            string targetFileLocation = FileStorageBasePath + filePath;
            BinaryFormatter targetBinaryFormatter = new BinaryFormatter();
            FileStream targetFile = File.Open(targetFileLocation, fileMode);
            targetBinaryFormatter.Serialize(targetFile, data);
            targetFile.Close();
        }

        /// <summary>
        /// Saves a file with given object content to specified path
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <param name="filePath"></param>
        /// <param name="fileMode"></param>
        public static void SaveToFile<T>(T data, string filePath, FileMode fileMode = FileMode.OpenOrCreate)
        {
            string targetFileLocation = FileStorageBasePath + filePath;
            BinaryFormatter targetBinaryFormatter = new BinaryFormatter();
            FileStream targetFile = File.Create(targetFileLocation);
            targetBinaryFormatter.Serialize(targetFile, JsonConvert.SerializeObject(data));

            targetFile.Close();
        }

        /// <summary>
        /// Delete a file from specified path
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static bool DeleteSaveFile(string filePath)
        {
            string targetFileLocation = FileStorageBasePath + filePath;
            try
            {
                File.Delete(targetFileLocation);
                return true;
            }
            catch (System.Exception ex)
            {
                Debug.LogException(ex);
                return false;
            }
        }

        /// <summary>
        /// Saves a key with given string content to player prefs
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public static void SaveToPlayerPrefs(string key, string value)
        {
            PlayerPrefs.SetString(key, value);
        }

        /// <summary>
        /// Reads a string from key in player prefs
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static bool ReadFromPlayerPrefs(string key, out string value, string defaultValue = "")
        {
            value = string.Empty;
            if (PlayerPrefs.HasKey(key))
            {
                value = PlayerPrefs.GetString(key, defaultValue);
                return true;
            }
            else
                return false;
        }

        /// <summary>
        /// Delete a key from player prefs
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool DeletePlayerPrefsKey(string key)
        {
            if (PlayerPrefs.HasKey(key))
            {
                PlayerPrefs.DeleteKey(key);
                return true;
            }
            else
                return false;
        }

        /// <summary>
        /// Delete all keys from player prefs
        /// </summary>
        public static void ClearPlayerPrefs()
        {
            PlayerPrefs.DeleteAll();
        }
    }
}