using UnityEngine;
using System.Collections;

public class CollectibleScript : MonoBehaviour {

    public bool bInBound = false;
    protected GameObject mBoundTo;
    protected Vector3 mVelocity;

    public float mSpdConst = 1f;

	// Use this for initialization
	void Start () {
        mVelocity = new Vector3(0, 0);
	}
	
	// Update is called once per frame
	void Update () {
        if (bInBound)
        {
            mVelocity = (mBoundTo.GetComponent<Transform>().position - this.GetComponent<Transform>().position).normalized * mSpdConst * Time.deltaTime;
            this.GetComponent<Transform>().Translate(mVelocity);
        }
	}

    void OnTriggerEnter(Collider coll)
    {
        // if the collectible isn't already being picked up, and the collision is with a player:
        if (!bInBound && coll.gameObject.tag == "Player")
        {
            mBoundTo = coll.gameObject;
            bInBound = true;
        }
    }
}
