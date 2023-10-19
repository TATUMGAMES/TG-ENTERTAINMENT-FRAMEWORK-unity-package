#if MIKROS_ADDED
using EntertainmentFramework.InternalLoggers;
using MikrosClient;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace EntertainmentFramework.AuthEvents
{
    public class AuthEventHandler
    {
        private static AuthEventHandler authHandlerInstance;

        public static AuthEventHandler Instance
        {
            get
            {
                // instantiate the class if the class object is null.
                if (authHandlerInstance == null)
                {
                    new AuthEventHandler();
                }
                // Return the AuthEventHandler object.
                return authHandlerInstance;
            }
        }

        /// <summary>
        /// Define private constructor for singleton class.
        /// </summary>
        private AuthEventHandler()
        {
            if (authHandlerInstance == null)
            {
                authHandlerInstance = this;
            }
        }

        /// <summary>
        /// Perform Mikros Signin.
        /// </summary>
        /// <param name="username">Username.</param>
        /// <param name="email">Email.</param>
        /// <param name="password">Password.</param>
        /// <param name="callback">Signin success callback.</param>
        /// <see href="https://docs.unity3d.com/ScriptReference/Events.UnityAction.html">Unity Action Documentation</see>
        public void MikrosSignin(string username, string email,
            string password, UnityAction<Dictionary<string, object>> callback = null, UnityAction<string> onFailure = null)
        {
            SigninRequest.Builder()
                .Username(username)
                .Email(email)
                .Password(password)
                .Create(signinRequest =>
                {
                    InternalLogger.Log("All input correct.");

                    MikrosManager.Instance.AuthenticationController.Signin(signinRequest, successResponse =>
                    {
                        InternalLogger.Log("Success");
                        Dictionary<string, object> successDictionary = successResponse.MapToDictionary();
                        callback?.Invoke(successDictionary);
                    },
                    failure => onFailure?.Invoke(failure.Message));
                },
                failure => onFailure?.Invoke(failure.Message));
        }

        /// <summary>
        /// Creates a Mikros account.
        /// </summary>
        /// <param name="username">Username.</param>
        /// <param name="email">Email.</param>
        /// <param name="password">Password.</param>
        /// <param name="callback">Signup success callback.</param>
        /// <see href="https://docs.unity3d.com/ScriptReference/Events.UnityAction.html">Unity Action Documentation</see>
        public void MikrosSignup(string username, string email, string password, UnityAction<Dictionary<string, object>> callback = null, bool enableSpecialCharacterUsername = true, UnityAction<string> onFailure = null)
        {
            SignupRequest.Builder()
                .Username(username)
                .Email(email)
                .Password(password)
                .EnableUsernameSpecialCharacters(enableSpecialCharacterUsername)
                .Create(signupRequest =>
                {
                    MikrosManager.Instance.AuthenticationController.Signup(signupRequest, successResponse =>
                    {
                        InternalLogger.Log("Success");
                        Dictionary<string, object> successDictionary = successResponse.MapToDictionary();
                        callback?.Invoke(successDictionary);
                    },
                    failure => onFailure?.Invoke(failure.Message));
                },
                failure => onFailure?.Invoke(failure.Message));
        }

        /// <summary>
        /// Perform Mikros Signout.
        /// </summary>
        /// <param name="callback">Signout success callback.</param>
        public void MikrosSignout(UnityAction<Dictionary<string, object>> callback = null, UnityAction<string> onFailure = null)
        {
            MikrosManager.Instance.AuthenticationController.Signout(() =>
            {
                InternalLogger.Log("Success");
                callback?.Invoke(null);
            },
            failure => onFailure?.Invoke(failure.Message));
        }

        /// <summary>
        /// Perform Mikros Forgot Password.
        /// </summary>
        /// <param name="username">Username.</param>
        /// <param name="email">Email.</param>
        /// <param name="callback">Forgot password success callback.</param>
        /// <param name="onFailure">Forgot password failure callback.</param>
        /// <see href="https://docs.unity3d.com/ScriptReference/Events.UnityAction.html">Unity Action Documentation</see>
        public void MikrosForgotPassword(string username = "", string email = "", UnityAction callback = null, UnityAction<string> onFailure = null)
        {
            Debug.Log(username + email);
            ForgotPasswordRequest.Builder()
                .Username(username)
                .Email(email)
                .Create(forgotPasswordRequest =>
                {
                    MikrosManager.Instance.AuthenticationController.ForgotPassword(forgotPasswordRequest, () =>
                    {
                        callback?.Invoke();
                    }, failure =>
                    {
                        onFailure?.Invoke(failure.Message);
                    });
                },
               failure => onFailure?.Invoke(failure.Message));
        }

        /// <summary>
        /// Launches the MIKROS SSO panel.
        /// </summary>
        /// <param name="mikrosUserAction"></param>
        public void OnClickMikrosSignin(Action<MikrosUser> mikrosUserAction = null)
        {
            MikrosSSORequest mikrosSSORequest = MikrosSSORequest.Builder().EnableUsernameSpecialCharacters(true).MikrosUserAction(mikrosUserAction).Create();
            MikrosManager.Instance.AuthenticationController.LaunchSignin(mikrosSSORequest);
        }

        /// <summary>
        /// Checks if there is any user.
        /// </summary>
        /// <returns></returns>
        public bool CheckForLoginStatus()
        {
            return MikrosManager.Instance.AuthenticationController.MikrosUser != null;
        }
    }
}
#endif