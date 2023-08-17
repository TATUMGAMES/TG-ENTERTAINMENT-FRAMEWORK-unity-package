using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.UI;

#if MIKROS_ADDED

using EntertainmentFramework.PresetEvent;
using MikrosClient;
using Achievement = MikrosClient.Analytics.TrackUnlockedAchievementRequest.Achievement;

#endif
#if UNITY_ANALYTICS_ADDED
#endif

namespace EntertainmentFramework.DemoProject
{
    public class AnalyticsPresetEventHandler : MonoBehaviour
    {
        [SerializeField] private InputField eventNameInputField;

        /// <summary>
        /// Tracks Game Over Event.
        /// <see href="https://developer.tatumgames.com/documentation/log-preset-events#track-game-over"> Used to track game is over.</see>
        /// </summary>
        public void TrackGameOver()
        {
#if MIKROS_ADDED
            PopupHandler.Instance.ShowLoader();
            PresetEventHandler.Instance.TrackGameOver(OnSuccessCallBack, OnFailureCallback);
#endif
        }

        /// <summary>
        /// Tracks Level End Preset Event.
        /// <see href="https://developer.tatumgames.com/documentation/log-preset-events#track-level-end"> Used to track when a level has ended.</see>
        /// </summary>
        public void TrackLevelEnd()
        {
#if MIKROS_ADDED
            PopupHandler.Instance.ShowLoader();
            PresetEventHandler.Instance.TrackLevelEnd(2, 3, "Level 2", "Find the GoldenSword", 25, true, OnSuccessCallBack, OnFailureCallback);
#endif
        }

        /// <summary>
        /// Tracks Level Start Preset Event.
        /// <see href="https://developer.tatumgames.com/documentation/log-preset-events#track-level-start">Used to track when a level has started.</see>
        /// </summary>
        public void TrackLevelStart()
        {
#if MIKROS_ADDED
            PopupHandler.Instance.ShowLoader();
            PresetEventHandler.Instance.TrackLevelStart(2, 3, "Level 2", "Find the GoldenSword", OnSuccessCallBack, OnFailureCallback);
#endif
        }

        /// <summary>
        /// Tracks Level Up Preset Event.
        /// <see href="https://developer.tatumgames.com/documentation/log-preset-events#track-level-up">Tracks when a user has advanced a level.</see>
        /// </summary>
        public void TrackLevelUp()
        {
#if MIKROS_ADDED
            PopupHandler.Instance.ShowLoader();
            PresetEventHandler.Instance.TrackLevelUp(2, 3, "Level 2", "Nattu-Lal", "", OnSuccessCallBack, OnFailureCallback);
#endif
        }

        /// <summary>
        /// Tracks Post Score Preset Event.
        /// <see href="https://developer.tatumgames.com/documentation/log-preset-events#track-post-score">Track when a user update score.</see>
        /// </summary>
        public void TrackPostScore()
        {
#if MIKROS_ADDED
            PopupHandler.Instance.ShowLoader();
            PresetEventHandler.Instance.TrackPostScore(10, 3, 1, "Find the Knife", "Johnny", OnSuccessCallBack, OnFailureCallback);
#endif
        }

        /// <summary>
        /// Tracks Share Preset Event.
        /// <see href="https://developer.tatumgames.com/documentation/log-preset-events#track-content-share">track the share event.</see>
        /// </summary>
        public void TrackShare()
        {
#if MIKROS_ADDED
            PopupHandler.Instance.ShowLoader();
            PresetEventHandler.Instance.TrackShare("Facebook", ContentType.Text, OnSuccessCallBack, OnFailureCallback);
#endif
        }

        /// <summary>
        /// Tracks SignIn Preset Event.
        /// <see href="https://developer.tatumgames.com/documentation/log-preset-events#track-signin-request">Track Signing when user Sign In.</see>
        /// </summary>
        public void TrackSignin()
        {
#if MIKROS_ADDED
            PopupHandler.Instance.ShowLoader();
            PresetEventHandler.Instance.TrackSignIn("Facebook", OnSuccessCallBack, OnFailureCallback);
#endif
        }

        /// <summary>
        /// Tracks Sign Up Preset Event.
        /// <see href="https://developer.tatumgames.com/documentation/log-preset-events#track-signup-request">Tracks when a user signs up.</see>
        /// </summary>
        public void TrackSignup()
        {
#if MIKROS_ADDED
            PopupHandler.Instance.ShowLoader();
            PresetEventHandler.Instance.TrackSignUp("Facebook", OnSuccessCallBack, OnFailureCallback);
#endif
        }

