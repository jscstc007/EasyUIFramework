using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour {

	// Use this for initialization
	void Start () {
        //测试生成基本UI
        BaseUIManager.Instance.OpenUI(UIType.Tutorial, null, null);
        //测试生成基本UI并传参给UI
        BaseUIManager.Instance.OpenUI(UIType.Tip, null, "设置传参给TipUI的实例");
        //测试事件系统
        Invoke("Test_InvokeEvent", 1f);
        //测试单例类
        TestSingleton.Instance.DoDebug();
        //测试计时器
        TimerManager.Instance.RegistTimer("Timer1", 2f, Test_Timer, new object[] { "测试计时器功能 2f后输出" });
    }

    private void Test_InvokeEvent ()
    {
        EventManager.Instance.CallDelegate(EventType.ShowTipUI, "测试事件系统,会在1S后输出");
    }

    private void Test_Timer (object[] objs)
    {
        string info = objs[0] as string;
        Debug.Log(info);
    }
}

