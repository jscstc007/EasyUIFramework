using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour {

	// Use this for initialization
	void Start () {
        //基本生成UI
        BaseUIManager.Instance.OpenUI(UIType.Tutorial, null, null);
        //传参给UI
        BaseUIManager.Instance.OpenUI(UIType.Tip, null, "This string will pass to the TipUI");
        //测试事件系统
        Invoke("InvokeEvent", 1f);
    }

    private void InvokeEvent ()
    {
        EventManager.Instance.CallDelegate(EventType.ShowTipUI, null);
    }
}

