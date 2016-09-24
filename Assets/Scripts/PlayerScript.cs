using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {

    public int mControllerNum = 1;
    public float speed = 0.3f;

    bool mIsMin;
    bool mIsHour;
    bool mIsFrozen;
    Color mStartColor;
    static Color frozenColor = Color.cyan;
    static Color hourColor = Color.white;
    static Color minuteColor = Color.black;


	// Use this for initialization
	void Start ()
    {
        mStartColor = GetComponent<MeshRenderer>().material.color;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (!mIsFrozen)
        {
            float x = Input.GetAxis("Player" + mControllerNum + "X");
            float z = Input.GetAxis("Player" + mControllerNum + "Y");
            transform.Find("FrozenSign").GetComponent<SpriteRenderer>().enabled = false;

            if ((x > 0.1f || x < -0.1f) || (z > 0.1f || z < -0.1f))
            {
                transform.rotation = Quaternion.Euler(0, Mathf.Atan2(-z, x) * 180 / Mathf.PI, 0);
                transform.position = transform.position + new Vector3(x * speed, 0, z * speed);
            }
        }
        else
        {
            transform.Find("FrozenSign").GetComponent<SpriteRenderer>().enabled = true;
        }
    }
    
    public void setMin()
    {
        mIsFrozen = false;
        mIsMin = true;
        if (!mIsHour)
        {
            GetComponent<MeshRenderer>().material.color = minuteColor;
        }
        else
        {
            GetComponent<MeshRenderer>().material.color = hourColor;
        }
    }
    public void removeMin()
    {
        mIsFrozen = false;
        mIsMin = false;
        if (mIsHour)
        {
            GetComponent<MeshRenderer>().material.color = hourColor;
        }
        else
        {
            GetComponent<MeshRenderer>().material.color = mStartColor;
        }
    }

    public void setHour()
    {
        mIsHour = true;
        GetComponent<MeshRenderer>().material.color = hourColor;
        transform.Find("HourPower").GetComponent<SpriteRenderer>().enabled = true;
    }
    public void removeHour()
    {
        mIsHour = false;
        transform.Find("HourPower").GetComponent<SpriteRenderer>().enabled = false;
        if (mIsMin)
        {
            GetComponent<MeshRenderer>().material.color = minuteColor;
        }
        else
        {
            GetComponent<MeshRenderer>().material.color = mStartColor;
        }
    }

    public bool getHour()
    {
        return mIsHour;
    }
    public bool getMin()
    {
        return mIsMin;
    }

    void OnCollisionEnter(Collision coll)
    {
        if(coll.gameObject.tag == "Player")
        {
            PlayerScript script = coll.gameObject.GetComponent<PlayerScript>();
            if (script.getHour())
            {
                mIsFrozen = true;
                GetComponent<MeshRenderer>().material.color = frozenColor;
            }
            else if (script.getMin())
            {
                if (!mIsHour)
                {
                    mIsFrozen = false;
                    GetComponent<MeshRenderer>().material.color = mStartColor;
                }
            }
        }
    }
    void OnTriggerEnter(Collider coll)
    {
        if(coll.gameObject.name == "Test")
        {
            Destroy(coll.gameObject);
        }
    }
}
