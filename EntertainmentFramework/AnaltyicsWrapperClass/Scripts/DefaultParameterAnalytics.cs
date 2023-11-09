#if MIKROS_ADDED
using Entertainment.Utility;
using EntertainmentFramework;
using MikrosClient;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

[Serializable]
internal sealed class DefaultParameterAnalytics
{
    #region Request Properties
    
    [JsonProperty(PropertyName = "appVersion")]
    public string appVersion = Application.version;

    [JsonProperty(PropertyName = "sdkVersion")]
    public string sdkVersion =  "1.4.0";// Constants.SDKVersion; //TODO need to check and load data from MIKROS Package.

    [JsonProperty(PropertyName = "Timestamp")]
    public string timestamp = UtilsClass.CurrentLocalTime();

    #endregion Request Properties
}
#endif