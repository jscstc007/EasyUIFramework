using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Random : MonoBehaviour {

	// Use this for initialization
	void Start () {
        int random = UnityEngine.Random.Range(0, 1000);
        Debug.Log(string.Format("Random Debug: {0}",random));	
	}
}
