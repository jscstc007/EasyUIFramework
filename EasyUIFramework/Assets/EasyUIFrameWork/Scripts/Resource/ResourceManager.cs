using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// 资源管理器,提供加载资源的接口
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

    private void Awake()
    {
        LoadedResource = new Dictionary<string, UnityEngine.Object>();
    }

    private void OnDestroy()
    {
        LoadedResource.Clear();
    }

    #region Resources加载

    /// <summary>
    /// 当前已经加载了的Resources下资源,用于缓存
    /// </summary>
    private Dictionary<string, UnityEngine.Object> LoadedResource;

    /// <summary>
    /// 同步加载一个Resources目录下的文件(Resource加载暂不提供异步加载方式)
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="path"></param>
    /// <returns></returns>
    public T LoadFromResource<T> (string path) where T : UnityEngine.Object
    {
        T resource = null;
        //如果缓存有 则直接获取缓存内容
        if (LoadedResource.ContainsKey(path))
        {
            resource = LoadedResource[path] as T;
        }
        //否则resource读取 并添加进缓存中
        else
        {
            resource = Resources.Load<T>(path);
            //添加进缓存
            LoadedResource.Add(path, resource as GameObject);   
        }
        return resource;
    }

    #endregion

    #region Assetbundle加载

    private AssetBundleManifest assetBundleManifest;

    /// <summary>
    /// 同步加载Assetbundle文件(待补充)
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="name"></param>
    /// <returns></returns>
    public T LoadFromAssetbundle<T>(string name) where T : UnityEngine.Object
    {
        //TODO
        T t = null;

        return t;
    }

    #endregion




}
