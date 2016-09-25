using UnityEngine;
using System.Collections;

public class CannonScript : MonoBehaviour {

    protected Object[] mCollectibles;
    public Transform mOrigin;

    protected bool fired = false;
    protected GameObject mMinuteHand;
    protected Random mRand;

	// Use this for initialization
	void Start () {
        mCollectibles = new Object[4] { Resources.Load("Prefabs/Powerup (Growth)"),
                                        Resources.Load("Prefabs/Powerup (pSpeed)"),
                                        Resources.Load("Prefabs/Powerup (cSpeed)"),
                                        Resources.Load("Prefabs/Powerup (Shoot)")};


        mMinuteHand = GameObject.Find("MinuteHandOrigin");
        mRand = new Random();
	}
	
	// Update is called once per frame
	void Update () {
        float min = mMinuteHand.transform.rotation.eulerAngles.y;
        if (min > 269.5f && min < 270.5f)
        {
            fire();
        }
	
	}

    void fire()
    {
        GameObject collectibleCloneObj = Instantiate(mCollectibles[Random.Range(0,3)], mOrigin.position, Quaternion.identity) as GameObject;
        Rigidbody collectibleClone = collectibleCloneObj.GetComponent<Rigidbody>();
        Vector3 force = (mMinuteHand.transform.position - mOrigin.position);
        force.x += Random.Range(-40f,40f);
        force.y = Random.Range(100f,200f);
        force.z = Random.Range(0f, -80f);

        force.Normalize();
        collectibleClone.AddForce(force * 1200);
        fired = true;
    }
}
