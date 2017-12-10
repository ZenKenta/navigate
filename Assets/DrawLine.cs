using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DrawLine : MonoBehaviour {

    public Vector3 startPosition = new Vector3(0.0f, 0.0f, 0.0f);
    public Vector3 secondPosition = new Vector3(0.0f, 0.0f, 1.0f);

    private LineRenderer lr;

    // Use this for initialization
    void Start()
    {
        lr = GameObject.Find("Line").GetComponent<LineRenderer>();
        lr.enabled =false;
    }

    // Update is called once per frame
    // void Update()
    // {
    //     if (OnTouchDown())
    //     {
    //         Debug.Log("タップされました");
    //     }
    // }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Draw();
        }
    }

    void Draw()
    {
        lr = GameObject.Find("Line").GetComponent<LineRenderer>();
        lr.material = new Material(Shader.Find("Sprites/Default"));
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        //debug用
        //print(lr.positionCount);
        //print(lr.GetPosition(0) + "+"+lr.GetPosition(1));

        // 座標の格納可能数を更新する

        Vector3 lineRendererPosition = new Vector3(mousePosition.x, mousePosition.y, -1.0f);
        Vector3 LastPosition = lr.GetPosition(lr.positionCount - 1);
        // print(LastPosition);
        if (lr.GetPosition(0) == startPosition)
        {
            // 座標を上書き格納する
            lr.SetPosition(0, lineRendererPosition);
        }
        else if (lr.GetPosition(1) == secondPosition)
        {
            // 座標を上書き格納する
            lr.SetPosition(1, lineRendererPosition);
            lr.enabled = true;
        }
        else if(lineRendererPosition != lr.GetPosition(lr.positionCount - 1))
        { 
            // 座標を追加格納する
            // SetPosition(index, Vector3)
            lr.positionCount = lr.positionCount + 1;
            lr.SetPosition(lr.positionCount - 1, lineRendererPosition);
        }

    }
    public void ResetLine()
    {
        lr = GameObject.Find("Line").GetComponent<LineRenderer>();
        lr.material = new Material(Shader.Find("Sprites/Default"));

        // Set some positions
        Vector3[] positions = new Vector3[2];
        positions[0] = startPosition;
        positions[1] = secondPosition;
        lr.positionCount = 2;
        lr.SetPositions(positions);
    }

    public void CancelLine()
    {
        lr = GameObject.Find("Line").GetComponent<LineRenderer>();
        lr.material = new Material(Shader.Find("Sprites/Default"));

        // Set some positions
        Vector3[] positions = new Vector3[lr.positionCount - 1];
        for (int i=0;i < lr.positionCount - 1; ++i)
        {
            positions[i] = lr.GetPosition(i);
        }

        lr.positionCount = lr.positionCount - 1;
        lr.SetPositions(positions);
    }


    //スマホ向け そのオブジェクトがタッチされていたらtrue（マルチタップ対応）
    bool OnTouchDown()
    {
        // タッチされているとき
        if (0 < Input.touchCount)
        {
            // タッチされている指の数だけ処理
            for (int i = 0; i < Input.touchCount; i++)
            {
                // タッチ情報をコピー
                Touch t = Input.GetTouch(i);
                // タッチしたときかどうか
                if (t.phase == TouchPhase.Began)
                {
                    GameObject.Find("PlaceText").GetComponent<Text>().text = "ttest";
                }
            }
        }
        return false; //タッチされてなかったらfalse
    }

}
