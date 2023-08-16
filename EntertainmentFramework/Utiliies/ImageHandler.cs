using EntertainmentFramework.InternalLoggers;
using System;
using System.IO;
using UnityEngine;

namespace EntertainmentFramework.Utilities
{
    public class ImageHandler : MonoBehaviour
    {
        /// <summary>
        /// Save image data
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="imageBytes"></param>
        public static void SaveImage(string filePath, byte[] imageBytes)
        {
            string targetFileLocation = Application.persistentDataPath + "/" + filePath;
            if (!Directory.Exists(Path.GetDirectoryName(targetFileLocation)))
                Directory.CreateDirectory(Path.GetDirectoryName(targetFileLocation));

            File.WriteAllBytes(filePath, imageBytes);
        }

        /// <summary>
        /// Get image data in Texture2D
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="a_Texture2D"></param>
        /// <returns></returns>
        public static bool GetImageTexture2D(string filePath, out Texture2D a_Texture2D)
        {
            string targetFileLocation = Application.persistentDataPath + "/" + filePath;

            a_Texture2D = new Texture2D(2, 2);
            if (!Directory.Exists(Path.GetDirectoryName(targetFileLocation)) || !File.Exists(targetFileLocation))
            {
                InternalLogger.LogError("File does not exist!");
                return false;
            }
            else
            {
                GetImageBytes(targetFileLocation, out byte[] t_DataByte);
                a_Texture2D.LoadImage(t_DataByte);
                return true;
            }
        }

        /// <summary>
        /// Get image data in bytes
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="imageBytes"></param>
        /// <returns></returns>
        public static bool GetImageBytes(string filePath, out byte[] imageBytes)
        {
            string targetFileLocation = Application.persistentDataPath + "/" + filePath;

            imageBytes = default;
            if (!Directory.Exists(Path.GetDirectoryName(targetFileLocation)) || !File.Exists(targetFileLocation))
            {
                InternalLogger.LogError("File does not exist!");
                return false;
            }
            else
            {
                imageBytes = File.ReadAllBytes(targetFileLocation);
                return true;
            }
        }

        /// <summary>
        /// Convert string to Texture2D
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static Texture2D StringToTexture2D(string data)
        {
            byte[] targetImageBytes = Convert.FromBase64String(data);
            Texture2D t_Tex = new Texture2D(2, 2);
            t_Tex.LoadImage(targetImageBytes);
            return t_Tex;
        }

        /// <summary>
        /// Convert bytes to Texture2D
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static Texture2D BytesToTexture2D(byte[] data)
        {
            byte[] target_ImageBytes = data;
            Texture2D t_Tex = new Texture2D(2, 2);
            t_Tex.LoadImage(target_ImageBytes);
            return t_Tex;
        }
    }
}