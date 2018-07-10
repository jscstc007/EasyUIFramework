using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 基础UI类 所有UI模块需继承该类
/// </summary>
public abstract class BaseUI : MonoBehaviour {

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
    /// <summary>
    /// 获取UIType(非拉伸铺满)
    /// </summary>
    /// <returns></returns>
    public abstract UIType GetUIType();

    /// <summary>
    /// 初始化UI位置与大小
    /// </summary>
    public void InitUIRect(Transform parent, Vector2 pos,Vector2 size)
    {
        CacheTransform.SetParent(parent);

        CacheRectTransform.anchoredPosition = pos;
        CacheRectTransform.sizeDelta = size;
        CacheRectTransform.localScale = Vector3.one;
    }

    /// <summary>
    /// 初始化UI位置与大小(拉伸铺满)
    /// </summary>
    public void InitUIRect(Transform parent)
    {
        CacheTransform.SetParent(parent);

        CacheRectTransform.anchoredPosition = Vector2.zero ;
        CacheRectTransform.sizeDelta = Vector2.zero;
        CacheRectTransform.localScale = Vector3.one;
    }

    void Awake()
    {
        OnAwake();
    }

    /// <summary>
    /// Awake时调用
    /// </summary>
    public virtual void OnAwake ()
    {
        //Write Your Code ...

        //建议在这里处理UI的位置关系
    }

    /// <summary>
    /// 在OnStart前调用,仅当参数不为空时才会触发
    /// </summary>
    public virtual void SetUI(params object[] objs)
    {
        //Write Your Code ...

        //建议在这里处理数据初始化
    }

    void Start()
    {
        OnStart();
    }

    /// <summary>
    /// Start时调用
    /// </summary>
    public virtual void OnStart()
    {
        //Write Your Code ...

        //建议在这里处理UI初始化
    }

    
    void OnDestroy()
    {
        OnRelease();
    }

    /// <summary>
    /// OnDestroy时调用
    /// </summary>
    public virtual void OnRelease ()
    {
        //Write Your Code ...

        //这里释放资源
    }
}
