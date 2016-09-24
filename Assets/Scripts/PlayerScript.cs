using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {

    public int mControllerNum = 1;
    public float speed = 0.3f;

    bool mIsMin;
    bool mIsHour;
    Color mStartColor;

    float x, z;//These are here temporarily, don't forget to move them

	// Use this for initialization
	void Start ()
    {
        mStartColor = GetComponent<MeshRenderer>().material.color;
	}
	
	// Update is called once per frame
	void Update ()
    {
        x = Input.GetAxis("Player" + mControllerNum + "X");
        z = Input.GetAxis("Player" + mControllerNum + "Y");
        //transform.Translate(new Vector3(Input.GetAxis("Horizontal") * speed, 0, Input.GetAxis("Vertical") * speed));
       
        if((x > 0.1f || x < -0.1f) || (z > 0.1f || z < -0.1f))
            transform.rotation = Quaternion.Euler(0, Mathf.Atan2(-z, x) * 180 / Mathf.PI,0);
            transform.position = transform.position + new Vector3(x * speed, 0, z * speed);

    }

    void OnGUI()
    {
        GUI.Label(new Rect(0, 0, 100, 20), "X: " + x);
        GUI.Label(new Rect(0, 20, 100, 20), "Z: " + z);
    }

    public void setMin()
    {
        if (!mIsMin)
        {
            mIsMin = true;
            if (!mIsHour)
            {
                GetComponent<MeshRenderer>().material.color = Color.cyan;
            }
        }
    }
    public void removeMin()
    {
        if (mIsMin)
        {
            mIsMin = false;
            GetComponent<MeshRenderer>().material.color = mStartColor;
        } 
    }

    public void setHour()
    {
        if (!mIsMin)
        {
            mIsMin = true;
            if (!mIsHour)
            {
                GetComponent<MeshRenderer>().material.color = Color.black;
            }
        }
    }
    public void removeHour()
    {
        if (mIsMin)
        {
            mIsMin = false;
            if (mIsMin)
            {
                GetComponent<MeshRenderer>().material.color = Color.cyan;
            }
            else
            {
                GetComponent<MeshRenderer>().material.color = mStartColor;
            }
            
        }
    }
}
