using UnityEngine;
using System.Collections;

public class RotateClock : MonoBehaviour {
    GameObject hourHand, minuteHand;
    public float minuteSpeed = 180;
    public float hourSpeed = 3;
    // Use this for initialization
    void Start () {
        hourHand = GameObject.Find("HourHandOrigin");
        minuteHand = GameObject.Find("MinuteHandOrigin");
    }
	
	// Update is called once per frame
	void Update () {
        float dt = Time.deltaTime;
        minuteHand.transform.Rotate(new Vector3(0, 0, -dt * minuteSpeed));
        hourHand.transform.Rotate(new Vector3(-dt * hourSpeed, 0, 0));

        float hour = (hourHand.transform.rotation.eulerAngles.y - 180) * Mathf.Deg2Rad;
    }
}
