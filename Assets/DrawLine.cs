using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DrawLine : MonoBehaviour {

    public Vector3 startPosition = new Vector3(0.0f, 0.0f, 0.0f);
    public Vector3 secondPosition = new Vector3(0.0f, 0.0f, 1.0f);

    private LineRenderer lr;
    private CourseData Data;

    // Use this for initialization
    void Start()
    {

    }

    void Update()
    {
        //マウス操作での確認処理
        MouseDown();

        //スマホ操作
        //OnTouchDown();

    }



    public void DataLoad()
    {
        lr = GameObject.Find("Line").GetComponent<LineRenderer>();
        //lr.enabled =false;
        Data = new CourseData();
        Data.Load();

        lr.positionCount = Data.positions.Length;
        lr.SetPositions(Data.positions);

        if (Data.positions.Length < 2)
        {
            lr.enabled = false;
        }
        else
        {
            lr.enabled = true;
        }
    }
    //マウス向け
    void MouseDown()
    {
        if (Input.GetMouseButton(0))
        {
            if (Input.mousePosition.x > 150)
            {
                Vector2 Position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Draw(Position);
            }
        }        
    }
    //スマホ向け そのオブジェクトがタッチされていたらtrue（マルチタップ対応）
    void OnTouchDown()
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
                    if (t.position.x > 150)
                    {
                        Draw(Camera.main.ScreenToWorldPoint(t.position));
                    }
                 }
            }
        }
    }

    void Draw(Vector2 Position)
    {
        lr = GameObject.Find("Line").GetComponent<LineRenderer>();
        lr.material = new Material(Shader.Find("Sprites/Default"));
        //debug用
        //print(lr.positionCount+":"+lr.GetPosition(0) + ","+lr.GetPosition(1));

        // 座標の格納可能数を更新する
        Vector3 lineRendererPosition = new Vector3(Position.x, Position.y, -1.0f);

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

}
