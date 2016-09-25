using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerScript : MonoBehaviour
{

    public int mControllerNum = 1;
    public float speed = 0.3f;

    bool mIsMin;
    bool mIsHour;
    bool mIsFrozen;
    Color mStartColor;
    static Color frozenColor = Color.cyan;
    static Color hourColor = Color.white;
    static Color minuteColor = Color.black;
    Animator mAnim;

    protected Dictionary<int, GameObject> mPtsDic = new Dictionary<int, GameObject>();

    // Use this for initialization
    void Start()
    {
        mAnim = GetComponent<Animator>();
        if (mAnim != null)
            mAnim.enabled = true;
        if (GetComponent<MeshRenderer>() != null)
            mStartColor = GetComponent<MeshRenderer>().material.color;
    }

    // Update is called once per frame
    void Update()
    {
        if (!mIsFrozen)
        {
            float x = Input.GetAxis("Player" + mControllerNum + "X");
            float z = Input.GetAxis("Player" + mControllerNum + "Y");
            transform.Find("FrozenSign").GetComponent<SpriteRenderer>().enabled = false;

            if ((x > 0.1f || x < -0.1f) || (z > 0.1f || z < -0.1f))
            {
                if (mAnim != null && !mAnim.GetBool("isRunning"))
                {
                    mAnim.SetBool("isRunning", true);
                }
                transform.rotation = Quaternion.Euler(0, Mathf.Atan2(-z, x) * 180 / Mathf.PI + 90, 0);
                transform.position = transform.position + new Vector3(x * speed, 0, z * speed);
            }
            else
            {
                if (mAnim != null)
                {
                    mAnim.SetBool("isRunning", false);
                }
            }
        }
        else
        {
            transform.Find("FrozenSign").GetComponent<SpriteRenderer>().enabled = true;
        }

        print("(a) Elements in mPtsDic: " + mPtsDic.Count);
        if (mPtsDic.Count > 0)
        {
            print("(b) Elements in mPtsDic: " + mPtsDic.Count);
            //List<int> keys = new List<int>(mPtsDic.Keys);
            int[] keys = (new List<int>(mPtsDic.Keys)).ToArray();
            //print("Elements in keys: " + keys.Count);
            for (int i = 0; i < keys.Length; i++)
            {
                int key = keys[i];
                GameObject tmp = mPtsDic[key];
                if (tmp != null && (transform.position - mPtsDic[key].transform.position).magnitude <= 1.5f)
                {
                    mPtsDic.Remove(key);
                    print(mPtsDic.Count);
                    Destroy(tmp);
                    
                }
            }
        }
    }

    public void setMin()
    {
        mIsFrozen = false;
        mIsMin = true;
        if (!mIsHour)
        {
            if (GetComponent<MeshRenderer>() != null)
                GetComponent<MeshRenderer>().material.color = minuteColor;
        }
        else
        {
            if (GetComponent<MeshRenderer>() != null)
                GetComponent<MeshRenderer>().material.color = hourColor;
        }
    }
    public void removeMin()
    {
        mIsFrozen = false;
        mIsMin = false;
        if (mIsHour)
        {
            if (GetComponent<MeshRenderer>() != null)
                GetComponent<MeshRenderer>().material.color = hourColor;
        }
        else
        {
            if (GetComponent<MeshRenderer>() != null)
                GetComponent<MeshRenderer>().material.color = mStartColor;
        }
    }

    public void setHour()
    {
        mIsHour = true;
        if (GetComponent<MeshRenderer>() != null)
            GetComponent<MeshRenderer>().material.color = hourColor;
        transform.Find("HourPower").GetComponent<SpriteRenderer>().enabled = true;
    }
    public void removeHour()
    {
        mIsHour = false;
        transform.Find("HourPower").GetComponent<SpriteRenderer>().enabled = false;
        if (mIsMin)
        {
            if (GetComponent<MeshRenderer>() != null)
                GetComponent<MeshRenderer>().material.color = minuteColor;
        }
        else
        {
            if (GetComponent<MeshRenderer>() != null)
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
        if (coll.gameObject.tag == "Player")
        {
            PlayerScript script = coll.gameObject.GetComponent<PlayerScript>();
            if (script.getHour())
            {
                mIsFrozen = true;
                if (GetComponent<MeshRenderer>() != null)
                    GetComponent<MeshRenderer>().material.color = frozenColor;
            }
            else if (script.getMin())
            {
                if (!mIsHour)
                {
                    mIsFrozen = false;
                    if (GetComponent<MeshRenderer>() != null)
                        GetComponent<MeshRenderer>().material.color = mStartColor;
                }
            }
        }
    }
    void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.tag == "Collectible")
        {
            int k = coll.gameObject.GetHashCode();
            if (!mPtsDic.ContainsValue(coll.gameObject))
            {
                mPtsDic[k] = coll.gameObject;
            }
        }
    }
}
