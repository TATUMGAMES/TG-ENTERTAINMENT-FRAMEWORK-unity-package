using System.Text;

namespace EntertainmentFramework.Extension
{
    public static class RestExtension
    {
        /// <summary>
        /// Functions to GetBytes from the string.
        /// </summary>
        /// <param name="str">string</param>
        /// <returns>byte array</returns>
        public static byte[] GetBytes(this string str)
        {
            return Encoding.ASCII.GetBytes(str);
        }
    }
}