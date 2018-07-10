using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// UI类型,可扩充自己的UI类型
/// </summary>
public enum UIType
{
    None = -1,

    Tutorial,
    //Write your UIType ...
}

public class BaseUIDefine {

    /// <summary>
    /// 根据UIType获取UIPath(用于Resources加载)
    /// </summary>
    public static string GetUIPathByUIType(UIType uIType)
    {
        string path = string.Empty;
        switch (uIType)
        {
            case UIType.Tutorial:
                path = "UI/Tutorial/TutorialUI";
                break;
            default:
                break;
        }
        return path;
    }

    /// <summary>
    /// 根据UIType获取UIPath(用于Assetbunlde加载)
    /// </summary>
    public static string GetUIPathByUIType_Assetbundle(UIType uIType)
    {
        string path = string.Empty;
        switch (uIType)
        {
            case UIType.Tutorial:
                path = "TutorialUI";
                break;
            default:
                break;
        }
        return path;
    }

    /// <summary>
    /// 根据UIType获取BaseUI
    /// </summary>
    public static Type GetBaseUIByUIType(UIType uIType)
    {
        switch (uIType)
        {
            case UIType.Tutorial:
                return typeof(TutorialUI);
            default:
                break;
        }
        return null;
    }


}

