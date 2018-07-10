using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void UIDelegate(params object[] objs);

public enum EventType
{
    None = -1,

    ShowTutorialUI,
}

public class EventManager : ISingleton<EventManager>{

    private Dictionary<EventType, UIDelegate> UIDelegateDic = new Dictionary<EventType, UIDelegate>();

    /// <summary>
    /// 注册一个事件
    /// </summary>
    /// <param name="type"></param>
    /// <param name="callback"></param>
    public void RegistDelegate (EventType type,UIDelegate callback)
    {
        if (UIDelegateDic.ContainsKey(type))
        {
            UIDelegateDic[type] += callback;
        }
        else
        {
            UIDelegateDic.Add(type, callback);
        }
    }

    /// <summary>
    /// 移除一个事件
    /// </summary>
    /// <param name="type"></param>
    /// <param name="callback"></param>
    public void RemoveDelegate (EventType type, UIDelegate callback)
    {
        if (UIDelegateDic.ContainsKey(type))
        {
            UIDelegateDic[type] -= callback;
        }
    }

    /// <summary>
    /// 触发一个事件
    /// </summary>
    /// <param name="type"></param>
    public void CallDelegate (EventType type,params object[] objs)
    {
        if (UIDelegateDic.ContainsKey(type))
        {
            UIDelegate uIDelegate = UIDelegateDic[type];
            if (null != uIDelegate)
            {
                uIDelegate.Invoke(objs);
            }
        }
    }
}