        /// <summary>
        /// Tracks Start Timer Preset Event.
        /// <see href="https://developer.tatumgames.com/documentation/log-preset-events#track-timed-events-start">Track when the level starts.</see>
        /// </summary>
        public void TrackStartTimer()
        {
#if MIKROS_ADDED
            PopupHandler.Instance.ShowLoader();
            PresetEventHandler.Instance.TrackStartTimer("Level 1", OnSuccessCallBack, OnFailureCallback);
#endif
        }

        /// <summary>
        /// Tracks Stop Timer Preset Event.
        /// <see cref=" https://developer.tatumgames.com/documentation/log-preset-events#track-timed-events-stop">Track when the level ends.</see>
        /// </summary>
        public void TrackStopTimer()
        {
#if MIKROS_ADDED
            PopupHandler.Instance.ShowLoader();
            PresetEventHandler.Instance.TrackStopTimer("Level 2", OnSuccessCallBack, OnFailureCallback);
#endif
        }

        /// <summary>
        /// Tracks Tutorial Begin Preset Event.
        /// <see href="https://developer.tatumgames.com/documentation/log-preset-events#track-tutorial-begin">Track when user start turorial.</see>
        /// </summary>
        public void TrackTutorialBegin()
        {
#if MIKROS_ADDED
            PopupHandler.Instance.ShowLoader();
            PresetEventHandler.Instance.TrackTutorialBegin(OnSuccessCallBack, OnFailureCallback);
#endif
        }

        /// <summary>
        /// Tracks Tutorial Completed Preset Event.
        /// <see href="https://developer.tatumgames.com/documentation/log-preset-events#track-tutorial-complete">Track when user complete turorial</see>
        /// </summary>
        public void TrackTutorialComplete()
        {
#if MIKROS_ADDED
            PopupHandler.Instance.ShowLoader();
            PresetEventHandler.Instance.TrackTutorialComplete(OnSuccessCallBack, OnFailureCallback);
#endif
        }

        /// <summary>
        /// Tracks Unlocked Achievements Preset Event.
        /// <see href="https://developer.tatumgames.com/documentation/log-preset-events#track-unlocked-achievement">Tracks Unlocked Achievements Event</see>
        /// </summary>
        public void TrackUnlockedAchievement()
        {
#if MIKROS_ADDED
            PopupHandler.Instance.ShowLoader();
            List<Achievement> achievementData = new List<Achievement>();

            Achievement achievementData1 = Achievement.Builder()
                .AchievementId("Unlock the beast")
                .AchievementName("Character Unlocked")
                .Create();

            Achievement achievementData2 = Achievement.Builder()
                        .AchievementId("Unlock the Sanatorium")
                        .AchievementName("Character Unlocked")
                        .Create();

            achievementData.Add(achievementData1);
            achievementData.Add(achievementData2);

            PresetEventHandler.Instance.TrackUnlockedAchievement(achievementData, OnSuccessCallBack, OnFailureCallback);
#endif
        }

        /// <summary>
        /// Tracks Screen Time Preset Event.
        /// <see href="https://developer.tatumgames.com/documentation/log-preset-events#track-screen">Track user Screen time</see>
        /// </summary>
        public void TrackScreenTime()
        {
#if MIKROS_ADDED
            PopupHandler.Instance.ShowLoader();
            PresetEventHandler.Instance.TrackScreenTime("InAppScreen", "MainScene", 20, OnSuccessCallBack, OnFailureCallback);
#endif
        }

