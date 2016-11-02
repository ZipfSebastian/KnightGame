using UnityEngine;
using System.Collections;

public class ThrowingDagger : MonoBehaviour {

	private bool initialized;
	private float speed;
	private Transform starter;
	private int damage;

	public void Init(float speed, Transform starter, int damage){
		this.speed = speed;
		this.damage = damage;
		this.starter = starter;
		initialized = true;
		if (speed > 0) {
			transform.localScale = new Vector2 (-transform.localScale.x, transform.localScale.y);
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (initialized) {
			transform.position = new Vector3 (transform.position.x + speed, transform.position.y,transform.position.z);
		}
	}

	public void OnTriggerEnter2D( Collider2D col )
	{
		if (initialized) {
			foreach (Transform trans in starter) {
				if (trans == col.transform) {
					return;
				}
			}
			if (initialized && col.transform != starter && !col.transform.GetComponent<ThrowingDagger>()) {
				initialized = false;

				transform.SetParent (col.transform, true);
				/*
				BlockingObject[] blockObjects = col.transform.GetComponentsInChildren<BlockingObject> ();
				if (blockObjects.Length > 0 && !blockObjects[0].name.Contains("Box")) {
					blockObjects[0].DamageIncome (damage);
					return;
				}*/
				BlockingObject[] blockObjects = col.transform.GetComponentsInParent<BlockingObject> ();
				if (blockObjects.Length > 0 && !blockObjects[0].name.Contains("Box")) {
					blockObjects[0].DamageIncome (damage);
					return;
				}
			}
		}
	}
}
