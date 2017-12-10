using UnityEngine;
using System.Collections;
using System;

public class DrawGoogleMap : MonoBehaviour
{
    private double longitude = 139.704051;
    private double latitude = 35.661777;

    private int width = 1000;
    private int height =1000;


    GeoLocation calculator;

 
   void Start()
    {
       if (calculator == null)
        {
            calculator = new GeoLocation(latitude, longitude);
            Build();
        }
    }

    public GeoLocation Geo
    {
        get
        {
            return calculator;
        }
        set
        {
            calculator = value;
            Build();
        }
    }

    public void SetGeo(GeoLocation geo)
    {
        Geo = geo;
    }

    // Use this for initialization
    public void Draw(GeoLocation calc)
    {
        calculator = calc;
        Build();
    }

    public void Build()
    {
        //ストリートビュー
        //	string url = "http://maps.googleapis.com/maps/api/streetview?" + "size=" + width + "x" + height + "&location=" + latitude + "," + longitude + "&heading=" + heading + "&pitch=" + pitch + "&fov=90&sensor=false";
        //地図表示
        String Url = "http://maps.googleapis.com/maps/api/staticmap?center=" + calculator.lat + "," + calculator.lon + "&zoom=" + 17 + "&size=" + width + "x" + height + "&markers=size:mid%7Color:red%7C" + calculator.nlat + "," + calculator.nlon;

        StartCoroutine(Download(Url, tex => addSplatPrototype(tex)));
    }

    /// 
    /// GoogleMapsAPIから地図画像をダウンロードする
    /// 

    /// ダウンロード元
    /// ダウンロード後に実行されるコールバック関数
    IEnumerator Download(string url, Action<Texture2D> callback)
    {
        var www = new WWW(url);
        yield return www; // Wait for download to complete

        callback(www.texture);
    }

    /// 
    /// Planeにテクスチャを貼り付ける
    /// 

    /// 
    public void addSplatPrototype(Texture2D tex)
    {
        GetComponent<Renderer>().material.mainTexture = tex;
    }
}