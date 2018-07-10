using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// UI管理器(同一资源不能同时进行同步/异步操作)
/// </summary>
public class BaseUIManager : MonoBehaviour
{

    #region 单例模式
    private static BaseUIManager instance;
    public static BaseUIManager Instance
    {
        get
        {
            if (null == instance)
            {
                GameObject go = new GameObject("BaseUIManager", typeof(BaseUIManager), typeof(Tag_DontDestroyOnLoad));
                instance = go.GetComponent<BaseUIManager>();
            }
            return instance;
        }
    }
    #endregion

    private void Awake()
    {
        LoadedUI = new Dictionary<UIType, BaseUI>();
    }

    /// <summary>
    /// 当前展示的UI
    /// </summary>
    private Dictionary<UIType, BaseUI> LoadedUI;

    /// <summary>
    /// 正在加载的UI(仅异步加载时有效,目前暂不支持)
    /// </summary>
    private List<UIType> LoadingUI;//Not Support Now

    #region 同步加载

    /// <summary>
    /// 同步加载一个UI
    /// </summary>
    public void OpenUI(UIType type,bool loadFromResources = true, params object[] obj)
    {
        //未展示时才会处理
        if (!LoadedUI.ContainsKey(type))
        {
            string uiPath = loadFromResources ? BaseUIDefine.GetUIPathByUIType(type) : BaseUIDefine.GetUIPathByUIType_Assetbundle(type);

            GameObject prefab = loadFromResources ? ResourceManager.Instance.LoadFromResource<GameObject>(uiPath) : ResourceManager.Instance.LoadFromAssetbundle<GameObject>(uiPath);
            if (null == prefab)
            {
                Debug.LogError(string.Format("OpenUI ({0}:{1}) error, not exist", type, uiPath));
                return;
            }

            //生成UI
            GameObject go = Instantiate(prefab);

            Type uiType = BaseUIDefine.GetBaseUIByUIType(type);
            BaseUI baseUI = null;
            if (null == go.GetComponent(uiType))
            {
                baseUI = go.AddComponent(uiType) as BaseUI;
            }
            else
            {
                baseUI = go.GetComponent(uiType) as BaseUI;
            }
            //Set UI Params
            baseUI.SetUI(obj);

            //添加进Dic中
            LoadedUI.Add(type, baseUI);
        }
        else
        {
            Util.LogError(string.Format("UI({0})已经在展示中了", type));
        }
    }

    #endregion

    /// <summary>
    /// 关闭一个UI
    /// </summary>
    public void CloseUI(UIType type)
    {
        if (LoadedUI.ContainsKey(type))
        {
            //删除UI
            Destroy(LoadedUI[type].CacheGo);
            //移除dic
            LoadedUI.Remove(type);
        }
    }
}