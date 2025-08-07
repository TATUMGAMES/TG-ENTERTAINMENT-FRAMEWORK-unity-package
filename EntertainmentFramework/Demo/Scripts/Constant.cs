using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EntertainmentFramework.DemoProject
{
    internal sealed class Constants
    {
        internal const string SUCCESS = "Success";

        internal const string NEW_CUSTOM_EVENT = "NEW_CUSTOM_EVENT";
        internal const string NEW_TEST_PARAM = "NEW_TEST_PARAM";
        internal const string NEW_TEST_VALUE = "NEW_TEST_VALUE";

        #region Forgot Password Messagges

        internal const string EMAIL_SEND_SUCCESSFULL_MESSAGE = "Reset password link has been sent to your email.";
        internal const string WRONG_EMAIL_MESSAGE = "This email does not exist. Try creating an account through sign up.";
        internal const string LIMIT_EXCEEDED_MESSAGE = "You have exceeded your password reset limit. After 24 hours you can try again.";

        internal const string EMAIL_SEND_SUCCESSFULL_RESPONSE = "SUCCESS";
        internal const string WRONG_EMAIL_RESPONSE = "WRONG_EMAIL";
        internal const string LIMIT_EXCEEDED_RESPONSE = "PASSWORD_RESET_LIMIT_EXCEEDED";

        #endregion Forgot Password Messagges
    }
}