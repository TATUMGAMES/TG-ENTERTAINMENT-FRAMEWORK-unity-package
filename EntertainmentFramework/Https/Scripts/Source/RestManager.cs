using EntertainmentFramework;
using EntertainmentFramework.Api;
using EntertainmentFramework.InternalLoggers;
using EntertainmentFramework.Utilities;
using EntertainmentFramework.Utils.Rest;
using System;
using RestError = EntertainmentFramework.Utils.RestUtil.RestCallError;
using RestUtil = EntertainmentFramework.Utils.RestUtil;

namespace EntertainmentFramework
{
    public class RestManager : Singleton<RestManager>
    {
        private RestUtil restUtil;
        private static string ClientAccessToken
        {
            get;
            set;
        }
        
        private static string UserAccessToken
        {
            get;
            set;
        }

        private static string UserRefreshToken
        {
            get;
            set;
        }

        private void Awake()
        {
            restUtil = RestUtil.Initialize(this);
            base.Awake();
        }

        /// <summary>
        /// Functions to send Web Request.
        /// </summary>
        /// <param name="builder"> WebRequestBuilder</param>
        /// <param name="onCompletion">Action</param>
        /// <param name="onError">Action</param>
        public static void SendWebRequest(WebRequestBuilder builder, Action onCompletion, Action<RestError> onError)
        {
            Instance.restUtil.Send(builder, handler => { onCompletion?.Invoke(); },
                restError => InterceptError(restError, () => onError?.Invoke(restError), onError));
        }

        /// <summary>
        /// Functions to send Web Request.
        /// </summary>
        /// <typeparam name="T">where T is generic</typeparam>
        /// <param name="builder">WebRequestBuilder</param>
        /// <param name="onCompletion">Action </param>
        /// <param name="onError">Action</param>
        public static void SendWebRequest<T>(WebRequestBuilder builder, Action<T> onCompletion,
            Action<RestError> onError = null)
        {
            Instance.restUtil.Send(builder,
                handler =>
                {                   
                    var response = DataConverter.DeserializeObject<ApiResponseFormat<T>>(handler.text);
                    onCompletion?.Invoke(response.Result);
                },
                restError => InterceptError(restError, () => onError?.Invoke(restError), onError));
        }

        private static void InterceptError(RestError error, Action onSuccess,
            Action<RestError> defaultOnError)
        {
            defaultOnError?.Invoke(error);
        }

        /// <summary>
        /// Get Api full Utl path.
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string GetApiUrl(string path)
        {
            return $"{Config.Api.Host}{path}";
        }

        /// <summary>
        /// Format the APi Url with agrs.
        /// </summary>
        /// <param name="path"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static string FormatApiUrl(string path, params object[] args)
        {
            return string.Format($"{Config.Api.Host}{path}", args);
        }

        /// <summary>
        /// Function to Add Auth Token.
        /// </summary>
        /// <param name="builder"></param>
        public static void AddUserAuthHeader(ref WebRequestBuilder builder, string userAccessToken)
        {
            UserAccessToken = userAccessToken;
            builder.Header("Authorization", $"Bearer {UserAccessToken}");
        }

        /// <summary>
        /// Function to clinet Auth token.
        /// </summary>
        /// <param name="builder"></param>
        public static void AddClientAuthHeader(ref WebRequestBuilder builder, string clientAcessToken)
        {
            ClientAccessToken = clientAcessToken;
            builder.Header("Authorization", $"Bearer {ClientAccessToken}");
        }
    }
}