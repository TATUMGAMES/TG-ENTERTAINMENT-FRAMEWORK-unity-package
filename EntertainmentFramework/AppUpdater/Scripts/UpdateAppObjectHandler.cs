using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

namespace EntertainmentFramework.InGameUpdate
{
    public class UpdateAppObjectHandler : MonoBehaviour
    {
        public static Action OnUpdateLaterAction;

        [SerializeField] private Button updateButton;
        [SerializeField] private Button updateLaterButton;
        [SerializeField] private TMP_Text messageText;
        [SerializeField] private TMP_Text versionText;
 private string appStoreUrl;

        public void SetPromptData(bool isForceUpdate, string storeUrl, string versionNumber, string updatedVersionNumber)
        {
            appStoreUrl = storeUrl;
            updateButton.onClick.AddListener(() => OpenAppstorePage());
            versionText.text = "v " + versionNumber;
            messageText.text = "A new version (v " + updatedVersionNumber + ") was detected that needs to be updated.";
            if (isForceUpdate)
                updateLaterButton.gameObject.SetActive(true);
            updateLaterButton.onClick.AddListener(() =>
            {
                OnUpdateLaterAction?.Invoke();
                this.gameObject.SetActive(false);
            });
        }

        public void OpenAppstorePage()
        {
            Application.OpenURL(appStoreUrl);
        }
    }
}
