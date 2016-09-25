using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerScript : MonoBehaviour
{
    public struct PlayerData
    {
        int playerNum;
        int hatNum;
        int accessoryNum;
        int animNum;
    }


    public int mControllerNum = 1;
    public float speed = 0.3f;
    public GameObject mScoreBoard;

    bool mIsMin;
    bool mIsHour;
    bool mIsFrozen;
    float mHealth;
    Color mStartColor;
    ScoreHolder mScoreScript;
    Animator mAnim;
    MatchControllerScript matchScript;

    static Color frozenColor = Color.cyan;
    static Color hourColor = Color.white;
    static Color minuteColor = Color.black;

    protected Dictionary<int, GameObject> mPtsDic = new Dictionary<int, GameObject>();

    // Use this for initialization
    void Start()
    {
        matchScript = Camera.main.GetComponent<MatchControllerScript>();
        mAnim = GetComponent<Animator>();
        if (mAnim != null)
            mAnim.enabled = true;
        if (GetComponent<MeshRenderer>() != null)
            mStartColor = GetComponent<MeshRenderer>().material.color;

        mHealth = 24;
        mScoreScript = mScoreBoard.GetComponent<ScoreHolder>();
        mScoreScript.setScore((int)mHealth);
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
            mHealth -= Time.deltaTime * 100;
            mScoreScript.setScore((int)mHealth);
            if(mHealth < 0)
            {
                matchScript.setDeath(mControllerNum);
                Destroy(gameObject);
            }
            transform.Find("FrozenSign").GetComponent<SpriteRenderer>().enabled = true;
        }

        if (mPtsDic.Count > 0)
        {
            int[] keys = (new List<int>(mPtsDic.Keys)).ToArray();
            for (int i = 0; i < keys.Length; i++)
            {
                int key = keys[i];
                GameObject tmp = mPtsDic[key];
                if (tmp != null && (transform.position - mPtsDic[key].transform.position).magnitude <= 3.0f)
                {
                    mPtsDic.Remove(key);
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
        GameObject minutePower = transform.Find("MinutePower").gameObject;
        if (minutePower.GetComponent<SpriteRenderer>() != null)
        {
            transform.Find("minutePower").GetComponent<SpriteRenderer>().enabled = true;
        }
        else
        {
            minutePower.transform.GetChild(0).GetComponent<MeshRenderer>().enabled = true;
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
        GameObject minutePower = transform.Find("MinutePower").gameObject;
        if (minutePower.GetComponent<SpriteRenderer>() != null)
        {
            transform.Find("minutePower").GetComponent<SpriteRenderer>().enabled = false;
        }
        else
        {
            minutePower.transform.GetChild(0).GetComponent<MeshRenderer>().enabled = false;
        }
    }

    public void setHour()
    {
        mIsHour = true;
        mIsFrozen = false;
        if (GetComponent<MeshRenderer>() != null)
            GetComponent<MeshRenderer>().material.color = hourColor;
        GameObject hourPower = transform.Find("HourPower").gameObject;
        if(hourPower.GetComponent<SpriteRenderer>() != null)
        {
            transform.Find("HourPower").GetComponent<SpriteRenderer>().enabled = true;
        }
        else
        {
            hourPower.transform.GetChild(0).GetComponent<MeshRenderer>().enabled = true;
        }
    }
    public void removeHour()
    {
        mIsHour = false;
        GameObject hourPower = transform.Find("HourPower").gameObject;
        if (hourPower.GetComponent<SpriteRenderer>() != null)
        {
            transform.Find("HourPower").GetComponent<SpriteRenderer>().enabled = false;
        }
        else
        {
            hourPower.transform.GetChild(0).GetComponent<MeshRenderer>().enabled = false;
        }
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
                freeze();
            }
            else if (script.getMin())
            {
                if (!mIsHour)
                {
                    unfreeze();
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

    void freeze()
    {
        mIsFrozen = true;
        if (GetComponent<MeshRenderer>() != null)
            GetComponent<MeshRenderer>().material.color = frozenColor;
        matchScript.setFrozen(mControllerNum);
    }

    void unfreeze()
    {
        mIsFrozen = false;
        if (GetComponent<MeshRenderer>() != null)
            GetComponent<MeshRenderer>().material.color = mStartColor;
        matchScript.removeFrozen(mControllerNum);
    }
}
