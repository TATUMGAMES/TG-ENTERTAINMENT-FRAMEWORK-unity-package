#if MIKROS_ADDED
using MikrosClient;
using MikrosClient.Analytics;
using Achievement = MikrosClient.Analytics.TrackUnlockedAchievementRequest.Achievement;
using Participant = MikrosClient.Analytics.TrackPlayerRatingRequest.Participant;
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Reflection;
using EntertainmentFramework.Analytics.Enums;
using Entertainment.Utility;
using System;
using EntertainmentFramework.InternalLoggers;
#if UNITY_ANALYTICS_ADDED
using EntertainmentFramework.UnityAnalytics;
#endif

namespace EntertainmentFramework.PresetEvent
{
    public class PresetEventHandler
    {
        private static PresetEventHandler presetHandlerInstance;

        public static PresetEventHandler Instance
        {
            get
            {
                // instantiate the class if the class object is null.
                if (presetHandlerInstance == null)
                {
                    new PresetEventHandler();
                }
                return presetHandlerInstance; // Return the PresetEventHandler object.
            }

        }

        /// <summary>
        /// Define private constructor for singleton class.
        /// </summary>
        private PresetEventHandler()
        {
            if (presetHandlerInstance == null)
            {
                presetHandlerInstance = this;
            }
        }

        /// <summary>
        /// Tracks Game over Preset Event.
        /// <see href="https://developer.tatumgames.com/documentation/log-preset-events#track-game-over"> Used to track Gameover.</see>
        /// </summary>
        public void TrackGameOver(UnityAction callback = null, UnityAction<string> failedCallBack = null)
        {
            IPresetAnalyticsEventRequest presetAnalyticsEventRequest = TrackGameOverRequest.Builder()
                  .Create(
                  trackGameOverRequest => MikrosManager.Instance.AnalyticsController.LogEvent(trackGameOverRequest, response =>
                  {
                      callback?.Invoke();
                      InternalLogger.Log("Success response : " + response.Status.StatusCode);
                  }),
                  onFailure =>
                  {
                      failedCallBack?.Invoke(onFailure.Message);
                      InternalLogger.LogError("Failue response : " + onFailure.Message);
                  });

            string primaryJson = JsonConvert.SerializeObject(presetAnalyticsEventRequest);
            // convert primary JSON into string, object pairs
            Dictionary<string, object> primaryJsonDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(primaryJson);
            // merge dictionaries to send as Track Gameover event
            Dictionary<string, object> mergedDictionary = GetCompleteAnalyticsParameterDictionary(primaryJsonDictionary);
            InternalLogger.Log("Final json " + JsonConvert.SerializeObject(mergedDictionary));
            SendUnityAnaltyicsEvent("game_over", mergedDictionary);
        }

        /// <summary>
        /// Tracks Level End Preset Event.
        /// <see href="https://developer.tatumgames.com/documentation/log-preset-events#track-level-end"> Used to track when a level has ended.</see>
        /// </summary>
        public void TrackLevelEnd(long level, long subLevel = 0, string levelName = null, string description = null, float levelPlayerDuration = 0, bool isLevelCompleted = true, UnityAction callback = null, UnityAction<string> failedCallBack = null)
        {
            IPresetAnalyticsEventRequest presetAnalyticsEventRequest = null;
            presetAnalyticsEventRequest = TrackLevelEndRequest.Builder()
                 .Level(level)
                 .SubLevel(subLevel)
                 .LevelName(levelName)
                 .Description(description)
                 .Duration(levelPlayerDuration)
                 .Success(isLevelCompleted)
                 .Create(
                 trackLevelEndRequest => MikrosManager.Instance.AnalyticsController.LogEvent(trackLevelEndRequest, response =>
                 {
                     callback?.Invoke();
                     InternalLogger.Log("Success response : " + response);
                 }),
                onFailure =>
                {
                    failedCallBack?.Invoke(onFailure.Message);
                    InternalLogger.LogError("Failue response : " + onFailure.Message);
                });

            string primaryJson = JsonConvert.SerializeObject(presetAnalyticsEventRequest);
            // convert primary JSON into string, object pairs
            Dictionary<string, object> primaryJsonDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(primaryJson);
            // merge dictionaries to send as Track Level End event
            Dictionary<string, object> mergedDictionary = GetCompleteAnalyticsParameterDictionary(primaryJsonDictionary);
            InternalLogger.Log("Final json " + JsonConvert.SerializeObject(mergedDictionary));
            SendUnityAnaltyicsEvent("level_end", mergedDictionary);
        }

        /// <summary>
        /// Tracks Level Start Preset Event.
        /// <see href="https://developer.tatumgames.com/documentation/log-preset-events#track-level-start">Used to track when a level has started.</see>
        /// </summary>
        public void TrackLevelStart(long level, long subLevel = 0, string levelName = null, string description = null, UnityAction callback = null, UnityAction<string> failedCallBack = null)
        {
            IPresetAnalyticsEventRequest presetAnalyticsEventRequest = TrackLevelStartRequest.Builder()
                  .Level(level)
                  .SubLevel(subLevel)
                  .LevelName(levelName)
                  .Description(description)
                  .Create(
                  trackLevelStartRequest => MikrosManager.Instance.AnalyticsController.LogEvent(trackLevelStartRequest, response =>
                  {
                      callback?.Invoke();
                      InternalLogger.Log("Success response : " + response.Status.StatusCode);
                  }),
                  onFailure =>
                  {
                      failedCallBack?.Invoke(onFailure.Message);
                      InternalLogger.LogError("Failue response : " + onFailure.Message);
                  });

            string primaryJson = JsonConvert.SerializeObject(presetAnalyticsEventRequest);
            // convert primary JSON into string, object pairs
            Dictionary<string, object> primaryJsonDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(primaryJson);
            // merge dictionaries to send as Track Level Start event
            Dictionary<string, object> mergedDictionary = GetCompleteAnalyticsParameterDictionary(primaryJsonDictionary);
            InternalLogger.Log("Final json " + JsonConvert.SerializeObject(mergedDictionary));
            SendUnityAnaltyicsEvent("level_start", mergedDictionary);
        }

