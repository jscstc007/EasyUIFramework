using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// 资源管理器
/// </summary>
public class ResourceManager : MonoBehaviour {

    #region 单例模式
    private static ResourceManager instance;
    public static ResourceManager Instance
    {
        get
        {
            if (null == instance)
            {
                GameObject go = new GameObject("ResourceManager", typeof(ResourceManager), typeof(Tag_DontDestroyOnLoad));
                instance = go.GetComponent<ResourceManager>();
            }
            return instance;
        }
    }
    #endregion

    /// <summary>
    /// 当前已经加载了的Resources下资源,用于缓存
    /// </summary>
    private Dictionary<string, UnityEngine.Object> LoadedResource;

    //TODO

    /// <summary>
    /// 同步加载一个Resources目录下的文件
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="path"></param>
    /// <returns></returns>
    public T LoadFromResource<T> (string path) where T : UnityEngine.Object
    {
        T resource = Resources.Load<T>(path);
        return resource;
    }

    /// <summary>
    /// 异步加载一个Resource目录下的文件
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="path"></param>
    /// <param name="action"></param>
    public void LoadFromResource<T> (string path,Action<T> action) where T : UnityEngine.Object
    {
        StartCoroutine(IE_LoadFromResource<T>(path, action));
    }

    private IEnumerator IE_LoadFromResource<T>(string path, Action<T> action) where T : UnityEngine.Object
    {
        ResourceRequest rr = Resources.LoadAsync<T>(path);
        yield return rr;
        if (rr.isDone)
        {
            T t = rr.asset as T;
            if (null != action)
            {
                action.Invoke(t);
            }
        }
    }
}
