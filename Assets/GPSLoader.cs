using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GPSLoader : MonoBehaviour
{
    private double initlongitude = 139.704051;
    private double initlatitude = 35.661777;
    private double longitude = 139.704051;
    private double latitude = 35.661777;

    private int width = 1000;
    private int height = 1000;

    private CourseData Data;
    GeoLocation calculator;
    private float intervalTime = 0.0f;



    //IEnumerator Start()
    void Start()
    {
        Data = new CourseData();
        Data.Load();
        initlongitude = Data.lon;
        initlatitude = Data.lat;
        print(longitude + " , " + latitude);
        //Build();
        StartCoroutine(UpdateLocation());
    }

    void Update()
    {
        //毎フレーム読んでると処理が重くなるので、3秒毎に更新
         intervalTime += Time.deltaTime;
         if (intervalTime >= 3.0f)
         {
            StartCoroutine(UpdateLocation());
            intervalTime = 0.0f;
         }
    }

    IEnumerator UpdateLocation()
    {
        // First, check if user has location service enabled
        if (!Input.location.isEnabledByUser)
        {
            yield break;
        }

        // Start service before querying location
        Input.location.Start();

        // Wait until service initializes
        int maxWait = 20;
        while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
        {
            yield return new WaitForSeconds(1);
            maxWait--;
        }

        // Service didn't initialize in 20 seconds
        if (maxWait < 1)
        {
#if DEBUG
            print("Timed out");
#endif

            yield break;
        }

        // Connection has failed
        if (Input.location.status == LocationServiceStatus.Failed)
        {
#if DEBUG
            print("Unable to determine device location");
#endif

            yield break;

        }
        else
        {
            latitude = Convert.ToDouble(Input.location.lastData.latitude);
            longitude = Convert.ToDouble(Input.location.lastData.longitude);

#if DEBUG
            //print("Location: " + initlatitude + " " + initlongitude + " " + Input.location.lastData.altitude + " " + Input.location.lastData.horizontalAccuracy + " " + Input.location.lastData.timestamp);
#endif

            Build();
        }

        // Stop service if there is no need to query location updates continuously
        Input.location.Stop();
    }

    public void Build()
    {
        //ストリートビュー
        //	string url = "http://maps.googleapis.com/maps/api/streetview?" + "size=" + width + "x" + height + "&location=" + latitude + "," + longitude + "&heading=" + heading + "&pitch=" + pitch + "&fov=90&sensor=false";
        //地図表示
        String Url = "http://maps.googleapis.com/maps/api/staticmap?center=" + initlatitude + "," + initlongitude + "&zoom=" + 17 + "&size=" + width + "x" + height + "&markers=size:mid%7Color:red%7C" + latitude + "," + longitude;
        print(Url);
        StartCoroutine(Download(Url));
    }

    IEnumerator Download(string url)
    {
        var www = new WWW(url);
        yield return www; // Wait for download to complete

        GameObject.Find("Map").GetComponent<Renderer>().material.mainTexture = www.texture;
    }

}