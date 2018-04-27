using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialUI : BaseUI {

    public override UIType GetUIType()
    {
        return UIType.Tutorial;
    }

    public override void OnAwake()
    {
        base.OnAwake();

        InitUIRect( UIRoot.Instance.StaticRoot );

        //Write your code here...
    }

    public override void SetUI(params object[] objs)
    {
        base.SetUI(objs);

        //Write your code here...
    }

    public override void OnStart()
    {
        base.OnStart();

        //Write your code here...
    }

    public override void OnRelease()
    {
        base.OnRelease();

        //Write your code here...
    }
}
