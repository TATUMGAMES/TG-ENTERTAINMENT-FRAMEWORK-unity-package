using EntertainmentFramework.InternalLoggers;
using System;
using System.Collections;
using System.Collections.Generic;

namespace EntertainmentFramework
{
    public static class GenericExtension
    {
        public static Dictionary<string, object> MapToDictionary(this object source)
        {
            var dictionary = new Dictionary<string, object>();
            MapToDictionaryInternal(dictionary, source);           
            return dictionary;
        }

        private static void MapToDictionaryInternal(
        Dictionary<string, object> sourceDictionary, object source)
        {
            var properties = source.GetType().GetProperties();
            foreach (var p in properties)
            {
                var key = p.Name;
                object value = p.GetValue(source, null);
                Type valueType = value.GetType();

                if (valueType.IsPrimitive || valueType == typeof(string))
                {
                    sourceDictionary[key] = value;
                }
                else if (value is IEnumerable)
                {
                    List<object> list = new List<object>();
                    foreach (object o in (IEnumerable)value)
                    {
                        AddItemToList(list, o);
                    }
                    sourceDictionary[key] = list;
                }
                else if (value is object)
                {
                    sourceDictionary[key] = ToDictionary(new Dictionary<string, object>(), value);
                }
            }
        }

        private static Dictionary<string, object> ToDictionary(Dictionary<string, object> dictionary, object source)
        {
            var properties = source.GetType().GetProperties();
            foreach (var p in properties)
            {
                var key = p.Name;
                object value = p.GetValue(source, null);
                Type valueType = value == null ? typeof(string) : value.GetType();
                if (valueType.IsPrimitive || valueType == typeof(string))
                {
                    dictionary[key] = value;
                }
                else if (value is IEnumerable)
                {
                    List<object> list = new List<object>();
                    foreach (object o in (IEnumerable)value)
                    {
                        AddItemToList(list, o);
                    }
                    dictionary[key] = list;
                }
                else
                {
                    dictionary[key] = ToDictionary(new Dictionary<string, object>(), value);
                }
            }
            return dictionary;
        }

        /// <summary>
        /// Add the object to ther list of type object.
        /// </summary>
        /// <param name="objectToAdd"></param>
        /// <param name="source"></param>
        private static void AddItemToList(List<object> objectToAdd, object source)
        {
            Type valueType = source.GetType();

            if (valueType.IsPrimitive || valueType == typeof(string))
            {
                objectToAdd.Add(source);
            }
            else if (source is IEnumerable)
            {
                List<object> list = new List<object>();
                var i = 0;
                foreach (object o in (IEnumerable)source)
                {
                    AddItemToList(list, o);
                    i++;
                }
                objectToAdd.Add(list);
            }
            else
            {
                objectToAdd.Add(ToDictionary(new Dictionary<string, object>(), source));
            }
        }
    }
}