using UnityEngine;
using System.Collections;

public class ScoreHolder : MonoBehaviour {

    public int mScore;
    public Color mColor;
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    public void setScore(int score)
    ///Each player should hold their own score and then set it here
    {
        mScore = score;
        for(int i = 0; i < transform.childCount; i++)
        {
            SevenSeg sevSeg = transform.GetChild(i).GetComponent<SevenSeg>();
            if(sevSeg != null)
            {
                sevSeg.ripScore();
            }
        }
    }

}
