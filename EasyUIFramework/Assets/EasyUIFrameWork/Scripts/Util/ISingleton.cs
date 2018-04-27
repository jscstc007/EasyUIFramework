using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 一个简易单例模板
/// </summary>
/// <typeparam name="T"></typeparam>
public class ISingleton<T> where T : new(){

    private static T instance;
    public static T Instance
    {
        get
        {
            if (null == instance)
            {
                instance = new T();
            }
            return instance;
        }
    }
}