        /// <summary>
        /// Tracks Level Up Preset Event.
        /// <see href="https://developer.tatumgames.com/documentation/log-preset-events#track-level-up">Tracks when a user has advanced a level.</see>
        /// </summary>
        public void TrackLevelUp(long level, long subLevel = 0, string levelName = null, string character = null, string description = null, UnityAction callback = null, UnityAction<string> failedCallBack = null)
        {
            IPresetAnalyticsEventRequest presetAnalyticsEventRequest = TrackLevelUpRequest.Builder()
                 .Level(level)
                 .SubLevel(subLevel)
                 .LevelName(levelName)
                 .Character(character)
                 .Description(description)
                 .Create(
                 trackLevelUpRequest => MikrosManager.Instance.AnalyticsController.LogEvent(trackLevelUpRequest, response =>
                 {
                     callback?.Invoke();
                     InternalLogger.Log("Success response : " + response.Status.StatusCode);
                 }),
                 onFailure =>
                 {
                     failedCallBack?.Invoke(onFailure.Message);
                     InternalLogger.LogError("Failue response : " + onFailure.Message);
                 });

            string primaryJson = JsonConvert.SerializeObject(presetAnalyticsEventRequest);
            // convert primary JSON into string, object pairs
            Dictionary<string, object> primaryJsonDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(primaryJson);
            // merge dictionaries to send as Track Level Up event
            Dictionary<string, object> mergedDictionary = GetCompleteAnalyticsParameterDictionary(primaryJsonDictionary);
            InternalLogger.Log("Final json " + JsonConvert.SerializeObject(mergedDictionary));
            SendUnityAnaltyicsEvent("level_up", mergedDictionary);
        }

        /// <summary>
        /// Tracks Post Score Preset Event.
        /// <see href="https://developer.tatumgames.com/documentation/log-preset-events#track-post-score">Track when a user update score.</see>
        /// </summary>
        public void TrackPostScore(long score, long level = 0, long subLevel = 0, string levelName = null, string character = null, UnityAction callback = null, UnityAction<string> failedCallBack = null)
        {
            IPresetAnalyticsEventRequest presetAnalyticsEventRequest = TrackPostScoreRequest.Builder()
                .Score(score)
                .Level(level)
                .SubLevel(subLevel)
                .LevelName(levelName)
                .Character(character)
                .Create(
                trackPostScoreRequest => MikrosManager.Instance.AnalyticsController.LogEvent(trackPostScoreRequest, response =>
                {
                    callback?.Invoke();
                    InternalLogger.Log("Success response : " + response.Status.StatusCode);
                }),
                onFailure =>
                {
                    failedCallBack?.Invoke(onFailure.Message);
                    InternalLogger.LogError("Failue response : " + onFailure.Message);
                });

            string primaryJson = JsonConvert.SerializeObject(presetAnalyticsEventRequest);
            // convert primary JSON into string, object pairs
            Dictionary<string, object> primaryJsonDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(primaryJson);
            // merge dictionaries to send as Track Post score event
            Dictionary<string, object> mergedDictionary = GetCompleteAnalyticsParameterDictionary(primaryJsonDictionary);
            InternalLogger.Log("Final json " + JsonConvert.SerializeObject(mergedDictionary));
            SendUnityAnaltyicsEvent("score", mergedDictionary);
        }

        /// <summary>
        /// Tracks Share Preset Event.
        /// <see href="https://developer.tatumgames.com/documentation/log-preset-events#track-content-share">Track share event when user share something.</see>
        /// </summary>
        public void TrackShare(string platform = null, ContentType content = ContentType.Text, UnityAction callback = null, UnityAction<string> failedCallBack = null)
        {
            IPresetAnalyticsEventRequest presetAnalyticsEventRequest = TrackShareRequest.Builder()
                  .Platform(platform)
                  .ContentType(content)
                  .Create(
                  trackShareRequest => MikrosManager.Instance.AnalyticsController.LogEvent(trackShareRequest, response =>
                  {
                      callback?.Invoke();
                      InternalLogger.Log("Success response : " + response.Status.StatusCode);
                  }),
                  onFailure =>
                  {
                      failedCallBack?.Invoke(onFailure.Message);
                      InternalLogger.LogError("Failue response : " + onFailure.Message);
                  });

            string primaryJson = JsonConvert.SerializeObject(presetAnalyticsEventRequest);
            // convert primary JSON into string, object pairs
            Dictionary<string, object> primaryJsonDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(primaryJson);
            // merge dictionaries to send as Track Share event
            Dictionary<string, object> mergedDictionary = GetCompleteAnalyticsParameterDictionary(primaryJsonDictionary);
            InternalLogger.Log("Final json " + JsonConvert.SerializeObject(mergedDictionary));
            SendUnityAnaltyicsEvent("share_social_media", mergedDictionary);
        }

