using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialUI : BaseUI {

    #region UI Base

    public override UIType GetUIType()
    {
        return UIType.Tutorial;
    }

    public override void OnAwake()
    {
        base.OnAwake();

        //处理位置关系
        InitUIRect(UIRoot.Instance.DynamicRoot);

        Util.Log(string.Format("OnAwake : {0}", GetUIType()));
    }

    public override void OnStart()
    {
        base.OnStart();

        Util.Log(string.Format("OnStart : {0}", GetUIType()));
    }

    public override void SetUI(params object[] objs)
    {
        base.SetUI(objs);

        Util.Log(string.Format("SetUI : {0}", GetUIType()));
    }

    public override void OnRelease()
    {
        base.OnRelease();

        Util.Log(string.Format("OnRelease : {0}", GetUIType()));
    }

    #endregion
}
