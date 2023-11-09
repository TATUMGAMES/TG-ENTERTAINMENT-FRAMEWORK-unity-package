using EntertainmentFramework.Utils.Rest;
using EntertainmentFramework.Utils;
using EntertainmentFramework.Api;
using EntertainmentFramework.InternalLoggers;

using System;
using System.Collections;
using UnityEngine;
using MikrosClient;
using UnityEngine.Events;

namespace EntertainmentFramework.InGameUpdate
{
    public class ForceInGameAppUpdater : MonoBehaviour
    {
        [SerializeField] BuildVersionConfig buildVersionConfig;
        [SerializeField] UnityEvent appVersionCheckingCompleted;
        private string buildVersion;

        private IEnumerator Start()
        {
            yield return new WaitUntil(() => MikrosManager.Instance.IsInitialized);
            SendConfigData(Setdata, onErrorGetDetails);
            buildVersion = buildVersionConfig.versionNumber + "." + buildVersionConfig.versionCode;
        }

        private void OnEnable()
        {
            UpdateAppObjectHandler.OnUpdateLaterAction += OnUpdateLaterButtonCliked;
        }
        private void OnDisable()
        {
            UpdateAppObjectHandler.OnUpdateLaterAction -= OnUpdateLaterButtonCliked;
        }

        private void SendConfigData(Action<ConfigDataResponseFormat> onCompletion, Action<RestUtil.RestCallError> onError)
        {
            ConfigDataClass dataClass = new ConfigDataClass();
            string jsonData = JsonUtility.ToJson(dataClass);
            Debug.Log(dataClass.appGameId);
            var builder = new WebRequestBuilder()
                 .Url(RestManager.FormatApiUrl(Urls.CONFIG))
                 .Data(jsonData, ContentTypes.TEXT)
                 .Verb(Verbs.POST)
                 .ContentType(ContentTypes.JSON);

            RestManager.SendWebRequest(builder, onCompletion, onError);
        }


        private void Setdata(ConfigDataResponseFormat configDatas)
        {
            ConfigData dataClass = configDatas.data.config;
            string[] str = configDatas.data.config.latestAppVersion.Split('.');
            float versionNumber = float.Parse(str[0] + "." + str[1]);
            float versionCode = float.Parse(str[2]);
            Debug.Log(versionNumber + "    " + versionCode);
            if (versionNumber > buildVersionConfig.versionNumber)
            {
                ShowUpdatePopup(dataClass.isForceUpdate, dataClass.latestAppVersion);
            }
            else if (versionNumber == buildVersionConfig.versionNumber && versionCode > buildVersionConfig.versionCode)
            {
                ShowUpdatePopup(dataClass.isForceUpdate, dataClass.latestAppVersion);
            }
            else
            {
                appVersionCheckingCompleted?.Invoke();
            }
        }
        public void ShowUpdatePopup(string forceUpdate, string latestAppVersion)
        {
            string storeURL;
#if UNITY_ANDROID
            storeURL = buildVersionConfig.AndroidAppStoreURL;
#elif UNITY_IPHONE
         storeURL =buildVersionConfig.iOSAppStoreURL;
#endif
            bool isForceUpdate = forceUpdate.Equals("1");
            InternalLogger.Log(isForceUpdate + "   " + storeURL + "   " + buildVersion + "   " + latestAppVersion);
        }

        public void OnUpdateLaterButtonCliked()
        {
            appVersionCheckingCompleted?.Invoke();
        }

        private void onErrorGetDetails(RestUtil.RestCallError restCallError)
        {
            appVersionCheckingCompleted?.Invoke();
        }
    }
}