        /// <summary>
        /// Tracks SignIn Preset Event.
        /// <see href="https://developer.tatumgames.com/documentation/log-preset-events#track-signin-request">Track Signing when user Sign In.</see>
        /// </summary>
        public void TrackSignIn(string platform = null, UnityAction callback = null, UnityAction<string> failedCallBack = null)
        {
            IPresetAnalyticsEventRequest presetAnalyticsEventRequest = TrackSigninRequest.Builder()
                 .Platform(platform)
                 .Create(
                 trackSigninRequest => MikrosManager.Instance.AnalyticsController.LogEvent(trackSigninRequest, response =>
                 {
                     callback?.Invoke();
                     InternalLogger.Log("Success response : " + response.Status.StatusCode);
                 }),
                 onFailure =>
                 {
                     failedCallBack?.Invoke(onFailure.Message);
                     InternalLogger.LogError("Failue response : " + onFailure.Message);
                 });

            string primaryJson = JsonConvert.SerializeObject(presetAnalyticsEventRequest);
            // convert primary JSON into string, object pairs
            Dictionary<string, object> primaryJsonDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(primaryJson);
            // merge dictionaries to send as Track Sign In event
            Dictionary<string, object> mergedDictionary = GetCompleteAnalyticsParameterDictionary(primaryJsonDictionary);
            InternalLogger.Log("Final json " + JsonConvert.SerializeObject(mergedDictionary));
            SendUnityAnaltyicsEvent("signin", mergedDictionary);
        }

        /// <summary>
        /// Tracks Sign Up Preset Event.
        /// <see href="https://developer.tatumgames.com/documentation/log-preset-events#track-signup-request">Tracks when a user signs up.</see>
        /// </summary>
        public void TrackSignUp(string platform = null, UnityAction callback = null, UnityAction<string> failedCallBack = null)
        {
            IPresetAnalyticsEventRequest presetAnalyticsEventRequest = TrackSignupRequest.Builder()
                  .Platform(platform)
                  .Create(
                  trackSignupRequest => MikrosManager.Instance.AnalyticsController.LogEvent(trackSignupRequest, response =>
                  {
                      callback?.Invoke();
                      InternalLogger.Log("Success response : " + response.Status.StatusCode);
                  }),
                  onFailure =>
                  {
                      failedCallBack?.Invoke(onFailure.Message);
                      InternalLogger.LogError("Failue response : " + onFailure.Message);
                  });

            string primaryJson = JsonConvert.SerializeObject(presetAnalyticsEventRequest);
            // convert primary JSON into string, object pairs
            Dictionary<string, object> primaryJsonDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(primaryJson);
            // merge dictionaries to send as Track Sign Up event
            Dictionary<string, object> mergedDictionary = GetCompleteAnalyticsParameterDictionary(primaryJsonDictionary);
            InternalLogger.Log("Final json " + JsonConvert.SerializeObject(mergedDictionary));
            SendUnityAnaltyicsEvent("signup", mergedDictionary);
        }

        /// <summary>
        /// Tracks Start Timer Preset Event.
        /// <see href="https://developer.tatumgames.com/documentation/log-preset-events#track-timed-events-start">Track when the level starts.</see>
        /// </summary>
        public void TrackStartTimer(string eventName, UnityAction callback = null, UnityAction<string> failedCallBack = null)
        {
            IPresetAnalyticsEventRequest presetAnalyticsEventRequest = TrackStartTimerRequest.Builder()
                  .Event(eventName)
                  .Create(
                  trackStartTimerRequest => MikrosManager.Instance.AnalyticsController.LogEvent(trackStartTimerRequest, response =>
                  {
                      callback?.Invoke();
                      InternalLogger.Log("Success response : " + response.Status.StatusCode);
                  }),
                  onFailure =>
                  {
                      failedCallBack?.Invoke(onFailure.Message);
                      InternalLogger.LogError("Failue response : " + onFailure.Message);
                  });

            string primaryJson = JsonConvert.SerializeObject(presetAnalyticsEventRequest);
            // convert primary JSON into string, object pairs
            Dictionary<string, object> primaryJsonDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(primaryJson);
            // merge dictionaries to send as Track Start Timer event
            Dictionary<string, object> mergedDictionary = GetCompleteAnalyticsParameterDictionary(primaryJsonDictionary);
            InternalLogger.Log("Final json " + JsonConvert.SerializeObject(mergedDictionary));
            SendUnityAnaltyicsEvent("start_timer", mergedDictionary);
        }

        /// <summary>
        /// Tracks Stop Timer Preset Event.
        /// <see cref=" https://developer.tatumgames.com/documentation/log-preset-events#track-timed-events-stop">Track when the level ends.</see>
        /// </summary>
        public void TrackStopTimer(string eventName, UnityAction callback = null, UnityAction<string> failedCallBack = null)
        {
            IPresetAnalyticsEventRequest presetAnalyticsEventRequest = TrackStopTimerRequest.Builder()
                  .Event(eventName)
                  .Create(
                  trackStopTimerRequest => MikrosManager.Instance.AnalyticsController.LogEvent(trackStopTimerRequest, response =>
                  {
                      callback?.Invoke();
                      InternalLogger.Log("Success response : " + response.Status.StatusCode);
                  }),
                  onFailure =>
                  {
                      failedCallBack?.Invoke(onFailure.Message);
                      InternalLogger.LogError("Failue response : " + onFailure.Message);
                  });

            string primaryJson = JsonConvert.SerializeObject(presetAnalyticsEventRequest);
            // convert primary JSON into string, object pairs
            Dictionary<string, object> primaryJsonDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(primaryJson);
            // merge dictionaries to send as Track Stop Timer event
            Dictionary<string, object> mergedDictionary = GetCompleteAnalyticsParameterDictionary(primaryJsonDictionary);
            InternalLogger.Log("Final json " + JsonConvert.SerializeObject(mergedDictionary));
            SendUnityAnaltyicsEvent("stop_timer", mergedDictionary);
        }

