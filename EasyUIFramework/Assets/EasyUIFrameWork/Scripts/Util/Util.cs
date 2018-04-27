using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// 提供了一些常用的功能
/// </summary>
public class Util {

    #region Debug
    
    public static void Log (string str)
    {
        Debug.Log(str);
    }

    public static void LogWarning(string str)
    {
        Debug.LogWarning(str);
    }

    public static void LogError(string str)
    {
        Debug.LogError(str);
    }

    public static void LogException(Exception exception)
    {
        Debug.LogException(exception);
    }
    
    #endregion
}
