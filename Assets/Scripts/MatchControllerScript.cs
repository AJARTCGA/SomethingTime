using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class MatchControllerScript : MonoBehaviour {

    int frozenFlags, deathFlags;
    ////////////////////////////////////////
    ////////////  Flag Key  ////////////////
    ////////////////////////////////////////
    ////    Player:    4 | 3 | 2 | 1   /////
    ///  Bit Value:    8 | 4 | 2 | 1  //////
    //   Win Value:    7 | 11| 13| 14///////
    ////////////////////////////////////////

    PlayerDataContainerScript container;

    // Use this for initialization
    void Start ()
    {
        container = GameObject.Find("PlayerDataContainer").GetComponent<PlayerDataContainerScript>();
        frozenFlags = 0;
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    public void setFrozen(int num)
    {
        frozenFlags |= 1 << (num - 1);
        switch(frozenFlags)
        {
            case 14:
                //Player1 win
                break;
            case 13:
                //Player2 win
                break;
            case 11:
                //Player3 win
                break;
            case 7:
                //Player4 win
                break;
        }
    }

    public void removeFrozen(int num)
    {
        frozenFlags &= ~0 - 1 << (num - 1);
    }

    public void setDeath(int num)
    {
        deathFlags |= 1 << (num - 1);
        print(deathFlags);
        switch (deathFlags)
        {
            case 14:
                print("Player 1 wins!");
                container.victoriousPlayer = 1;
                SceneManager.LoadScene("WinScreen");
                break;
            case 13:
                print("Player 2 wins!");
                container.victoriousPlayer = 2;
                SceneManager.LoadScene("WinScreen");
                break;
            case 11:
                print("Player 3 wins!");
                container.victoriousPlayer = 3;
                SceneManager.LoadScene("WinScreen");
                break;
            case 7:
                print("Player 4 wins!");
                container.victoriousPlayer = 4;
                SceneManager.LoadScene("WinScreen");
                break;
        }
    }
}
