using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Component
{
    private static T _instance;
    
    // public static T Instance
    // {
    //     get {
    //         if (instance == null)
    //         {
    //             GameObject obj = new GameObject();
    //             obj.name = typeof(T).Name;
    //             obj.hideFlags = HideFlags.HideAndDontSave;
    //             instance = obj.AddComponent<T>();
    //         }
    //         
    //         return instance;
    //     }
    // }

    public static T Instance => _instance;
    
    protected virtual void Awake()
    {
        if (_instance == null)
        {
            _instance = this as T;
        }
        else
        {
            Destroy(this);
        }
    }

    private void OnDestroy()
    {
        if (_instance == this)
        {
            _instance = null;
        }
    }
}
