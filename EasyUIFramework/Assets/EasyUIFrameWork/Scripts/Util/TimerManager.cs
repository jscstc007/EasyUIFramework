using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BaseTimer
{
    public string Uid { get; set; }
    public float Time { get; set; }
    public object[] Params { get; set; }
    public Action<object[]> Callback { get; set; }

    public BaseTimer (string uid,float time,Action<object[]> action,object[] objs)
    {
        Uid = uid;
        Time = time;
        Params = objs;
        Callback = action;
    }

    public BaseTimer (string uid,float time, Action<object[]> action):this(uid,time,action,null)
    {
        //Nothing
    }
}

/// <summary>
/// 一个简易计时器管理器
/// </summary>
public class TimerManager : MonoBehaviour {

    #region 单例模式
    private static TimerManager instance;
    public static TimerManager Instance
    {
        get
        {
            if (null == instance)
            {

                GameObject go = new GameObject("TimerManager", typeof(TimerManager), typeof(Tag_DontDestroyOnLoad));
                instance = go.GetComponent<TimerManager>();
            }
            return instance;
        }
    }
    #endregion

    private Dictionary<string, BaseTimer> TimerDic;
    private List<string> ReadyToDeleteTimer;

    private void Awake()
    {
        TimerDic = new Dictionary<string, BaseTimer>();
        ReadyToDeleteTimer = new List<string>();
    }

    // Use this for initialization
    void Start () {
		
	}

    /// <summary>
    /// 注册一个计时器,如果已有同名计时器则无效
    /// </summary>
    /// <param name="uid"></param>
    /// <param name="time"></param>
    /// <param name="action"></param>
    /// <param name="objs"></param>
    public void RegistTimer (string uid,float time,Action<object[]> action,object[] objs = null)
    {
        if (!TimerDic.ContainsKey(uid))
        {
            BaseTimer timer = new BaseTimer(uid, time, action, objs);
            TimerDic.Add(uid, timer);
        }
    }

    /// <summary>
    /// 移除一个Timer
    /// </summary>
    /// <param name="uid"></param>
    public void RemoveTimer (string uid)
    {
        if (TimerDic.ContainsKey(uid))
        {
            TimerDic.Remove(uid);
        }
    }

    private void FixedUpdate()
    {
        foreach(BaseTimer timer in TimerDic.Values)
        {
            timer.Time -= Time.fixedDeltaTime;

            if (timer.Time <= 0)
            {
                //尝试触发Action 并将该Timer放入删除列表中
                Action<object[]> action = timer.Callback;
                object[] objs = timer.Params;
                if (null != action)
                {
                    action.Invoke(objs);
                }

                ReadyToDeleteTimer.Add(timer.Uid);
            }
        }

        //遍历并删除待删除Timer
        if (ReadyToDeleteTimer.Count > 0)
        {
            foreach (string uid in ReadyToDeleteTimer)
            {
                RemoveTimer(uid);
            }
            ReadyToDeleteTimer.Clear();
        }
        
    }

    private void OnDestroy()
    {
        TimerDic.Clear();
        ReadyToDeleteTimer.Clear();
    }
}
