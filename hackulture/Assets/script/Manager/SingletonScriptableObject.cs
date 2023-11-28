    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public abstract class SingletonScriptableObject<T> : ScriptableObject where T : ScriptableObject
    {
        private static T _instance = null;
        public void Awake()
        {
            Debug.Log("Awake called for SingletonScriptableObject: " + typeof(T).ToString());
            DontDestroyOnLoad(this);
        }
        /*public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = Resources.Load<T>("Path/To/Your/ScriptableObject") as T;
                    if (_instance == null)
                    {
                        Debug.LogError("SingletonScriptableObject -> Instance -> Unable to load " + typeof(T).ToString() + ".");
                    }
                }
                return _instance;
            }
        }*/
    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                T[] results = Resources.LoadAll<T>("");
                if (results.Length == 0)
                {
                    Debug.LogError("SingletonScriptableObject -> Instance -> results length is 0 for type " + typeof(T).ToString() + ".");
                    return null;
                }
                if (results.Length > 1)
                {
                    Debug.LogError("SingletonScriptableObject -> Instance -> results length is greater than 1 for type " + typeof(T).ToString() + ".");
                    return null;
                }
                _instance = results[0];
            }
            return _instance;
        }
    }
}
