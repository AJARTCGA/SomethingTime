using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {

    public int mControllerNum = 0;
    public float speed = 0.3f;

    float x, z;//These are here temporarily, don't forget to move them

	// Use this for initialization
	void Start ()
    { 

	}
	
	// Update is called once per frame
	void Update ()
    {
        x = Input.GetAxis("Horizontal");
        z = Input.GetAxis("Vertical");
        //transform.Translate(new Vector3(Input.GetAxis("Horizontal") * speed, 0, Input.GetAxis("Vertical") * speed));
        transform.position = transform.position + new Vector3(x * speed, 0, z * speed);
        if((x > 0.1f || x < -0.1f) || (z > 0.1f || z < -0.1f))
            transform.rotation = Quaternion.Euler(0, Mathf.Atan2(-z, x) * 180 / Mathf.PI,0);

    }

    void OnGUI()
    {
        GUI.Label(new Rect(0, 0, 100, 20), "X: " + x);
        GUI.Label(new Rect(0, 20, 100, 20), "Z: " + z);
    }
}
