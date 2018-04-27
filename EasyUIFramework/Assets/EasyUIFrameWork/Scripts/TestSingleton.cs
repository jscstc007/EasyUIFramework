using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSingleton : ISingleton<TestSingleton> {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void DoDebug ()
    {
        Debug.Log("这是单例模式的测试");
    }
}