        /// <summary>
        /// Tracks Tutorial Begin Preset Event.
        /// <see href="https://developer.tatumgames.com/documentation/log-preset-events#track-tutorial-begin">Track when user start turorial.</see>
        /// </summary>
        public void TrackTutorialBegin(UnityAction callback = null, UnityAction<string> failedCallBack = null)
        {
            IPresetAnalyticsEventRequest presetAnalyticsEventRequest = TrackTutorialBeginRequest.Builder()
                  .Create(
                  trackTutorialBeginRequest => MikrosManager.Instance.AnalyticsController.LogEvent(trackTutorialBeginRequest, response =>
                  {
                      callback?.Invoke();
                      InternalLogger.Log("Success response : " + response.Status.StatusCode);
                  }),
                  onFailure =>
                  {
                      failedCallBack?.Invoke(onFailure.Message);
                      InternalLogger.LogError("Failue response : " + onFailure.Message);
                  });

            string primaryJson = JsonConvert.SerializeObject(presetAnalyticsEventRequest);
            // convert primary JSON into string, object pairs
            Dictionary<string, object> primaryJsonDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(primaryJson);
            // merge dictionaries to send as Track Tutorial Begin event
            Dictionary<string, object> mergedDictionary = GetCompleteAnalyticsParameterDictionary(primaryJsonDictionary);
            InternalLogger.Log("Final json " + JsonConvert.SerializeObject(mergedDictionary));
            SendUnityAnaltyicsEvent("tutorial_begin", mergedDictionary);
        }

        /// <summary>
        /// Tracks Tutorial Completed Preset Event.
        /// <see href="https://developer.tatumgames.com/documentation/log-preset-events#track-tutorial-complete">Track when user complete turorial</see>
        /// </summary>
        public void TrackTutorialComplete(UnityAction callback = null, UnityAction<string> failedCallBack = null)
        {
            IPresetAnalyticsEventRequest presetAnalyticsEventRequest = TrackTutorialCompleteRequest.Builder()
                 .Create(
                 trackTutorialCompleteRequest => MikrosManager.Instance.AnalyticsController.LogEvent(trackTutorialCompleteRequest, response =>
                 {
                     callback?.Invoke();
                     InternalLogger.Log("Success response : " + response.Status.StatusCode);
                 }),
                 onFailure =>
                 {
                     failedCallBack?.Invoke(onFailure.Message);
                     InternalLogger.LogError("Failue response : " + onFailure.Message);
                 });

            string primaryJson = JsonConvert.SerializeObject(presetAnalyticsEventRequest);
            // convert primary JSON into string, object pairs
            Dictionary<string, object> primaryJsonDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(primaryJson);
            // merge dictionaries to send as Track Tutorial Complete event
            Dictionary<string, object> mergedDictionary = GetCompleteAnalyticsParameterDictionary(primaryJsonDictionary);
            InternalLogger.Log("Final json " + JsonConvert.SerializeObject(mergedDictionary));
            SendUnityAnaltyicsEvent("tutorial_completed", mergedDictionary);
        }

        /// <summary>
        /// Tracks Unlocked Achievements Preset Event.
        /// <see href="https://developer.tatumgames.com/documentation/log-preset-events#track-unlocked-achievement">Tracks Unlocked Achievements Event</see>
        /// </summary>
        public void TrackUnlockedAchievement(List<Achievement> achievementData, UnityAction callback = null, UnityAction<string> failedCallBack = null)
        {
            IPresetAnalyticsEventRequest presetAnalyticsEventRequest = TrackUnlockedAchievementRequest.Builder()
                 .Achievements(achievementData)
                 .Create(
                 trackUnlockedAchievementRequest => MikrosManager.Instance.AnalyticsController.LogEvent(trackUnlockedAchievementRequest, response =>
                 {
                     callback?.Invoke();
                     InternalLogger.Log("Success response : " + response.Status.StatusCode);
                 }),
                 onFailure =>
                 {
                     failedCallBack?.Invoke(onFailure.Message);
                     InternalLogger.LogError("Failue response : " + onFailure.Message);
                 });

            string primaryJson = JsonConvert.SerializeObject(presetAnalyticsEventRequest);
            // convert primaryjson to AchievementsDetails Class
            AchievementDetails achievementDetails = JsonConvert.DeserializeObject<AchievementDetails>(primaryJson);

            if (achievementDetails.achievementData.Count > 0)
            {
                for (int i = 0; i < achievementDetails.achievementData.Count; i++)
                {
                    //Converting the AchievementData class to string
                    string finalJson = JsonConvert.SerializeObject(achievementDetails.achievementData[i]);
                    InternalLogger.Log(finalJson);
                    // convert fianl JSON into string, object pairs
                    Dictionary<string, object> primaryJsonDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(finalJson);
                    // merge dictionaries to send as Track Achievements event
                    Dictionary<string, object> mergedDictionary = GetCompleteAnalyticsParameterDictionary(primaryJsonDictionary);
                    InternalLogger.Log("Final json " + JsonConvert.SerializeObject(mergedDictionary));
                    SendUnityAnaltyicsEvent("achievement", mergedDictionary);
                }
            }
            else
            {
                // convert primary JSON into string, object pairs
                Dictionary<string, object> primaryJsonDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(primaryJson);
                Dictionary<string, object> tempDictionary = new Dictionary<string, object>();
                foreach (KeyValuePair<string, object> val in primaryJsonDictionary)
                {
                    // only add matched values from the primary dictionary to a temporary dictionary
                    tempDictionary.Add(val.Key, JsonConvert.SerializeObject(val.Value));
                }
                // merge dictionaries to send as Track Unlock Achievement event
                Dictionary<string, object> mergedDictionary = GetCompleteAnalyticsParameterDictionary(tempDictionary);
                InternalLogger.Log("Final json " + JsonConvert.SerializeObject(mergedDictionary));
                SendUnityAnaltyicsEvent("achievement", mergedDictionary);
            }
        }

