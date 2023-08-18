using EntertainmentFramework.Extension;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace EntertainmentFramework.Utils.Rest
{
    public class WebRequestBuilder : IDisposable
    {
        private UnityWebRequest webRequest;

        private string url;
        private string verb;
        private Dictionary<string, string> requestHeaders = new Dictionary<string, string>();
        private Dictionary<string, object> formData = new Dictionary<string, object>();
        private DownloadHandler downloadHandler = new DownloadHandlerBuffer();
        private UploadHandler uploadHandler;
        public WebRequestBuilder()
        {
            Verb(Verbs.GET);
        }

        /// <summary>
        /// Functions
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public WebRequestBuilder Url(string url)
        {
            this.url = url;
            return this;
        }

        /// <summary>
        /// Sets the call's method (GET, POST, etc) a.k.a verb
        /// </summary>
        public WebRequestBuilder Verb(Verbs verb)
        {
            this.verb = verb.ToString();
            return this;
        }

        /// <summary>
        ///  Attaches Header to the rest call.
        /// </summary>
        /// <param name="name">Header Key</param>
        /// <param name="value">header value</param>
        /// <returns>WebRequestBuilder</returns>
        public WebRequestBuilder Header(string name, string value)
        {
            if (requestHeaders.ContainsKey(name))
                requestHeaders[name] = value;
            else
                requestHeaders.Add(name, value);

            return this;
        }

        /// <summary>
        /// Attaches Header to the rest call.
        /// </summary>
        /// <param name="headers">Dictionary for header key and value</param>
        /// <returns>WebRequestBuilder</returns>
        public WebRequestBuilder Headers(IDictionary<string, string> headers)
        {
            foreach (var entry in headers)
                Header(entry.Key, entry.Value);
            return this;
        }

        /// <summary>
        /// Attach Content to header to the rest call.
        /// </summary>
        /// <param name="type">string </param>
        /// <returns></returns>
        public WebRequestBuilder ContentType(string type)
        {
            return Header("Content-Type", type);
        }

        /// <summary>
        /// Attaches data to the rest call.
        /// </summary>
        /// <param name="data">The data to be sent as a byte array</param>
        /// <param name="mimeType">Leave null to send as binary data or see <code>OmegaTech.Utils.Rest.ContentTypes</code></param>
        public WebRequestBuilder Data(byte[] data, string mimeType = null)
        {
            if (uploadHandler == null)
                uploadHandler = new UploadHandlerRaw(data);

            uploadHandler.contentType = mimeType ?? ContentTypes.BINARY;

            return this;
        }

        /// <summary>
        /// Attaches string data to the rest call.
        /// </summary>
        /// <param name="data">The data to be sent as a string</param>
        /// <param name="mimeType">Leave null to send as plain text data or see <code>OmegaTech.Utils.Rest.ContentTypes</code></param>
        public WebRequestBuilder Data(string data, string mimeType = null)
        {
            return Data(data.GetBytes(), mimeType ?? ContentTypes.TEXT);
        }

        /// <summary>
        /// Attaches multipart form data to the rest call.
        /// </summary>
        /// <param name="name">The key of the data value</param>
        /// <param name="data">The actual data</param>
        public WebRequestBuilder FormData(string name, string data)
        {
            if (formData.ContainsKey(name))
                formData[name] = data;
            else
                formData.Add(name, data);
            return this;
        }

        /// <summary>
        /// Attaches multipart form data to the rest call.
        /// </summary>
        /// <param name="name">The key of the data value</param>
        /// <param name="data">The actual data</param>
        public WebRequestBuilder FormData(string name, FileData data)
        {
            if (formData.ContainsKey(name))
                formData[name] = data;
            else
                formData.Add(name, data);
            return this;
        }

        /// <summary>
        /// Attaches multipart form data to the rest call.
        /// </summary>
        /// <param name="name">The key of the data value</param>
        /// <param name="data">The actual data</param>
        public WebRequestBuilder FormData(string name, bool data)
        {
            if (formData.ContainsKey(name))
                formData[name] = data;
            else
                formData.Add(name, data);
            return this;
        }

        /// <summary>
        /// Attaches multipart form data to the rest call.
        /// </summary>
        /// <param name="name">The key of the data value</param>
        /// <param name="data">The actual data</param>
        public WebRequestBuilder FormData(string name, int data)
        {
            if (formData.ContainsKey(name))
                formData[name] = data;
            else
                formData.Add(name, data);
            return this;
        }

        public WebRequestBuilder Handler(DownloadHandler handler)
        {
            downloadHandler = handler;
            return this;
        }

        /// <summary>
        /// Build function return new object of the UnityWebRequest class.
        /// </summary>
        /// <returns>Return new UnityWebRequest Object</returns>
        internal UnityWebRequest Build()
        {
            if (Verbs.POST.ToString().Equals(verb))
            {
                var formData = new WWWForm();

                foreach (var item in this.formData)
                {
                    if (item.Value is int)
                    {
                        formData.AddField(item.Key, Convert.ToInt32(item.Value));
                    }
                    else if (item.Value is string)
                    {
                        formData.AddField(item.Key, item.Value.ToString());
                    }
                    else if (item.Value is FileData)
                    {
                        var data = (FileData)item.Value;
                        formData.AddBinaryData(item.Key, data.Bytes, data.Filename, data.Mime);
                    }
                    else if (item.Value is bool)
                    {
                        formData.AddField(item.Key, ((bool)item.Value) ? "1" : "0");
                    }
                }

                webRequest = UnityWebRequest.Post(url, formData);
            }
            else if (Verbs.DELETE.ToString().Equals(verb))
            {
                webRequest = UnityWebRequest.Delete(url);
            }
            else
            {
                webRequest = new UnityWebRequest();
                webRequest.url = url;
            }

            if (downloadHandler == null)
                downloadHandler = new DownloadHandlerBuffer();

            webRequest.downloadHandler = downloadHandler;

            if (uploadHandler != null)
                webRequest.uploadHandler = uploadHandler;

            foreach (var item in requestHeaders)
                webRequest.SetRequestHeader(item.Key, item.Value);

            return webRequest;
        }

        public void Dispose()
        {
            webRequest?.Dispose();
        }

        public struct FileData
        {
            public byte[] Bytes;
            public string Filename;
            public string Mime;
        }
    }
}