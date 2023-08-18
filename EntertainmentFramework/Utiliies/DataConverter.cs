using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Collections.Generic;
using System.Linq;

namespace EntertainmentFramework.Utilities
{
    public static class DataConverter
    {
        private static readonly DefaultContractResolver contractResolver = new DefaultContractResolver
        {
            NamingStrategy = new SnakeCaseNamingStrategy()
        };

        private static readonly JsonSerializerSettings serializerSettings = new JsonSerializerSettings()
        {
            ContractResolver = contractResolver,
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            TypeNameHandling = TypeNameHandling.All,
            StringEscapeHandling = StringEscapeHandling.EscapeHtml,
        };

        private static bool customConvertersAdded = false;

        /// <summary>
        /// Function to SerializeObject
        /// </summary>
        /// <typeparam name="T">where T is generic</typeparam>
        /// <param name="dataObj"></param>
        /// <returns> strings</returns>
        public static string SerializeObject<T>(T dataObj)
        {
            return JsonConvert.SerializeObject(dataObj, Formatting.None);
        }

        /// <summary>
        /// Function to DeserializeObject
        /// </summary>
        /// <typeparam name="T">where T is generic</typeparam>
        /// <param name="jsonObject"></param>
        /// <returns>Object T</returns>
        public static T DeserializeObject<T>(string jsonObject)
        {
            return JsonConvert.DeserializeObject<T>(jsonObject);
        }

        /// <summary>
        /// Function to Deserialize Anonymous Object
        /// </summary>
        /// <typeparam name="T">where T is generic</typeparam>
        /// <param name="jsonObject"></param>
        /// <param name="anonymousTypeObject"></param>
        /// <returns>Object T</returns>
        public static T DeserializeAnonymousType<T>(string jsonObject, T anonymousTypeObject)
        {
            return JsonConvert.DeserializeAnonymousType<T>(jsonObject, anonymousTypeObject, serializerSettings);
        }

        /// <summary>
        /// Function to merge two dictionaries of string and object type.
        /// </summary>
        /// <param name="mainDictionary">Main Dictionary in which the other the dictionary will be merged.</param>
        /// <param name="secondaryDictionary">Secondary Dictionary which will be merged in the main dictionary.</param>
        /// <returns></returns>
        public static Dictionary<string, object> MergedDictionaryObject(Dictionary<string, object> mainDictionary, Dictionary<string, object> secondaryDictionary)
        {
            var mergedDictionary = mainDictionary.Concat(secondaryDictionary)
            .ToLookup(x => x.Key, x => x.Value)
            .ToDictionary(x => x.Key, g => g.First());
            return mergedDictionary;
        }
    }
}