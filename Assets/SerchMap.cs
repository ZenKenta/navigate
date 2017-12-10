using System.Collections;
using UnityEngine;
using System.Xml;
using System;
using UnityEngine.UI;

public class SerchMap : MonoBehaviour {
//    public DrawGoogleMap drawer;

    GeoLocation calculator;
    Text Place;

    private int width = 2000;
    private int height =2000;

    private double longitude;
    private double latitude;

    // Use this for initialization
    void Start () {
        ///StartCoroutine(GetGeoCode());
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    public void test()
    {
        StartCoroutine(GetGeoCode());
    }

    private string GeoURL = "http://www.geocoding.jp/api/?q=";
    public IEnumerator GetGeoCode()
    {
        Place = GameObject.Find("PlaceText").GetComponent<Text>();

        print(GeoURL+ Place.text);
        //WWW www = new WWW(GeoURL + "下作延４－２２－３１");
        WWW www = new WWW(GeoURL + Place.text);
        yield return www;
        if (www.error == null)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(www.text);
            XmlNodeList nodes = xmlDoc.GetElementsByTagName("result");

            foreach (XmlNode node in nodes)
            {
                XmlNode childNode = node.FirstChild;
                int count = 0;
                do
                {
                    if (++count > 10)
                        break;
                    if (childNode.Name == "coordinate")
                    {
                        XmlNodeList list = childNode.ChildNodes;
                        int c = 0;
                        foreach (XmlNode n in list)
                        {
                            if (c == 0)
                            {
                                latitude = Convert.ToDouble(n.FirstChild.Value);
                            }
                            else if (c == 1)
                            {
                                longitude = Convert.ToDouble(n.FirstChild.Value);
                            }
                            c++;
                        }
                    }
                } while ((childNode = childNode.NextSibling) != null);
            }
            print("Location: " + latitude + " " + longitude + " " + Input.location.lastData.altitude + " " + Input.location.lastData.horizontalAccuracy + " " + Input.location.lastData.timestamp);
            //drawer.Geo = new GeoLocation(latitude, longitude);
            //	string url = "http://maps.googleapis.com/maps/api/streetview?" + "size=" + width + "x" + height + "&location=" + latitude + "," + longitude + "&heading=" + heading + "&pitch=" + pitch + "&fov=90&sensor=false";
            string url = "http://maps.googleapis.com/maps/api/staticmap?center=" + latitude + "," + longitude + "&zoom=" + 17 + "&size=" + width + "x" + height ;

            www = new WWW(url);
            yield return www;

            GameObject.Find("Map").GetComponent<Renderer>().material.mainTexture = www.texture;
        }
        else
        {
            Debug.LogError(www.error);
        }
    }

}