        /// <summary>
        /// Tracks Screen Time Preset Event.
        /// <see href="https://developer.tatumgames.com/documentation/log-preset-events#track-screen">Track user Screen time</see>
        /// </summary>
        public void TrackScreenTime(string screenName, string screenClass = null, float timeSpentOnScreen = 0.0f, UnityAction callback = null, UnityAction<string> failedCallBack = null)
        {
            IPresetAnalyticsEventRequest presetAnalyticsEventRequest = TrackScreenTimeRequest.Builder()
                  .ScreenName(screenName)
                  .ScreenClass(screenClass)
                  .TimeSpentOnScreen(timeSpentOnScreen)
                  .Create(
                  trackScreenTimeRequest => MikrosManager.Instance.AnalyticsController.LogEvent(trackScreenTimeRequest, response =>
                  {
                      callback?.Invoke();
                      InternalLogger.Log("Success response : " + response.Status.StatusCode);
                  }),
                 onFailure =>
                 {
                     failedCallBack?.Invoke(onFailure.Message);
                     InternalLogger.LogError("Failue response : " + onFailure.Message);
                 });

            string primaryJson = JsonConvert.SerializeObject(presetAnalyticsEventRequest);
            // convert primaryjson to ScreenDetails Class
            ScreenDetails trackScreen = JsonConvert.DeserializeObject<ScreenDetails>(primaryJson);
            //Converting the AchievementData class to string
            string finalJson = JsonConvert.SerializeObject(trackScreen.screenData);
            InternalLogger.Log(finalJson);

            // convert primary JSON into string, object pairs
            Dictionary<string, object> primaryJsonDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(finalJson);
            // merge dictionaries to send as Track Screen Time event
            Dictionary<string, object> mergedDictionary = GetCompleteAnalyticsParameterDictionary(primaryJsonDictionary);
            InternalLogger.Log("Final json " + JsonConvert.SerializeObject(mergedDictionary));
            SendUnityAnaltyicsEvent("screen_time", mergedDictionary);
        }

