using UnityEngine;

namespace EntertainmentFramework.Api
{
    public class Config : Singleton<Config>
    {
        #region Serialized Fields

        [SerializeField] private Configuration configuration;

        #endregion Serialized Fields

        public class Api
        {
            public static string Host
            { get { return Instance.configuration.Api.Host; } }
        }
    }
}