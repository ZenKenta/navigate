using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavigateMap : MonoBehaviour {
    private DrawLine line;

    // Use this for initialization
    void Start () {
        line = new DrawLine();
        line.DataLoad();
        
    }
	
	// Update is called once per frame
	void Update () {

    }
}
