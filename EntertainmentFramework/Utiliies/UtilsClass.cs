using EntertainmentFramework;
#if MIKROS_ADDED
using MikrosClient;
#endif
using System;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

namespace Entertainment.Utility
{
    public sealed class UtilsClass
    {
        //Fixed character limit for a custom event name
        private const int eventNameCharacterLimit = 40;

        //Fixed character limit for a Username.
        private const int minimumUsernameLength = 6;
        private const int maximumUsernameLength = 30;

        /// <summary>
        /// Generate current UTC time.
        /// </summary>
        /// <returns>Current UTC time as string.</returns>
        internal static string CurrentUTCTime()
        {
            return DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Generate current local time.
        /// </summary>
        /// <returns></returns>
        internal static string CurrentLocalTime()
        {
            return DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Generate a random sequence of alpha-numeric characters.
        /// </summary>
        /// <param name="length">Length of the string to be generated.</param>
        /// <returns>Random sequence of alpha-numeric characters.</returns>
        internal static string GetRandomAlphanumericSequence(int length = 6)
        {
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        /// <summary>
        /// Check for error in signin email/username input.
        /// </summary>
        /// <param name="userInput">Input of user.</param>
        /// <returns>Details of error in signin email/username input (if any).</returns>
        internal static string SigninUsernameEmailError(string userInput)
        {
            if (string.IsNullOrEmpty(userInput))
            {
                return Constants.ValueRequiredMessage;
            }
            else
            {               
                string username = "", email = "";
                (username, email) = DetermineUsernameOrEmail(userInput.Trim(), true);
                if (string.IsNullOrEmpty(username) && string.IsNullOrEmpty(email))
                {
                    return "Invalid Email/Username format.";
                }
                else
                {
                    return string.Empty;
                }
            }
        }
        /// <summary>
        /// Check for error in signin password input.
        /// </summary>
        /// <param name="userInput">Input of user.</param>
        /// <returns>Details of error in signin password input (if any).</returns>
        internal static string SigninPasswordError(string userInput)
        {
            if (string.IsNullOrEmpty(userInput))
            {
                return Constants.ValueRequiredMessage;
            }
            else if (string.IsNullOrWhiteSpace(userInput))
            {
                return "The password you submitted contains spaces.";
            }
            else
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Check for error in signup username input.
        /// </summary>
        /// <param name="userInput">Input of user.</param>
        /// <returns>Details of error in signup username input (if any).</returns>
        internal static string SignupUsernameError(string userInput, bool enableUsernameSpecialCharacters)
        {
            string errorMessage = string.Empty;
            if (string.IsNullOrEmpty(userInput))
            {
                return Constants.ValueRequiredMessage;
            }
            else if (!ValidateUsername(userInput, enableUsernameSpecialCharacters, out errorMessage))
            {
                return errorMessage;
            }
            else
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Checks whether username or email is been given as input by user.
        /// </summary>
        /// <param name="userInput">Input of user.</param>
        /// <returns>Corresponding (username, email) set.</returns>
        public static (string, string) DetermineUsernameOrEmail(string userInput, bool isEnableUsernameSpecialCharacters)
        {
            string errorMessage = string.Empty;
            if (ValidateEmail(userInput))
            {
                return ("username", userInput);
            }
            else if (ValidateUsername(userInput, isEnableUsernameSpecialCharacters, out errorMessage))
            {
                return (userInput, "");
            }
            else
            {
                return ("", "");
            }
        }

        /// <summary>
        /// Validate email.
        /// </summary>
        /// <param name="email">Email string to check.</param>
        /// <returns>True if valid email, else False.</returns>
        internal static bool ValidateEmail(string email)
        {
            Regex mailValidator = new Regex(@"^((([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+(\.([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+)*)|((\x22)((((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(([\x01-\x08\x0b\x0c\x0e-\x1f\x7f]|\x21|[\x23-\x5b]|[\x5d-\x7e]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(\\([\x01-\x09\x0b\x0c\x0d-\x7f]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]))))*(((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(\x22)))@((([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.)+(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.?$");
            if (string.IsNullOrEmpty(email) || string.IsNullOrWhiteSpace(email))
            {
                return false;
            }
            if (mailValidator.IsMatch(email))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Validate Username.
        /// </summary>
        /// <param name="username">Username string to check.</param>
        /// <param name="enableUsernameSpecialCharacters">c.</param>
        /// <returns>True if valid username, else False.</returns>
        internal static bool ValidateUsername(string username, bool enableUsernameSpecialCharacters, out string errorMessage)
        {
            errorMessage = String.Empty;
            if (username.Length >= minimumUsernameLength && username.Length <= maximumUsernameLength)
            {
                bool isAllLettersDigits = username.All(e => char.IsLetterOrDigit(e));
                if (enableUsernameSpecialCharacters)
                {
                    return true;
                }
                else
                {
                    if (!isAllLettersDigits)
                        errorMessage = "Special characters not allowed in username.";
                    return isAllLettersDigits;
                }
            }
            else
            {
                errorMessage = "Username must contain 6 to 30 alphanumeric characters.";
                return false;
            }
        }
    }
}