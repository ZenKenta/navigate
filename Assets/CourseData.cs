using UnityEngine;
using UnityEditor;
using System; // UnityJsonを使う場合に必要
using System.IO; // ファイル書き込みに必要

[System.Serializable]
public class CourseData : MonoBehaviour
{
    [SerializeField]
    public struct SaveData
    {
        public int x;
        public int y;

        public void Dump()
        {
            Debug.Log("x = " + x);
            Debug.Log("y = " + y);
        }
    }

    // 保存するファイル
    const string SAVE_FILE_PATH = "save.txt";
    /// <summary>
    /// ScriptableWizardのメインとなるボタンが押された際に呼ばれるよ！
    /// 今回の場合はDisplayWizardの第二引数で指定した"Save"ボタンが押されたとき。
    /// </summary>
    public void Create()
    {
        var data = new SaveData();
        data.x = 5;
        data.y = 7;
        string json = JsonUtility.ToJson(data);
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


} // class JsonEditorWindow