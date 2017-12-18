using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEditor;


public class MoveCamera : MonoBehaviour {
    private Camera MC;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Right()
    {
        Move(1.0f,0.0f,0.0f);
    }

    public void Left()
    {
        Move(-1.0f, 0.0f, 0.0f);
    }
    public void Up()
    {
        Move(0.0f, 1.0f, 0.0f);
    }

    public void Down()
    {
        Move(0.0f,-1.0f, 0.0f);
    }


    private void Move(float x,float y,float z)
    {
        MC = GameObject.Find("Main Camera").GetComponent<Camera>();
        MC.transform.position = new Vector3(MC.transform.position.x+x, MC.transform.position.y +y, MC.transform.position.z+z);
    }


    public void ZoomIn()
    {
        MC = GameObject.Find("Main Camera").GetComponent<Camera>();

        float size = MC.orthographicSize;
        if (size > 2.0f)
        {
            MC.orthographicSize = size - 1.0f;
        }
        else
        {
         //   EditorUtility.DisplayDialog("Notice", message.ZOOMIN_MSG, "OK");
        }
    }

    public void ZoomOut()
    {
        MC = GameObject.Find("Main Camera").GetComponent<Camera>();

        float size = MC.orthographicSize;
        if (size < 7.0f)
        {
            MC.orthographicSize = size + 1.0f; 
        }
        else
        {
           // EditorUtility.DisplayDialog("Notice", message.ZOOMOUT_MSG, "OK");
        }
    }

}
