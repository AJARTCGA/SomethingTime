using UnityEngine;
using System.Collections;

public class DoorTriggerScript : MonoBehaviour {

    Collider childColl;
	// Use this for initialization
	void Start ()
    {
        childColl = transform.GetChild(0).GetComponent<BoxCollider>();
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    void OnTriggerEnter(Collider coll)
    {
        Physics.IgnoreCollision(childColl, coll, true);
    }

    void OnTriggerExit(Collider coll)
    {
        Physics.IgnoreCollision(childColl, coll, false);
    }
}
