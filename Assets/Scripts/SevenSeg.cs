using UnityEngine;
using System.Collections;

public class SevenSeg : MonoBehaviour {

    // Use this for initialization
    int mScore;
    int mRippedScore;
    byte[] displayScore = new byte[] { 1, 1, 1, 1, 1, 1, 0 };
    public int digitPos;
    
	void Start ()
    {
        mScore = transform.parent.GetComponent<ScoreHolder>().mScore;
        for(int i = 0; i < transform.childCount; i++)
        {
            Transform child = transform.GetChild(i);
            child.gameObject.GetComponent<SpriteRenderer>().color = GetComponentInParent<ScoreHolder>().mColor;
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
        ripScore();
        int temp = 0;
        foreach (Transform child in transform)
        {
            if(displayScore[temp] == 0 )
            {
                child.GetComponent<SpriteRenderer>().enabled = false;

            }
            else if(displayScore[temp] == 1)
            {
                child.GetComponent<SpriteRenderer>().enabled = true;
            }
            temp++;
        }
    }

    public void ripScore()
    {
        mScore = transform.parent.GetComponent<ScoreHolder>().mScore;
        string tempScore = mScore.ToString();
        char tempDigit;
        if (tempScore.Length - digitPos - 1 >= 0)
            tempDigit = tempScore[tempScore.Length - digitPos - 1];
        else
            tempDigit = '0';
        int temp = tempDigit - '0';
        
        switch (temp)
        {
            case 0:
                displayScore = new byte[] { 1, 1, 1, 1, 1, 1, 0 };
                break;
            case 1:
                displayScore = new byte [] { 0, 1, 1, 0, 0, 0, 0 };
                break;
            case 2:
                displayScore = new byte[] { 1, 1, 0, 1, 1, 0, 1 };
                break;
            case 3:
                displayScore = new byte[] { 1, 1, 1, 1, 0, 0, 1 };
                break;
            case 4:
                displayScore = new byte[] { 0, 1, 1, 0, 0, 1, 1 };
                break;
            case 5:
                displayScore = new byte[] { 1, 0, 1, 1, 0, 1, 1 };
                break;
            case 6:
                displayScore = new byte[] { 1, 0, 1, 1, 1, 1, 1 };
                break;
            case 7:
                displayScore = new byte[] { 1, 1, 1, 0, 0, 0, 0 };
                break;
            case 8:
                displayScore = new byte[] { 1, 1, 1, 1, 1, 1, 1 };
                break;
            case 9:
                displayScore = new byte[] { 1, 1, 1, 0, 0, 1, 1 };
                break;

            default:
                displayScore = new byte[] { 1, 1, 1, 1, 1, 1, 0 };
                break;
        }
        temp = 0;
        foreach (Transform child in transform)
        {
            if (displayScore[temp] == 0)
            {
                child.GetComponent<SpriteRenderer>().enabled = false;

            }
            else if (displayScore[temp] == 1)
            {
                child.GetComponent<SpriteRenderer>().enabled = true;
            }
            temp++;
        }
    }
}
