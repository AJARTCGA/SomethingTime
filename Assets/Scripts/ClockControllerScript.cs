using UnityEngine;
using System.Collections;

public class ClockControllerScript : MonoBehaviour {

    GameObject hourHand, minuteHand, secondHand;
    PlayerScript player1, player2, player3, player4;
    public float minuteSpeed = 180;
    public float hourSpeed = 3;
	// Use this for initialization
	void Start ()
    {
        hourHand = GameObject.Find("HourHandOrigin");
        minuteHand = GameObject.Find("MinuteHandOrigin");
        player1 = GameObject.Find("Player1").GetComponent<PlayerScript>();
        player2 = GameObject.Find("Player2").GetComponent<PlayerScript>();
        player3 = GameObject.Find("Player3").GetComponent<PlayerScript>();
        player4 = GameObject.Find("Player4").GetComponent<PlayerScript>();

    }
	
	// Update is called once per frame
	void Update ()
    {
        float dt = Time.deltaTime;
        float pi = Mathf.PI;
        minuteHand.transform.Rotate(new Vector3(0, dt * minuteSpeed, 0));
        hourHand.transform.Rotate(new Vector3(0, dt * hourSpeed, 0));
        if (minuteHand.transform.rotation.y > 360)
        {
            minuteHand.transform.Rotate(0, -360, 0);
        }
        checkMin();
        checkHour();
    }
    private void checkMin()
    {
        float min = minuteHand.transform.rotation.eulerAngles.y;

        if (min > 0 && min < 90)
        {
            player1.setMin();
            player4.removeMin();
        }
        else if (min > 90 && min < 180)
        {
            player2.setMin();
            player1.removeMin();
        }
        else if (min > 180 && min < 270)
        {
            player3.setMin();
            player2.removeMin();
        }
        else if (min > 270 && min < 360)
        {
            player4.setMin();
            player3.removeMin();
        }
    }
    private void checkHour()
    {
        float hour = hourHand.transform.rotation.eulerAngles.y;

        if (hour > 0 && hour < 90)
        {
            player1.setHour();
            player4.removeHour();
        }
        else if (hour > 90 && hour < 180)
        {
            player2.setHour();
            player1.removeHour();
        }
        else if (hour > 180 && hour < 270)
        {
            player3.setHour();
            player2.removeHour();
        }
        else if (hour > 270 && hour < 360)
        {
            player4.setHour();
            player3.removeHour();
        }
    }
}
