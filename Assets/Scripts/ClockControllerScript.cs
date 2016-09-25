using UnityEngine;
using System.Collections;

public class ClockControllerScript : MonoBehaviour {

    GameObject hourHand, minuteHand, secondHand, sun;
    PlayerScript player1, player2, player3, player4;
    public float minuteSpeed = 180;
    public float hourSpeed = 3;

    int currentMinQuadrant = 1;
    int currentHourQuadrant = 1;

    // Use this for initialization
    void Start ()
    {
        Object.DontDestroyOnLoad(GameObject.Find("PlayerDataContainer"));
        hourHand = GameObject.Find("HourHandOrigin");
        minuteHand = GameObject.Find("MinuteHandOrigin");
        sun = GameObject.Find("Sun");
        player1 = GameObject.Find("Player1").GetComponent<PlayerScript>();
        player2 = GameObject.Find("Player2").GetComponent<PlayerScript>();
        player3 = GameObject.Find("Player3").GetComponent<PlayerScript>();
        player4 = GameObject.Find("Player4").GetComponent<PlayerScript>();
        player1.setMin();
        player1.setHour();
    }
	
	// Update is called once per frame
	void Update ()
    {
        float dt = Time.deltaTime;
        float pi = Mathf.PI;
        minuteHand.transform.Rotate(new Vector3(0, dt * minuteSpeed, 0));
        hourHand.transform.Rotate(new Vector3(0, dt * hourSpeed, 0));

        float hour = (hourHand.transform.rotation.eulerAngles.y - 180) * Mathf.Deg2Rad;
        sun.transform.position = new Vector3(Mathf.Cos(hour) * -50, 50, Mathf.Sin(hour) * 50);
            
        checkMin();
        checkHour();
    }
    private void checkMin()
    {
        float min = minuteHand.transform.rotation.eulerAngles.y;

        //Debug.Log(currentMinQuadrant + " - " + min);
        if (currentMinQuadrant == 4 && min < 5)
        {
            if(player1 != null)
                player1.setMin();
            if (player4 != null)
                player4.removeMin();
            currentMinQuadrant = 1;
        }
        else if (currentMinQuadrant == 1 && min > 90)
        {
            if (player2 != null)
                player2.setMin();
            if (player1 != null)
                player1.removeMin();
            currentMinQuadrant = 2;
        }
        else if (currentMinQuadrant == 2 && min > 180)
        {
            if (player3 != null)
                player3.setMin();
            if (player2 != null)
                player2.removeMin();
            currentMinQuadrant = 3;
        }
        else if (currentMinQuadrant == 3 && min > 270)
        {
            if (player4 != null)
                player4.setMin();
            if (player3 != null)
                player3.removeMin();
            currentMinQuadrant = 4;
        }
    }
    private void checkHour()
    {
        float hour = hourHand.transform.rotation.eulerAngles.y;

        if (currentHourQuadrant == 4 && hour < 5)
        {
            if (player1 != null)
                player1.setHour();
            if (player4 != null)
                player4.removeHour();
            currentHourQuadrant = 1;
        }
        else if (currentHourQuadrant == 1 && hour > 90)
        {
            if (player2 != null)
                player2.setHour();
            if (player1 != null)
                player1.removeHour();
            currentHourQuadrant = 2;
        }
        else if (currentHourQuadrant == 2 && hour > 180)
        {
            if (player3 != null)
                player3.setHour();
            if (player2 != null)
                player2.removeHour();
            currentHourQuadrant = 3;
        }
        else if (currentHourQuadrant == 3 && hour > 270)
        {
            if (player4 != null)
                player4.setHour();
            if (player3 != null)
                player3.removeHour();
            currentHourQuadrant = 4;
        }
    }
}
