using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        // --------------------------------------------------------------------------------------------

        //基于Resource加载

        //生成基本UI(Resources)
        //BaseUIManager.Instance.OpenUI(UIType.Tutorial);
        //再次调用无效
        //BaseUIManager.Instance.OpenUI(UIType.Tutorial);
        //关闭UI
        //BaseUIManager.Instance.CloseUI(UIType.Tutorial);

        // --------------------------------------------------------------------------------------------

        //基于Assetbundle加载

        //生成基本UI(Assetbundle)
        BaseUIManager.Instance.OpenUI(UIType.Tutorial,false);
    }
}