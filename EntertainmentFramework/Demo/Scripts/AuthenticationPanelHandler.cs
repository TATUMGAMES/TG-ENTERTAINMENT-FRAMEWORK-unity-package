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

        [SerializeField] private InputField forgotPasswordEmailUsername;

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
            AuthEventHandler.Instance.MikrosSignup(signUpUsername.text, signUpEmail.text, signUpPassword.text,true, OnAuthSuccess);
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
        /// Perform Mikros Forgot Password.
        /// </summary>
        public void OnClickForgotPassword()
        {
#if MIKROS_ADDED
            PopupHandler.Instance.ShowLoader();
            AuthEventHandler.Instance.MikrosForgotPassword("",forgotPasswordEmailUsername.text, () =>
            {
                OnForgotPasswordCallbackReceived(ConvertForgotPasswordResponse(Constants.EMAIL_SEND_SUCCESSFULL_RESPONSE));
            }, onFailure =>
            {
                OnForgotPasswordCallbackReceived(ConvertForgotPasswordResponse(onFailure));
            });
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

        /// <summary>
        /// Callback for handling success from Mikros Forgot Password.
        /// </summary>
        /// <param name="message">Message for showing the status </param>
        private void OnForgotPasswordCallbackReceived(string message= null)
        {
            PopupHandler.Instance.HideLoader();
            PopupHandler.Instance.ShowPopup(message);
        }

        /// <summary>
        /// Convert Forgot Password Response is method that is converting the response 
        /// message to our own type of message that developer can show to the user.
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public string ConvertForgotPasswordResponse(string message)
        {
            string returedMessage;
            switch (message)
            {
                case Constants.EMAIL_SEND_SUCCESSFULL_RESPONSE:
                    returedMessage = Constants.EMAIL_SEND_SUCCESSFULL_MESSAGE;
                    break;

                case Constants.WRONG_EMAIL_RESPONSE:
                    returedMessage = Constants.WRONG_EMAIL_MESSAGE;
                    break;

                case Constants.LIMIT_EXCEEDED_RESPONSE:
                    returedMessage = Constants.LIMIT_EXCEEDED_MESSAGE;
                    break;

                default:
                    returedMessage = message.Replace("_", " ");
                    break;
            }
            return returedMessage;
        }
    }
}