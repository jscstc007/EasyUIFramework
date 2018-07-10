using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// 资源管理器,提供加载资源的接口(提供了基于Resources和Assetbundle的加载方式,同一资源不能同时混用,目前均只提供同步加载方式)
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
        //初始化Resources字典
        LoadedResources = new Dictionary<string, UnityEngine.Object>();
    }

    private void OnDestroy()
    {
        //清空Resources字典
        LoadedResources.Clear();
    }

    #region Resources加载

    /// <summary>
    /// 当前已经加载了的Resources下资源,用于缓存
    /// </summary>
    private Dictionary<string, UnityEngine.Object> LoadedResources;

    /// <summary>
    /// 同步加载一个Resources目录下的文件
    /// </summary>
    public T LoadFromResource<T> (string path) where T : UnityEngine.Object
    {
        T resource = null;
        //如果缓存有 则直接获取缓存内容
        if (LoadedResources.ContainsKey(path))
        {
            resource = LoadedResources[path] as T;
        }
        //否则resource读取 并添加进缓存中
        else
        {
            resource = Resources.Load<T>(path);
            //添加进缓存
            LoadedResources.Add(path, resource as UnityEngine.Object);   
        }
        return resource;
    }

    #endregion

    #region Assetbundle加载

    private static string AssetbundleLoadPath = Application.streamingAssetsPath;

    private AssetBundleManifest assetbundleManifest;
    private AssetBundleManifest AssetbundleManifest
    {
        get
        {
            if (null == assetbundleManifest)
            {
                AssetBundle ab = AssetBundle.LoadFromFile(string.Format("{0}/{1}", AssetbundleLoadPath, "StreamingAssets"));
                assetbundleManifest = ab.LoadAsset<AssetBundleManifest>("AssetBundleManifest");
            }
            return assetbundleManifest;
        }
    }

    /// <summary>
    /// 当前已经加载过的ab资源
    /// </summary>
    private Dictionary<string, AssetBundle> LoadedAssetbundles = new Dictionary<string, AssetBundle>();

    /// <summary>
    /// 加载Assetbundle文件(同步)
    /// </summary>
    public AssetBundle LoadAssetbundle(string asset)
    {
        AssetBundle ab = null;
        if (LoadedAssetbundles.ContainsKey(asset))
        {
            ab = LoadedAssetbundles[asset];
        }
        else
        {
            //先加载引用项
            string[] dependencies = AssetbundleManifest.GetAllDependencies(asset);

            for (int i = 0; i < dependencies.Length; i++)
            {
                string dependence = dependencies[i];
                Debug.Log("这里可能有问题 不确定是不是要加路径 dependence:" + dependence[i]);

                if (LoadedAssetbundles.ContainsKey(dependence))
                {
                    //已经加载过 则不用再加载了
                }
                else
                {
                    //加载引用项
                    AssetBundle dependenceAssetbundle = AssetBundle.LoadFromFile(dependence);
                    //添加进已加载列表中
                    LoadedAssetbundles.Add(dependence, dependenceAssetbundle);
                }
            }
            //加载实际项
            ab = AssetBundle.LoadFromFile(GetAssetbundlePath(asset));

            //添加进已加载列表中
            LoadedAssetbundles.Add(asset, ab);
        }
        return ab;
    }

    /// <summary>
    /// 获得assetbundle真实路径
    /// </summary>
    /// <param name="asset"></param>
    /// <returns></returns>
    private string GetAssetbundlePath (string asset)
    {
        return string.Format("{0}/{1}", AssetbundleLoadPath, asset);
    }

    /// <summary>
    /// 加载Assetbundle,加载asset(同步)
    /// </summary>
    public T LoadFromAssetbundle<T>(string asset) where T : UnityEngine.Object
    {
        T t = null;
        AssetBundle ab = LoadAssetbundle(asset);
        if (null != ab)
        {
            t = ab.LoadAsset<T>(asset);
        }
        return t;
    }

    /// <summary>
    /// 加载Assetbundle,加载asset并生成一个资源(同步)
    /// </summary>
    public T LoadAndCreateFromAssetbundle<T>(string asset) where T : UnityEngine.Object
    {
        T t = null;
        T origin = LoadFromAssetbundle<T>(asset);

        if (origin != null)
        {
            t = Instantiate<T>(origin);
        }
        
        return t;
    }
    
    #endregion
}
