using UnityEngine;
using UnityEngine.UI;
using System; // UnityJsonを使う場合に必要
using System.IO; // ファイル書き込みに必要
using System.Runtime.Serialization.Formatters.Binary;

[System.Serializable]
public class CourseData : MonoBehaviour
{
    public Vector3[] positions;
    public double lon;
    public double lat;

    [SerializeField]
    public struct SaveData
    {    
        public Vector3[] positions;

        public double lon;
        public double lat;

        public void Dump()
        {
            Debug.Log("緯度 = " + lat);
            Debug.Log("経度 = " + lon);
            Debug.Log("座標"+positions);
        }
    }

    // 保存するファイル
    const string SAVE_FILE_PATH = "save.json";


    private LineRenderer lr;
    public void SaveMapData()
    {
        var data = new SaveData();

        data.lat= Convert.ToDouble(GameObject.Find("Ido").GetComponent<Text>().text);
        data.lon= Convert.ToDouble(GameObject.Find("Keido").GetComponent<Text>().text);


        lr = GameObject.Find("Line").GetComponent<LineRenderer>();
        lr.material = new Material(Shader.Find("Sprites/Default"));

  
        // Set positions
        Vector3[] SavePositions = new Vector3[lr.positionCount];
        for (int i = 0; i < lr.positionCount; ++i)
        {
            SavePositions[i] = lr.GetPosition(i);
        }
        print(data.lat + " " + data.lon);
        data.positions = SavePositions;
        string json = JsonUtility.ToJson(data);
        var path = Application.persistentDataPath + "/" + SAVE_FILE_PATH;
        // var writer = new StreamWriter(path, false); // 上書き
        // writer.WriteLine(json);
        // writer.Flush();
        // writer.Close();
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(path);
        bf.Serialize(file, json);
        file.Close();

    }

    public void Save(SaveData sd)
    {
        string json = JsonUtility.ToJson(sd);
        var path = Application.dataPath + "/" + SAVE_FILE_PATH;
        //string path = EditorUtility.SaveFilePanel("名前を付けてJsonを保存しよう", "", "Setting", "json");

        //System.IO.File.WriteAllText(path, json);

        // プロジェクトフォルダ内に保存された際の対応.
        //AssetDatabase.Refresh();

        var writer = new StreamWriter(path, false); // 上書き
        writer.WriteLine(json);
        writer.Flush();
        writer.Close();
    }

    public void Load()
    {
        
        var path = Application.persistentDataPath + "/" + SAVE_FILE_PATH;

        if (!File.Exists(path))
        {//ファイルがない場合FALSE.
            Debug.Log("FileEmpty!");

        }
        else
        {
            //var reader = new StreamReader(info.OpenRead());
            // var json = reader.ReadToEnd();
            //  var data = JsonUtility.FromJson<SaveData>(json);
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(path, FileMode.Open);
            string json = (string)bf.Deserialize(file);
            file.Close();
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            positions = data.positions;
            lon = data.lon;
            lat = data.lat;
        }
       // data.Dump();
    }


} // class JsonEditorWindow