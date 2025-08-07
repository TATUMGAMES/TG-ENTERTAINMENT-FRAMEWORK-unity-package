using UnityEngine;
using UnityEngine.UI;

namespace EntertainmentFramework.DemoProject
{
    public class PopupHandler : MonoBehaviour
    {
        /// <summary>
        /// All Gameobjects.
        /// Serialized Field to show up in inspector of Unity.
        /// </summary>
        [SerializeField] private GameObject loadingPanel;

        [SerializeField] private GameObject popupObject;

        [SerializeField] private Text popupText;

        public static PopupHandler Instance;

        /// <summary>
        /// Awake is called when the script instance is being loaded.
        /// Inactive GameObject is set to active, or after a GameObject created with Object.Instantiate is initialized.
        /// Use Awake to initialize.
        /// <see href="https://docs.unity3d.com/ScriptReference/MonoBehaviour.Awake.html">Unity Awake Documentation</see>
        /// </summary>
        private void Awake()
        {
            if (Equals(Instance, null))
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        /// <summary>
        /// Activates the loader.
        /// </summary>
        public void ShowLoader()
        {
            loadingPanel.SetActive(true);
        }

        /// <summary>
        /// Deactivates the loader.
        /// </summary>
        public void HideLoader()
        {
            loadingPanel.SetActive(false);
        }

        /// <summary>
        /// Activates the popup.
        /// </summary>
        /// <param name="text">Text to display in popup.</param>
        public void ShowPopup(string text)
        {
            popupText.text = text;
            popupObject.SetActive(true);
        }

        /// <summary>
        /// Deactivates the popup.
        /// </summary>
        public void HidePopup()
        {
            popupObject.SetActive(false);
            popupText.text = string.Empty;
        }
    }
}