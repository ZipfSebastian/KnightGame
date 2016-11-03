using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Elevator : MonoBehaviour {

    public Transform[] positions;
    private List<Vector3> lockedPositions;
    public bool random;
    public float speed;
    private Vector3 nextPosition;
    private int index  = 0;
    private bool started = false;
    public float precizion;
    Dictionary<Transform, Transform> defautParent;

    // Use this for initialization
    void Start () {
        defautParent = new Dictionary<Transform, Transform>();
        lockedPositions = new List<Vector3>();
        foreach(Transform pos in positions) {
            lockedPositions.Add(pos.position);
        }
        if (lockedPositions.Count > 0 && !random) {
            nextPosition = lockedPositions[0];
        }else if (lockedPositions.Count > 0) {
            nextPosition = lockedPositions[Random.Range(0, lockedPositions.Count)];
        }
        started = true;
	}
    /*
    void OnCollisionStay2D(Collision2D coll) {
    }

    void OnCollisionEnter2D(Collision2D coll) {
        if (defautParent.ContainsKey(coll.transform)) {
            defautParent[coll.transform] = coll.transform.parent;
        }else {
            defautParent.Add(coll.transform, coll.transform.parent);
        }
        coll.transform.SetParent(transform);

    }

    void OnCollisionExit2D(Collision2D coll) {
        Vector3 lastPos = coll.transform.position;
        Vector3 lastScale = coll.transform.lossyScale;
        coll.transform.SetParent(defautParent[coll.transform]);
        coll.transform.position = lastPos;
        coll.transform.localScale = lastScale;
    }
    */
    
	
	// Update is called once per frame
	void Update () {
        if (lockedPositions.Count > 0 && started) {
            if (Vector3.Distance(transform.position, nextPosition)>precizion) {
                Vector3 step = Vector3.MoveTowards(transform.position, nextPosition, speed);
                transform.position = step;
                //Debug.Log(step + "; " + transform.position + "; " + nextPosition);
            }else {
                GoToNext();
            }
        }
	}

    public void GoToNext() {
        if(index>= lockedPositions.Count-1) {
            index = 0;
        }
        else {
            index++;
        }
        if (random) {
            nextPosition = lockedPositions[Random.Range(0, lockedPositions.Count)];
        }else {
            nextPosition = lockedPositions[index];
        }
        
    }
}
