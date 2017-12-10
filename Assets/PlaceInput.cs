using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlaceInput : MonoBehaviour {

    InputField inputField;
    public string text { get; set; }
    /// <summary>
    /// Startメソッド
    /// InputFieldコンポーネントの取得および初期化メソッドの実行
    /// </summary>
    void Start()
    {
        inputField = GetComponent<InputField>();
        InitInputField();
    }

    void InitInputField()
    {

        // 値をリセット
        inputField.text = "";

        // フォーカス
        inputField.ActivateInputField();
    }

}