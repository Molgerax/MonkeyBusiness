using UnityEngine;

namespace BFB.Core.Singletons
{
    public abstract class SingletonScriptableObject<T> : ScriptableObject where T : ScriptableObject
    {
        private static T _instance;

        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = Resources.Load<T>(typeof(T).ToString());

                    if (_instance == null)
                    {
                        _instance = ScriptableObject.CreateInstance<T>();
                    }

                    (_instance as SingletonScriptableObject<T>).OnInitialize();
                }

                return _instance;
            }
        }

        protected virtual void OnInitialize() {}
    }
}