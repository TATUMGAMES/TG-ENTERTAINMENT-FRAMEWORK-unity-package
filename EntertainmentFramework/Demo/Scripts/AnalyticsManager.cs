using EntertainmentEditor;
using EntertainmentFramework;
#if UNITY_ANALYTICS_ADDED
using EntertainmentFramework.UnityAnalytics;
#endif
using UnityEngine;

public class AnalyticsManager : MonoBehaviour
{
    private static AnalyticsManager analyticsManagerInstance;

    [HideInInspector]
    public EntertainmentFrameworkSettings EntertainementSettings;

    public static AnalyticsManager Instance
    {
        get
        {
            return analyticsManagerInstance;
        }
    }

    private void Awake()
    {
        if (Equals(analyticsManagerInstance, null))
        {
            analyticsManagerInstance = this;
        }
    }

    private void Start()
    {
        LoadEntertainmentSettings();
#if UNITY_ANALYTICS_ADDED
        if (EntertainementSettings.EnableAutoInitalizeUnityAnalytics)
            UnityAnalyticsServicesHandler.Instance.InitializeUnityService();        
#endif
    }
    /// <summary>
    /// ManualInitializating Unity Analytics.
    /// </summary>
    public void ManualInitializationOfUnityAnalyticsServices()
    {
#if UNITY_ANALYTICS_ADDED
        if (EntertainementSettings.EnableAutoInitalizeUnityAnalytics)
            UnityAnalyticsServicesHandler.Instance.InitializeUnityService();
#endif
    }

    internal void LoadEntertainmentSettings()
    {
        EntertainementSettings = Resources.Load<EntertainmentFrameworkSettings>(Constants.MikrosSettingsAssetName);
    }
}