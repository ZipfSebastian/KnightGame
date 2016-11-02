using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
	private SpawnController spawnController;


	void Start(){
		spawnController = GetComponent<SpawnController> ();
		spawnController.spawnEnabled = true;
	}

	public void RestartGame(){
		SceneManager.LoadScene (SceneManager.GetActiveScene ().name);
	}
}

