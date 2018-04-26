using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tag_DontDestroyOnLoad : MonoBehaviour {

    void Awake()
    {
        DontDestroyOnLoad(gameObject);    
    }
}
