using EntertainmentFramework.InternalLoggers;
using UnityEngine;

namespace EntertainmentFramework
{
    /// <summary>
    /// Inherit this class from any unity component class to make that class Singleton
    /// isPersistent - true to make the gameobject not destroyable through out the game
    /// </summary>
    /// <typeparam name="T">The Child Component</typeparam>
    public class Singleton<T> : MonoBehaviour where T : Component
    {
        private static bool IsQuiting = false;
        private static T instance;

        public static T Instance
        {
            get
            {
                if (IsQuiting)
                {
                    return null;
                }

                if (!instance)
                {
                    instance = FindObjectOfType<T>();
                    if (!instance)
                    {
                        GameObject obj = new GameObject
                        {
                            name = typeof(T).Name
                        };
                        instance = obj.AddComponent<T>();
                    }
                }
                return instance;
            }
        }

        public bool IsPersistent = false;

        private string instanceName = typeof(T).Name;

        protected virtual void Awake()
        {
            if (Instance == null)
            {
                InternalLogger.Log(instanceName + " Instance Missing. Creating new Instance of " + instanceName + ".");
                instance = this as T;
                InternalLogger.Log(instanceName + " Instance created successfully!:).");
            }
            else if (Instance != this)
            {
                InternalLogger.Log(instanceName + " already exists!...");
                if (this.gameObject)
                    Destroy(this.gameObject);
                return;
            }

            if (IsPersistent)
            {
                DontDestroyOnLoad(gameObject);
            }
        }

        protected virtual void OnApplicationQuit()
        {
            IsQuiting = true;
        }
    }
}