using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BaseUIManager : MonoBehaviour{

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

    /// <summary>
    /// 当前资源目录是否位于Resources下
    /// </summary>
    public static bool IsUseResource = true;

    /// <summary>
    /// 当前展示的UI
    /// </summary>
    private Dictionary<UIType, BaseUI> UIDic = new Dictionary<UIType, BaseUI>();

    /// <summary>
    /// 正在加载的UI
    /// </summary>
    private List<UIType> loadingUIList = new List<UIType>();

    /// <summary>
    /// 打开一个UI(异步加载)
    /// </summary>
    /// <param name="uIType"></param>
    public void OpenUI (UIType uIType,Action<BaseUI> action,params object[] objs)
    {
        //如果该UI正在展示 不作处理
        if (UIDic.ContainsKey(uIType))
        {
            //Do Nothing
        }
        //否则 如果该UI正在被加载中 不作处理
        else if (loadingUIList.Contains(uIType))
        {
            //Do Nothing
        }
        //否则 准备加载该UI
        else
        {
            //在Resource目录下获取资源
            if (IsUseResource)
            {
                StartCoroutine(IE_OpenUI_Resources(uIType, action, objs));
            }
            //通过Assetbundle获取资源
            else
            {
                //TODO
            }
        }
    }

    /// <summary>
    /// 异步加载Resources下的UI预设体
    /// </summary>
    /// <param name="path"></param>
    /// <param name="action"></param>
    /// <returns></returns>
    private IEnumerator IE_OpenUI_Resources(UIType uIType, Action<BaseUI> action, params object[] objs)
    {
        string path = GetUIPathByUIType(uIType);
        ResourceRequest rr = Resources.LoadAsync<GameObject>(path);
        yield return rr;
        if (rr.isDone)
        {
            GameObject prefab = rr.asset as GameObject;
            //Init and create Gameobject
            GameObject go = Instantiate(prefab);
            BaseUI ui = go.GetComponent<BaseUI>();
            if (null == ui)
            {
                Type type = GetBaseUIByUIType(uIType);
                ui = go.AddComponent(type) as BaseUI;
            }
            if (null != objs)
            {
                ui.SetUI(objs);
            }
            if (null != action)
            {
                action.Invoke(ui);
            }
        }
    }

    /// <summary>
    /// 异步加载Assetbundle的UI预设体(默认位于streamingassets目录下,可手动调整至persist目录)
    /// </summary>
    /// <param name="path"></param>
    /// <param name="action"></param>
    /// <returns></returns>
    private IEnumerator IE_OpenUI_Assetbundle(Action<BaseUI> action)
    {
        //TODO
        yield return null;
    }

    /// <summary>
    /// 根据UIType获取UIPath(用于Resources加载)
    /// </summary>
    /// <param name="uIType"></param>
    /// <returns></returns>
    private string GetUIPathByUIType (UIType uIType)
    {
        string path = string.Empty;
        switch (uIType) {
            case UIType.Tutorial:
                path = "UIPrefab/Tutorial/TutorialUI";
                break;
            case UIType.Tip:
                path = "UIPrefab/Tip/TipUI";
                break;
            default:
                break;
        }
        return path;
    }

    /// <summary>
    /// 根据UIType获取BaseUI
    /// </summary>
    /// <param name="uIType"></param>
    /// <returns></returns>
    private Type GetBaseUIByUIType (UIType uIType)
    {
        switch (uIType)
        {
            case UIType.Tutorial:
                return typeof(TutorialUI);
            case UIType.Tip:
                return typeof(TipUI);
            default:
                break;
        }
        return null;
    }

    
}
