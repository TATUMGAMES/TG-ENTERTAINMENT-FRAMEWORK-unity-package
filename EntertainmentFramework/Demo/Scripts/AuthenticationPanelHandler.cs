using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using EntertainmentFramework.InternalLoggers;

#if MIKROS_ADDED

using EntertainmentFramework.AuthEvents;

#endif

namespace EntertainmentFramework.DemoProject
{
    public class AuthenticationPanelHandler : MonoBehaviour
    {
        [SerializeField] private InputField signUpUsername;
        [SerializeField] private InputField signUpEmail;
        [SerializeField] private InputField signUpPassword;

        [SerializeField] private InputField signInUsername;
        [SerializeField] private InputField signInPassword;

        [SerializeField] private GameObject signoutButton;

        private GameObject currentPanel = null;
        [SerializeField] private GameObject homePanel;

        private void Start()
        {
            ShowSignOutButton();
        }

        /// <summary>
        /// Perform Mikros SignUp.
        /// </summary>
        public void OnClickSignup(GameObject currentPanel)
        {
#if MIKROS_ADDED
            this.currentPanel = currentPanel;
            PopupHandler.Instance.ShowLoader();
            AuthEventHandler.Instance.MikrosSignup(signUpUsername.text, signUpEmail.text, signUpPassword.text, OnAuthSuccess);
#endif
        }

        /// <summary>
        /// Perform Mikros Sign Out.
        /// </summary>
        public void OnClickSignout()
        {
#if MIKROS_ADDED
            PopupHandler.Instance.ShowLoader();
            AuthEventHandler.Instance.MikrosSignout(OnAuthSuccess);
#endif
        }

        /// <summary>
        /// Perform Mikros Signin.
        /// </summary>
        public void OnClickSignin(GameObject currentPanel)
        {
#if MIKROS_ADDED
            this.currentPanel = currentPanel;
            PopupHandler.Instance.ShowLoader();
            AuthEventHandler.Instance.MikrosSignin(signInUsername.text, "", signInPassword.text, OnAuthSuccess);
#endif
        }

        /// <summary>
        /// Check the status for the User login and make the signout button enable or disable.
        /// </summary>
        private void ShowSignOutButton()
        {
#if MIKROS_ADDED
            signoutButton.SetActive(AuthEventHandler.Instance.CheckForLoginStatus());
#endif
        }

        /// <summary>
        /// Callback received from the Authentication.
        /// </summary>
        /// <param name="mikrosUserData"></param>
        private void OnAuthSuccess(Dictionary<string, object> mikrosUserData = null)
        {
            PopupHandler.Instance.HideLoader();
#if MIKROS_ADDED
            if (mikrosUserData != null)
            {
                string email = mikrosUserData["Email"].ToString();
                PopupHandler.Instance.ShowPopup(mikrosUserData == null ? "Signed out" : ("Auth Success: " + email));
                currentPanel.SetActive(false);
                homePanel.SetActive(true);
            }
            ShowSignOutButton();
#endif
        }
    }
}