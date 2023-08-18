#if UNITY_ANALYTICS_ADDED
using EntertainmentFramework.InternalLoggers;
using System.Collections.Generic;
using Unity.Services.Analytics;
using Unity.Services.Core;

namespace EntertainmentFramework.UnityAnalytics
{
    public class UnityAnalyticsServicesHandler
    {
        private static UnityAnalyticsServicesHandler analyticsHandler;
        public static UnityAnalyticsServicesHandler Instance
        {
            get
            {
                // instantiate the class if the class object is null.
                if (analyticsHandler == null)
                {
                    new UnityAnalyticsServicesHandler();
                }
                return analyticsHandler; // Return the UnityAnalyticsServicesHandler object.
            }
        }

        /// <summary>
        /// Define private constructor for singleton class.
        /// </summary>
        private UnityAnalyticsServicesHandler()
        {
            if (analyticsHandler == null)
            {
                analyticsHandler = this;
            }
        }

        /// <summary>
        /// Function to Initialize Unity Analytucs Services.
        /// </summary>
        public async void InitializeUnityService()
        {
            await UnityServices.InitializeAsync();
            InternalLogger.Log($"Started UGS Analytics Sample with user ID: {AnalyticsService.Instance.GetAnalyticsUserID()}");
            if (AnalyticsManager.Instance.EntertainementSettings.SendUnityAnalytics)
                StartCollectingData();
        }

        /// <summary>
        /// Function to start collecting data for Sending to Unity Analytics.
        /// </summary>
        public void StartCollectingData()
        {
            AnalyticsService.Instance.StartDataCollection();
        }

        /// <summary>
        /// Function to stop collecting data for Sending to Unity Analytics.
        /// </summary>
        public void StopCollectingData()
        {
            AnalyticsService.Instance.StopDataCollection();
        }

        /// <summary>
        /// Fucntion sends the analytics data to Unity Analytics.
        /// </summary>
        /// <param name="eventName"></param>
        /// <param name="mergedDictionary"></param>
        public void SendUnityAnaltyicsEvent(string eventName, Dictionary<string, object> mergedDictionary = null)
        {
#if UNITY_ANALYTICS_ENABLED			
            AnalyticsService.Instance.CustomData(eventName, mergedDictionary);
#endif
        }

    }
}
#endif