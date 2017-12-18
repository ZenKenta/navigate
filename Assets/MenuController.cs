using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
    public void CreateButtonClicked()
    {
        SceneManager.LoadScene("CreateMap");
    }

    public void DrawMapButtonClicked()
    {
        SceneManager.LoadScene("DrawMap");
    }
    public void MenuReturnClicked()
    {
        SceneManager.LoadScene("Menu");
    }
}
