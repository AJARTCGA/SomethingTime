using UnityEngine;
using System.Collections;

public class MenuScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        
    }
    public void menu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }
    public void quit()
    {
        Application.Quit();
    }
}
