using UnityEngine;

public class RightCollider: MonoBehaviour {
    public bool collision = false;

    void OnTriggerEnter(Collider coll) 
        {
        if (coll.gameObject.transform.parent != null)
        {
            if (coll.gameObject.transform.parent.name == "Environment")
                collision = true;
        }

    }

    void OnTriggerExit(Collider coll) 
        {
        if (coll.gameObject.transform.parent != null)
        {
            if (coll.gameObject.transform.parent.name == "Environment")
                collision = false;
        }
    }

    void OnTriggerStay(Collider coll) {

        if (coll.gameObject.transform.parent != null)
        {
            if (coll.gameObject.transform.parent.name == "Environment")
                collision = true;
        }
    }
}