        /// <summary>
        /// Tracks Purchase Request Preset Event.
        /// <see href="https://developer.tatumgames.com/documentation/log-preset-events#track-purchase">Tracks user purchase</see>
        /// In this instance, the terms SkuType refers to items like currency, bundle, character skin,
        /// weapons, armor, ect.    SkuSubType is used to provide more details on item purchased
        /// </summary>
        public void TrackPurchaseRequestEvent(string skuName, string skuDescription, PurchaseCategory purchaseCategory, PurchaseType purchaseType,
           PurchaseCurrencyType purchaseCurrencyType, float purchasePrice, List<PurchaseInfoData> purchaseDetails, float discountPercentage = 0,
                                                float amountRewarded = 0, UnityAction callback = null, UnityAction<string> failedCallBack = null)
        {
            List<TrackPurchaseRequest.PurchaseInfo> purchaseDetail = new List<TrackPurchaseRequest.PurchaseInfo>();
            foreach (PurchaseInfoData purchaseinfo in purchaseDetails)
            {
                TrackPurchaseRequest.PurchaseInfo purchaseInfoData = TrackPurchaseRequest.PurchaseInfo.Builder()
                      .SkuName(purchaseinfo.skuName)
                      .SkuDescription(purchaseinfo.skuDescription)
                      .PurchaseCategory(purchaseinfo.purchaseCategory)
                      .Create();

                purchaseDetail.Add(purchaseInfoData);
            }

            IPresetAnalyticsEventRequest presetAnalyticsEventRequest = TrackPurchaseRequest.Builder()
            .SkuName(skuName)
            .SkuDescription(skuDescription)
            .PurchaseCategory(purchaseCategory)
            .PurchaseType(purchaseType)
            .PurchaseCurrencyType(purchaseCurrencyType)
            .PurchasePrice(purchasePrice)
            .PercentDiscount(discountPercentage)
            .AmountRewarded(amountRewarded)
            .PurchaseDetails(purchaseDetail)
            .Create(
            trackPurchaseRequest => MikrosManager.Instance.AnalyticsController.LogEvent(trackPurchaseRequest, response =>
            {
                callback?.Invoke();
                InternalLogger.Log("Success response : " + response.Status.StatusCode);
            }),
            onFailure =>
            {
                failedCallBack?.Invoke(onFailure.Message);
                InternalLogger.LogError("Failue response : " + onFailure.Message);
            });

            string primaryJson = JsonConvert.SerializeObject(presetAnalyticsEventRequest);
            InternalLogger.Log(primaryJson);
            PurchaseDataInfo purchaseDataInfo = JsonConvert.DeserializeObject<PurchaseDataInfo>(primaryJson);
            PurchaseDetailInfo purchaseDetailInfo = JsonConvert.DeserializeObject<PurchaseDetailInfo>(primaryJson);

            InternalLogger.Log(purchaseDetailInfo.purchaseData.purchaseDetails.Count);

            //Converting the int value type to the respective Enum value
            List<string> purchaseCategoryType = GetPurchaseCategoryData(purchaseCategory);
            purchaseDataInfo.purchaseData.skuType = purchaseCategoryType[0];
            purchaseDataInfo.purchaseData.skuSubType = purchaseCategoryType[1];
            purchaseDataInfo.purchaseData.purchaseType = purchaseType.ToString();
            purchaseDataInfo.purchaseData.purchaseCurrencyType = purchaseCurrencyType.ToString();
            purchaseDataInfo.purchaseData.uniquePurchaseId = UtilsClass.GetRandomAlphanumericSequence();


            if (purchaseDetailInfo.purchaseData.purchaseDetails.Count > 0)
            {
                //Converts the purchaseDataInfo class to string
                string primaryJsons = JsonConvert.SerializeObject(purchaseDataInfo.purchaseData);
                for (int i = 0; i < purchaseDetailInfo.purchaseData.purchaseDetails.Count; i++)
                {
                    //Converting the int value type to the respective Enum value
                    List<string> subPurchaseCategoryType = GetPurchaseCategoryData(purchaseDetails[i].purchaseCategory);
                    purchaseDetailInfo.purchaseData.purchaseDetails[i].skuType = subPurchaseCategoryType[0];
                    purchaseDetailInfo.purchaseData.purchaseDetails[i].skuSubType = subPurchaseCategoryType[1];

                    //Creating a new class to merge it in a single class object
                    SecondaryPurchaseInfo subPurchaseDetails = new SecondaryPurchaseInfo
                    {
                        skuName = purchaseDetailInfo.purchaseData.purchaseDetails[i].skuName,
                        skuDescription = purchaseDetailInfo.purchaseData.purchaseDetails[i].skuDescription,
                        skuType = purchaseDetailInfo.purchaseData.purchaseDetails[i].skuType,
                        skuSubType = purchaseDetailInfo.purchaseData.purchaseDetails[i].skuSubType,
                        timestamp = purchaseDetailInfo.purchaseData.purchaseDetails[i].timestamp
                    };
                    //Merging two string into a single 
                    string mergerString = GetMergedString(primaryJsons, JsonConvert.SerializeObject(subPurchaseDetails));
                    Dictionary<string, object> primaryJsonDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(mergerString);

                    // merge dictionaries to send as Track Purchase event
                    Dictionary<string, object> mergedDictionary = GetCompleteAnalyticsParameterDictionary(primaryJsonDictionary);
                    InternalLogger.Log("Final json " + JsonConvert.SerializeObject(mergedDictionary));
                    SendUnityAnaltyicsEvent("purchase", mergedDictionary);
                }
            }
            else
            {
                string primaryJsons = JsonConvert.SerializeObject(purchaseDataInfo.purchaseData);
                // convert primary JSON into string, object pairs
                Dictionary<string, object> primaryJsonDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(primaryJsons);
                // only add matched values from the primary dictionary to a temporary dictionary

                // merge dictionaries to send as Track Purchase event
                Dictionary<string, object> mergedDictionary = GetCompleteAnalyticsParameterDictionary(primaryJsonDictionary);
                InternalLogger.Log("Final json " + JsonConvert.SerializeObject(mergedDictionary));
                SendUnityAnaltyicsEvent("purchase", mergedDictionary);
            }
        }

        /// <summary>
        /// Tracks Player Rating Request Preset Event.
        /// <see href="https://developer.tatumgames.com/documentation/log-preset-events#track-player-rating">Tracks user purchase</see>
        /// </summary>
        public void TrackPlayerRating(List<ParticipantData> participantsDetails, UnityAction callback = null, UnityAction<string> failedCallBack = null)
        {
            List<Participant> participants = new List<Participant>();
            foreach (ParticipantData participantInfo in participantsDetails)
            {
                Participant participantInfoData = Participant.Builder()
                .Username(participantInfo.userName)
                .Email(participantInfo.email)
                .Behavior(participantInfo.participantBehavior)
                .Create(
                participantRequest =>
                {
                    participants.Add(participantRequest);
                },
                onFailure =>
                {
                    InternalLogger.LogError(onFailure.Message);
                });
            }
            IPresetAnalyticsEventRequest presetAnalyticsEventRequest = TrackPlayerRatingRequest.Builder()
                .Participants(participants)
                .Create(
                playerRatingRequest =>
                {
                    MikrosManager.Instance.AnalyticsController.LogEvent(playerRatingRequest, response =>
                    {
                        STATUS_TYPE statusType = MikrosClient.Utils.DetectStatusType(response.Status.StatusCode);
                        if (statusType == STATUS_TYPE.SUCCESS)
                        {
                            InternalLogger.Log("Success message: " + response.Status.StatusMessage);
                        }
                        else if (statusType == STATUS_TYPE.ERROR)
                        {
                            InternalLogger.Log("Error message: " + response.Status.StatusMessage);
                        }
                        else
                        {
                            InternalLogger.Log("Other issue status message: " + response.Status.StatusMessage);
                        }
                        callback?.Invoke();
                    });
                },
                onFailure =>
                {
                    failedCallBack?.Invoke(onFailure.Message);
                    InternalLogger.Log(onFailure.Message);
                });

            string primaryJson = JsonConvert.SerializeObject(presetAnalyticsEventRequest);
            // convert primary JSON into string, object pairs
            Dictionary<string, object> primaryJsonDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(primaryJson);
            // merge dictionaries to send as Track Gameover event
            Dictionary<string, object> mergedDictionary = GetCompleteAnalyticsParameterDictionary(primaryJsonDictionary);
            InternalLogger.Log("Final json " + JsonConvert.SerializeObject(mergedDictionary));
            SendUnityAnaltyicsEvent("player_rating", mergedDictionary);
        }

