#define DEBUG_REST_CALLS

using EntertainmentFramework.Api;
using EntertainmentFramework.InternalLoggers;
using EntertainmentFramework.Utilities;
using EntertainmentFramework.Utils.Rest;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace EntertainmentFramework.Utils
{
    public class RestUtil
    {
        public UnityWebRequest CurrentRequest => _currentCall.Request;

        private bool Uploading => _currentCall.Request.method.Equals("POST");

        public float Progress
            => Uploading ? _currentCall.Request.uploadProgress : _currentCall.Request.downloadProgress;

        public ulong TransmittedBytes
            => Uploading ? _currentCall.Request.uploadedBytes : _currentCall.Request.downloadedBytes;

        private readonly MonoBehaviour _monoBehaviour;
        private readonly Queue<Call> _callQueue = new Queue<Call>();
        private int _callCounter;
        private Coroutine _coroutine;
        private Call _currentCall;
        private bool _endGracefully;

        private RestUtil(MonoBehaviour monoBehaviour, bool autoStart = true)
        {
            _monoBehaviour = monoBehaviour;

            if (autoStart)
                Start();
        }

        public static RestUtil Initialize(MonoBehaviour monoBehaviour)
        {
            return new RestUtil(monoBehaviour);
        }

        public void Start()
        {
            if (_coroutine == null)
                _coroutine = _monoBehaviour.StartCoroutine(Run());
        }

        /// <summary>
        /// The utility will stop sending requests when the current request has finished.
        /// </summary>
        public void StopGracefully()
        {
            _endGracefully = true;
        }

        public void ForceStop()
        {
            if (_coroutine != null)
                _monoBehaviour.StopCoroutine(_coroutine);
        }

        /// <summary>
        /// Sends a web request over the network.
        /// </summary>
        /// <param name="builder">The builder that contains the web request data.</param>
        /// <param name="onCompletion">Function to be called when the request is completed successfully.</param>
        /// <param name="onError">Function to be called when the request fails.</param>
        /// <returns>An integer representing the number of the queued call.</returns>
        public int Send(WebRequestBuilder builder,
            Action<DownloadHandler> onCompletion, Action<RestCallError> onError)
        {
            _callQueue.Enqueue(new Call()
            {
                Builder = builder,
                OnCompletion = onCompletion,
                OnError = onError
            });

            return _callCounter++;
        }

        private IEnumerator Run()
        {
            do
            {
                do
                {
                    yield return new WaitForEndOfFrame();
                } while (_currentCall == null && _callQueue.Count == 0);

                _currentCall = _callQueue.Dequeue();
                _currentCall.Request = _currentCall.Builder.Build();
#if DEBUG_REST_CALLS
                InternalLogger.Log($"Making {_currentCall.Request.method} call to: {_currentCall.Request.url}");
#endif
                yield return _currentCall.Request.SendWebRequest();

#if DEBUG_REST_CALLS
                InternalLogger.Log($"Call {_currentCall.Request.url} completed with status { _currentCall.Request.responseCode} " +
                    $"and response {_currentCall.Request.downloadHandler.text}");
#endif
                if (_currentCall.Request.responseCode == Constants.HttpOk || _currentCall.Request.responseCode == Constants.HttpCreated)
                {
                    _currentCall.OnCompletion(_currentCall.Request.downloadHandler);
                }
                else
                {
#if DEBUG_REST_CALLS
                    InternalLogger.Log($"Called: {_currentCall.Request.url}\nResponse: { _currentCall.Request.downloadHandler.text}");
#endif
                    var restCallError = new RestCallError()
                    {
                        Raw = _currentCall.Request.downloadHandler.text,
                        Code = _currentCall.Request.responseCode,
                        Headers = _currentCall.Request.GetResponseHeaders(),
                    };
                    var oauthResponse =
                        DataConverter.DeserializeObject<ConfigDataResponseFormat>(restCallError.Raw);
                    if (oauthResponse == null)
                    {
                        restCallError.Error = Constants.NO_CONNECTION_ERROR;
                        restCallError.Description = Constants.NO_CONNECTION_ERROR;
                    }
                    else if (oauthResponse.data != null)
                    {
                        restCallError.Error = oauthResponse.status.statusCode.ToString();
                        restCallError.Description = oauthResponse.status.statusMessage;
                    }
                    else
                    {
                        var deSerializedData =
                            DataConverter.DeserializeObject<ConfigDataResponseFormat>(restCallError.Raw);
                        restCallError.Error = deSerializedData.status.statusCode.ToString();
                        restCallError.Description = deSerializedData.status.statusMessage;
                    }

                    _currentCall.OnError(restCallError);
                }

                _currentCall.Request.Dispose();
                _currentCall = null;
            } while (!_endGracefully);
        }

        public struct RestCallError
        {
            public string Raw;
            public long Code;
            public string Error;
            public string Description;
            public Dictionary<string, string> Headers;
        }

        private class Call
        {
            public WebRequestBuilder Builder;
            public Action<DownloadHandler> OnCompletion;
            public Action<RestCallError> OnError;
            public UnityWebRequest Request;
        }
    }
}