        /// <summary>
        /// Tracks Purchase Request Preset Event.
        /// <see href="https://developer.tatumgames.com/documentation/log-preset-events#track-purchase">Tracks user purchase</see>
        /// In this instance, the terms SkuType refers to items like currency, bundle, character skin,
        /// weapons, armor, ect.    SkuSubType is used to provide more details on item purchased
        /// </summary>
        public void TrackPurchaseRequestEvent()
        {
#if MIKROS_ADDED
            PopupHandler.Instance.ShowLoader();
            List<PurchaseInfoData> purchaseDetails = new List<PurchaseInfoData>();

            PurchaseInfoData purchaseInfoData1 = new PurchaseInfoData
            {
                skuName = "Coins",
                skuDescription = "50 Coins",
                purchaseCategory = PurchaseCategory.Currency.COINS
            };

            PurchaseInfoData purchaseInfoData2 = new PurchaseInfoData
            {
                skuName = "Thor Hammer",
                skuDescription = "Produce thunder with lighting",
                purchaseCategory = PurchaseCategory.Weapon.HAMMER
            };

            purchaseDetails.Add(purchaseInfoData1);
            purchaseDetails.Add(purchaseInfoData2);

            PresetEventHandler.Instance.TrackPurchaseRequestEvent("Currency", "Buy Coins", PurchaseCategory.Currency.GOLD, PurchaseType.IN_APP,
            PurchaseCurrencyType.USD, 30.0f, purchaseDetails, 0, 0, OnSuccessCallBack, OnFailureCallback);
#endif
        }

        /// <summary>
        /// Tracks Player Rating Request Preset Event.
        /// <see href="https://developer.tatumgames.com/documentation/log-preset-events#track-player-rating">Tracks user purchase</see>
        /// </summary>
        public void TrackPlayerRating()
        {
#if MIKROS_ADDED
            PopupHandler.Instance.ShowLoader();
            List<ParticipantData> participants = new List<ParticipantData>();
            ParticipantData participantData1 = new ParticipantData
            {
                userName = "Test101",
                email = "test101@test.com",
                participantBehavior = PlayerBehavior.TROLLING
            };
            ParticipantData participantData2 = new ParticipantData
            {
                userName = "Test101",
                email = "test101@test.com",
                participantBehavior = PlayerBehavior.OFFENSIVE_LANGUAGE
            };
            participants.Add(participantData1);
            participants.Add(participantData2);
            PresetEventHandler.Instance.TrackPlayerRating(participants, OnSuccessCallBack, OnFailureCallback);
#endif
        }

        /// <summary>
        /// Tracks handled exceptions.
        /// </summary>
        public void TrackException(System.Exception exception)
        {
#if MIKROS_ADDED
            PopupHandler.Instance.ShowLoader();
            PresetEventHandler.Instance.TrackException(exception, OnSuccessCallBack, OnFailureCallback);
#endif
        }

        /// <summary>
        /// Code snippet for tracking the Exception.
        /// </summary>
        internal void ForceException()
        {
            try
            {
                // Create random exceptions here
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                FileStream file = File.Open("D:/Work/x.cs", FileMode.Open);
                string t_ReadString = (string)binaryFormatter.Deserialize(file);
                file.Close();
            }
            catch (System.Exception e)
            {
                TrackException(e);
            }
        }

        /// <summary>
        /// Log a custom event set by user.
        /// Custom events can be made by the developer for anything not already provided
        /// in the preset events.
        /// <see href="https://developer.tatumgames.com/documentation/log-custom-events"> MIKROS Custom Events Documentation</see>
        /// </summary>
        public void LogCustomEvent()
        {
#if MIKROS_ADDED
            PresetEventHandler.Instance.TrackCustom(eventNameInputField.text, OnSuccessCallBack, OnFailureCallback);
#endif
        }

        /// <summary>
        /// Track some sample custom events.
        /// <see href="https://developer.tatumgames.com/documentation/log-custom-events">MIKROS Custom Samples Documentation</see>
        /// </summary>
        public void TrackCustom()
        {
#if MIKROS_ADDED
            List<Hashtable> customEvents = new List<Hashtable>();
            for (int i = 1; i <= 10; i++)
            {
                PresetEventHandler.Instance.TrackCustom(Constants.NEW_CUSTOM_EVENT + i, Constants.NEW_TEST_PARAM, Constants.NEW_TEST_VALUE, OnSuccessCallBack, OnFailureCallback);
            }
#endif
        }

        /// <summary>
        /// Flush events on button click. Flush refers to when information in a buffer becomes written to storage
        /// </summary>
        public void FlushEvents()
        {
#if MIKROS_ADDED
            PresetEventHandler.Instance.FlushEvents();
#endif
        }

        private void OnSuccessCallBack()
        {
            PopupHandler.Instance.ShowPopup(Constants.SUCCESS);
            PopupHandler.Instance.HideLoader();
        }

        private void OnFailureCallback(string message)
        {
            PopupHandler.Instance.ShowPopup(message);
            PopupHandler.Instance.HideLoader();
        }
    }
}