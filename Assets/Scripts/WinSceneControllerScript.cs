using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class WinSceneControllerScript : MonoBehaviour {

    PlayerDataContainerScript container;
    GameObject winner;

    // Use this for initialization
    void Start ()
    {
        container = GameObject.Find("PlayerDataContainer").GetComponent<PlayerDataContainerScript>();
        GameObject.Find("WinText").GetComponent<Text>().text = "Player " + container.victoriousPlayer + " wins!";
        winner = GameObject.Find("Winner");
        setWinnerColor();


    }
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    void setWinnerColor()
    {
       SkinnedMeshRenderer renderer = winner.transform.GetChild(0).Find("Body").GetComponent<SkinnedMeshRenderer>();
        switch (container.victoriousPlayer)
        {
            case 1:
                renderer.material = container.player1Mat;
                break;
            case 2:
                renderer.material = container.player2Mat;
                break;
            case 3:
                renderer.material = container.player3Mat;
                break;
            case 4:
                renderer.material = container.player4Mat;
                break;
        }
        
    }
}
