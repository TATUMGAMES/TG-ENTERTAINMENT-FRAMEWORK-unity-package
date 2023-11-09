using UnityEngine;
using MikrosClient;
using Newtonsoft.Json;
using System;

namespace EntertainmentFramework.Api
{
    [Serializable]
    public class ConfigDataResponseFormat
    {
        [JsonProperty(PropertyName = "status")]
        public Status status;
        [JsonProperty(PropertyName = "data")]
        public ConfigClass data;
    }
    [Serializable]
    public class ConfigData
    {
        [JsonProperty(PropertyName = "latestAppVersion")]
        public string latestAppVersion;
        [JsonProperty(PropertyName = "previousAppVersion")]
        public string previousAppVersion;
        [JsonProperty(PropertyName = "isForceUpdate")]
        public string isForceUpdate;
    }

    [Serializable]
    public class ConfigClass
    {
        [JsonProperty(PropertyName = "config")]
        public ConfigData config;
    }

    public class Status
    {
        [JsonProperty(PropertyName = "statusCode")]
        public int statusCode;
        [JsonProperty(PropertyName = "statusMessage")]
        public string statusMessage;
    }

    public class ConfigDataClass
    {
        public string packageName = Application.identifier;
        public string appGameId = MikrosManager.Instance.ConfigurationController.MikrosSettings.AppGameID;
    }
}