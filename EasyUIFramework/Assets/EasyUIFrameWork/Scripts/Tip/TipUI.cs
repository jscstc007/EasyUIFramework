using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TipUI : BaseUI {

    public override UIType GetUIType()
    {
        return UIType.Tip;
    }

    public override void OnAwake()
    {
        base.OnAwake();

        InitUIRect( UIRoot.Instance.DynamicRoot,new Vector2(0,-100),new Vector2(300,200) );

        //Write your code here...
    }

    public override void SetUI(params object[] objs)
    {
        base.SetUI(objs);

        if (null != objs)
        {
            string info = objs[0].ToString();
            Debug.Log(string.Format("TipUI -- SetUI :{0}", info));
        }
    }

    public override void OnStart()
    {
        base.OnStart();

        //Write your code here...
        EventManager.Instance.RegistDelegate(EventType.ShowTipUI, OnTipShow);
    }

    public override void OnRelease()
    {
        base.OnRelease();

        //Write your code here...
        EventManager.Instance.RemoveDelegate(EventType.ShowTipUI, OnTipShow);
    }

    
    /// <summary>
    /// 事件测试
    /// </summary>
    /// <param name="objs"></param>
    private void OnTipShow (params object[] objs)
    {

        Debug.Log("触发了事件 OnTipShow");
    }
}
