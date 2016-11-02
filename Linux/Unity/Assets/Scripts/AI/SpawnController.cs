using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class SpawnController : MonoBehaviour {

	private List<SpawnPoint> spawnPoints;
	public float spawnTime;
	public GameObject enemyObject;
	public Transform aiContainer;
	private float lastTime;
	public bool spawnEnabled = false;

	// Use this for initialization
	void Start () {
		spawnPoints = new List<SpawnPoint> ();
		Transform spawnParent = GameObject.FindGameObjectWithTag ("SpawnPoints").transform;
		foreach (Transform trans in spawnParent) {
			spawnPoints.Add (trans.GetComponent<SpawnPoint>());
		}
	}

	void FixedUpdate(){
		if (Time.time > lastTime + spawnTime && spawnEnabled) {
			GameObject aiTempRef = Instantiate (enemyObject);
			aiTempRef.transform.SetParent (aiContainer, true);
			int rand = UnityEngine.Random.Range (0, spawnPoints.Count);
			aiTempRef.transform.position = spawnPoints [rand].transform.position;
			aiTempRef.GetComponent<AIInputController> ().startDirection = spawnPoints [rand].startDirection;
			lastTime = Time.time;

		}
	}
}
