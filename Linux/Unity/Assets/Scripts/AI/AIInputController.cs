using UnityEngine;
using System.Collections;
using System;
using Prime31;
public class AIInputController : InputController {

	public EventType startDirection;
	private float horizontal;
	private float vertical;
	private CharacterController2D controller;
	private GameObject targetObject;
	public float attackSpeed = 1.0f;
	private float lastAttacktime;
	private int damage = 10;

	void Start(){
		controller = GetComponent<CharacterController2D> ();
		controller.onControllerCollidedEvent += OnCollide;
		if (startDirection == EventType.Left) {
			
			horizontal = -1;
		} else if (startDirection == EventType.Right) {
			horizontal = 1;
		} else if (startDirection == EventType.Jump) {
			vertical = 1;

		} else if (startDirection == EventType.Stop) {
			horizontal = 0;
			vertical = 0;
		}
	}


	public override float GetHorizontal ()
	{
		return horizontal;
	}

	public override float GetVertical ()
	{
		StartCoroutine (DisableJump ());
		return vertical;
	}

	private IEnumerator DisableJump(){
		yield return new WaitForSeconds (0.1f);
		vertical = 0;
	}

	void OnCollide(RaycastHit2D hit) {
		BlockingObject hitObject = hit.transform.GetComponent<BlockingObject> ();
		if (hitObject && !hitObject.GetComponent<AIInputController>()) {
			targetObject = hit.transform.gameObject;
		} else if (hit.transform.GetComponent<CharacterUIController> ()) {
			targetObject = hit.transform.gameObject;
		}

	}

	void FixedUpdate(){

		if (Time.time > lastAttacktime + attackSpeed && targetObject) {
			if (targetObject.GetComponent<CharacterUIController> ()) {
				lastAttacktime = Time.time;
				targetObject.GetComponent<CharacterUIController> ().DamageIncome (damage);
				targetObject = null;
			} else if (targetObject.GetComponent<BlockingObject> ()) {
				lastAttacktime = Time.time;
				targetObject.GetComponent<BlockingObject> ().DamageIncome (damage);
				targetObject = null;
			}
		}
	}

	void OnTriggerEnter2D(Collider2D coll) {
		//Debug.Log ("Enter: " + coll.gameObject.name);
		AIEventPoint type = coll.gameObject.GetComponent<AIEventPoint> ();
		if(type != null){
			if (type.eventType == EventType.Left) {
				horizontal = -1;
			} else if (type.eventType == EventType.Right) {
				horizontal = 1;
			} else if (type.eventType == EventType.Jump) {
				vertical = 1;

			} else if (type.eventType == EventType.Stop) {
				horizontal = 0;
				vertical = 0;
			}
		}
		if (coll.GetComponent<CharacterController2D> ()) {
			//horizontal = 0;
		}
	}
}