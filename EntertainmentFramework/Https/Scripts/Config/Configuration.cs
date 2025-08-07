using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace EntertainmentFramework.Api
{
    [CreateAssetMenu(menuName = "Api Config")]
    public class Configuration : ScriptableObject
    {
        public ApiConfiguration Api = new ApiConfiguration();

        [Serializable]
        public class ApiConfiguration
        {
            public string Host = "localhost";
        }
    }
}