        /// <summary>
        /// Tracks Exception Event.
        /// <see href="https://developer.tatumgames.com/documentation/log-preset-events#track-game-over"> Used to track Gameover.</see>
        /// </summary>
        public void TrackException(System.Exception exception, UnityAction callback = null, UnityAction<string> failedCallBack = null)
        {

            IPresetAnalyticsEventRequest presetAnalyticsEventRequest = TrackHandledExceptionRequest.Builder()
                .SetException(exception);

            string primaryJson = JsonConvert.SerializeObject(presetAnalyticsEventRequest);
            InternalLogger.Log(primaryJson);
            // convert primary JSON into string, object pairs.
            Dictionary<string, object> primaryJsonDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(primaryJson);
            //converts the dictionary into string.
            string exceptionString = JsonConvert.SerializeObject(primaryJsonDictionary[Constants.Exception]);
            Exception exceptionData = JsonConvert.DeserializeObject<Exception>(exceptionString);
            InternalLogger.Log(exceptionString);
            //removing the unwanted strings from the primaryJsonDictionary 
            primaryJsonDictionary.Remove(Constants.Exception);
            //adding the detail message strings to the primaryJsonDictionary
            primaryJsonDictionary.Add(Constants.ExceptionMessage, exceptionData.detailMessage.ToString());

            // merge dictionaries to send as Track Gameover event
            Dictionary<string, object> mergedDictionary = GetCompleteAnalyticsParameterDictionary(primaryJsonDictionary);
            InternalLogger.Log("Final json " + JsonConvert.SerializeObject(mergedDictionary));
            SendUnityAnaltyicsEvent("exception", mergedDictionary);

        }

        /// <summary>
        /// Tracks Custom event with only event name.
        /// </summary>
        /// <param name="eventName"></param>
        /// <param name="callback"></param>
        /// <param name="failedCallBack"></param>
        public void TrackCustom(string eventName, UnityAction callback = null, UnityAction<string> failedCallBack = null)
        {
            List<Hashtable> customEvents = new List<Hashtable>();
            MikrosManager.Instance.AnalyticsController.LogEvent(eventName, (Hashtable customEvent) =>
            {
                customEvents.Add(customEvent);
                callback?.Invoke();
            },
            onFailure =>
            {
                InternalLogger.LogError(onFailure.Message);
                failedCallBack?.Invoke(onFailure.Message);
            });

            SendUnityAnaltyicsEvent(eventName);
        }
        /// <summary>
        /// Tracks Custom event.
        /// </summary>
        /// <param name="eventName">Event name of the event</param>
        /// <param name="eventKey">string, the name of the event</param>
        /// <param name="eventValue">string, the value of the event key</param>
        /// <param name="callback"></param>
        /// <param name="failedCallBack"></param>
        public void TrackCustom(string eventName, string eventKey, string eventValue, UnityAction callback = null, UnityAction<string> failedCallBack = null)
        {
            List<Hashtable> customEvents = new List<Hashtable>();
            MikrosManager.Instance.AnalyticsController.LogEvent(eventName, eventKey, eventValue, (Hashtable customEvent) =>
            {
                customEvents.Add(customEvent);
                callback?.Invoke();
            },
            onFailure =>
            {
                InternalLogger.LogError(onFailure.Message);
                failedCallBack?.Invoke(onFailure.Message);
            });
            Dictionary<string, object> finalDictionary = new Dictionary<string, object>
            {
                { eventKey, eventValue }
            };
            SendUnityAnaltyicsEvent(eventName, finalDictionary);
        }

        /// <summary>
        /// Tracks Custom event.
        /// </summary>
        /// <param name="eventName">Event name of the event.</param>
        /// <param name="key">string, the name of the event</param>
        /// <param name="value">double, the value of the event key</param>
        /// <param name="callback"></param>
        /// <param name="failedCallBack"></param>
        public void TrackCustom(string eventName, string eventKey, double eventValue, UnityAction callback = null, UnityAction<string> failedCallBack = null)
        {
            List<Hashtable> customEvents = new List<Hashtable>();
            MikrosManager.Instance.AnalyticsController.LogEvent(eventName, eventKey, eventValue, (Hashtable customEvent) =>
            {
                customEvents.Add(customEvent);
                callback?.Invoke();
            },
            onFailure =>
            {
                InternalLogger.LogError(onFailure.Message);
                failedCallBack?.Invoke(onFailure.Message);
            });
            Dictionary<string, object> finalDictionary = new Dictionary<string, object>
            {
                { eventKey, eventValue }
            };
            SendUnityAnaltyicsEvent(eventName, finalDictionary);
        }

