using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIRoot : MonoBehaviour {

    #region 单例模式
    private static UIRoot instance;
    public static UIRoot Instance
    {
        get
        {
            if (null == instance)
            {
                GameObject prefab = Resources.Load<GameObject>("UIPrefab/UIRoot");
                GameObject go = Instantiate(prefab);
                instance = go.GetComponent<UIRoot>();
                if (null == instance)
                {
                    go.AddComponent<Tag_DontDestroyOnLoad>();
                    instance = go.AddComponent<UIRoot>();
                }
            }
            return instance;
        }
    }
    #endregion

    private GameObject cacheGo;
    public GameObject CacheGo
    {
        get
        {
            if (null == cacheGo)
            {
                cacheGo = gameObject;
            }
            return cacheGo;
        }
    }
    private Transform cacheTransform;
    public Transform CacheTransform
    {
        get
        {
            if (null == cacheTransform)
            {
                cacheTransform = transform;
            }
            return cacheTransform;
        }
    }
    private RectTransform cacheRectTransform;
    public RectTransform CacheRectTransform
    {
        get
        {
            if (null == cacheRectTransform)
            {
                cacheRectTransform = CacheTransform.GetComponent<RectTransform>();
            }
            return cacheRectTransform;
        }
    }

    private Transform staticRoot;
    public Transform StaticRoot
    {
        get
        {
            if (null == staticRoot)
            {
                staticRoot = CacheTransform.Find("StaticRoot");
            }
            return staticRoot;
        }
    }
    private Transform dynamicRoot;
    public Transform DynamicRoot
    {
        get
        {
            if (null == dynamicRoot)
            {
                dynamicRoot = CacheTransform.Find("DynamicRoot");
            }
            return dynamicRoot;
        }
    }
}
