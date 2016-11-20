using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class OnlineGameController : Reciver
{
	public Transform characterTransform;
	public List<EnemyObject> enemyList = new List<EnemyObject> ();
	public EnemyObject enemyObject;
	public bool gameStarted = false;

	void Start(){
		Init ();
	}

	public override void OnRecive (string data)
	{
		try{
			Response res = JsonUtility.FromJson<Response> (data);
			if (res.type == CommunicationTypes.INIT_RESPONSE) {
				informationPanel.gameObject.SetActive (true);
				informationPanel.mainText.text = "Waiting for players...";
				informationPanel.btnOk.gameObject.SetActive (false);
			} else if (res.type == CommunicationTypes.START_GAME_RESPONSE) {
				LoadGame(JsonUtility.FromJson<StartGameResponse>(data));
			}else if(res.type == CommunicationTypes.POSITION_RESPONSE){
				PositionResponse response = JsonUtility.FromJson<PositionResponse>(data);
				RefreshPosition(response);
			}else if(res.type == CommunicationTypes.GAME_END_RESPONSE){
				gameStarted = false;
				SceneManager.LoadScene(SceenNames.MAIN_SCEEN);
			}else{
				Debug.Log("ParseError");
			}
		}catch(Exception e){
			Debug.Log (e.Message);
		}
	}

	private void RefreshPosition(PositionResponse resp){
		foreach (EnemyObject enemy in enemyList) {
			if (enemy.id == resp.id) {
				enemy.MoveSmothTo (resp.newPosition, resp.moveDirection);

			}
		}
	}

	private void LoadGame(StartGameResponse resp){
		gameStarted = true;
		informationPanel.gameObject.SetActive (false);
		informationPanel.btnOk.gameObject.SetActive (true);

		characterTransform.position = resp.position;
		foreach (Enemy enemy in resp.enemyList) {
			if (!ExistingEnemy (enemy.id)) {
				EnemyObject enemyRef = Instantiate (enemyObject).GetComponent<EnemyObject> ();
				enemyRef.transform.position = enemy.position;
				enemyRef.id = enemy.id;
				enemyList.Add (enemyRef);
			} else {
				EnemyObject existing = GetEnemy (enemy.id);
				existing.transform.position = enemy.position;
			}
		}
	}

	public EnemyObject GetEnemy(int id){
		foreach (EnemyObject enemy in enemyList) {
			if (enemy.id == id) {
				return enemy;
			}
		}
		return null;
	}

	public bool ExistingEnemy(int id){
		foreach (EnemyObject enemy in enemyList) {
			if (enemy.id == id) {
				return true;
			}
		}
		return false;
	}

	new public void Send(string message){
		if (gameStarted) {
			base.Send (message);
		}
	}

	public override void ConnectionResult (int res)
	{
		Debug.Log ("RESULT: " + res);
		base.ConnectionResult (res);
		if (res == NetworkInterface.CONNECTION_SUCESSFULL) {
			Request req = new Request ();
			req.type = CommunicationTypes.INIT_REQUEST;
			req.session = session;
			base.Send(JsonUtility.ToJson(req));
			informationPanel.gameObject.SetActive (true);
			informationPanel.mainText.text = "Waiting for players...";
			informationPanel.btnOk.gameObject.SetActive (false);
		}
	}
}