        /// <summary>
        /// Tracks Custom event.
        /// </summary>
        /// <param name="eventName">Event name of the event.</param>
        /// <param name="key">string, the name of the event</param>
        /// <param name="value">long, the value of the event key</param>
        /// <param name="callback"></param>
        /// <param name="failedCallBack"></param>
        public void TrackCustom(string eventName, string eventKey, long eventValue, UnityAction callback = null, UnityAction<string> failedCallBack = null)
        {
            List<Hashtable> customEvents = new List<Hashtable>();
            MikrosManager.Instance.AnalyticsController.LogEvent(eventName, eventKey, eventValue, (Hashtable customEvent) =>
            {
                customEvents.Add(customEvent);
                callback?.Invoke();
            },
            onFailure =>
            {
                InternalLogger.LogError(onFailure.Message);
                failedCallBack?.Invoke(onFailure.Message);
            });
            Dictionary<string, object> finalDictionary = new Dictionary<string, object>
            {
                { eventKey, eventValue }
            };
            SendUnityAnaltyicsEvent(eventName, finalDictionary);
        }

        /// <summary>
        /// Tracks Custom event.
        /// </summary>
        /// <param name="eventName">Event name of the event.</param>
        /// <param name="data"></param>
        /// <param name="callback"></param>
        /// <param name="failedCallBack"></param>
        public void TrackCustom(string eventName, Hashtable data, UnityAction callback = null, UnityAction<string> failedCallBack = null)
        {
            List<Hashtable> customEvents = new List<Hashtable>();
            Dictionary<string, object> finalDictionary = new Dictionary<string, object>();
            MikrosManager.Instance.AnalyticsController.LogEvent(eventName, data, (Hashtable customEvent) =>
            {
                customEvents.Add(customEvent);
                callback?.Invoke();
                foreach (DictionaryEntry dictionaryEntry in customEvent)
                {
                    finalDictionary.Add(dictionaryEntry.Key.ToString(), dictionaryEntry.Value);
                }
            },
            onFailure =>
            {
                InternalLogger.LogError(onFailure.Message);
                failedCallBack?.Invoke(onFailure.Message);
            });
            SendUnityAnaltyicsEvent(eventName, finalDictionary);
        }
        /// <summary>
        /// Flush events on button click. Flush refers to when information in a buffer becomes written to storage
        /// </summary>
        public void FlushEvents()
        {
            MikrosManager.Instance.AnalyticsController.FlushEvents();
        }


        #region Default Analytics Parameter 
        /// <summary>
        /// Functions that return the default data and creates a new merged dictionary.
        /// </summary>
        /// <param name="secondaryDictionary"></param>
        /// <returns>Final dictionary that needs to be send for unity analytics</returns>
        internal Dictionary<string, object> GetCompleteAnalyticsParameterDictionary(Dictionary<string, object> secondaryDictionary)
        {
            DefaultParameterAnalytics defaultParameterAnalytics = new DefaultParameterAnalytics();
            string primaryJson = JsonConvert.SerializeObject(defaultParameterAnalytics);
            // convert primary JSON into string, object pairs
            Dictionary<string, object> primaryJsonDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(primaryJson);
            //merging two dictionaries of string object pairs.
            Dictionary<string, object> requiredDict = Utilities.DataConverter.MergedDictionaryObject(secondaryDictionary, primaryJsonDictionary);
            InternalLogger.LogError(JsonConvert.SerializeObject(requiredDict));
            return requiredDict;
        }
        #endregion Default Analytics Parameter 

        internal string GetMergedString(string primaryJson, string tempJson)
        {

            primaryJson = primaryJson.Replace('}', ',');
            tempJson = tempJson.Replace('{', ' ');


            string mergerString = string.Concat(primaryJson, tempJson);
            mergerString = mergerString.Trim();
            InternalLogger.LogError(mergerString);
            return mergerString;
        }

        internal List<string> GetPurchaseCategoryData(PurchaseCategory purchaseCategory)
        {
            FieldInfo[] newFields = purchaseCategory.GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Instance);
            PurchaseCategories purchaseCast = new PurchaseCategories();
            List<string> subPurchaseCategoryType = purchaseCast.GetPurchaseCatagory((int)newFields[0].GetValue(purchaseCategory), (int)newFields[1].GetValue(purchaseCategory));

            return subPurchaseCategoryType;
        }

        /// <summary>
        /// Function used fro sending analytics data to unity dashboard.
        /// </summary>
        /// <param name="eventName"></param>
        /// <param name="mergedDictionary"></param>
        internal void SendUnityAnaltyicsEvent(string eventName, Dictionary<string, object> mergedDictionary = null)
        {
#if UNITY_ANALYTICS_ADDED
            UnityAnalyticsServicesHandler.Instance.SendUnityAnaltyicsEvent(eventName, mergedDictionary);
#endif
        }
    }
}
public class PurchaseInfoData
{
    public string skuName;
    public string skuDescription;
    public PurchaseCategory purchaseCategory;
}
#endif
