using System;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuController : Reciver
{

	public Canvas mainCanvas;
	//public GameObject informationPanel;
	public Button btnEvent;
	public Text btnEventText;
	public Text informationText;
	public LoginController loginController;
	public SearchGameHandler searchGameController;

	void Start(){
		StartCoroutine(Connect());
	}

	public override void OnRecive(string data) {
		try{
			Response resp = JsonUtility.FromJson<Response>(data);
			if(resp.type == CommunicationTypes.LOGIN_RESPONSE){
				loginController.OnRecive(data);
			}else if(resp.type == CommunicationTypes.SEARCH_RESPONSE){
				searchGameController.OnRecive(data);
			}
		}catch(Exception e){
			Debug.Log (e);
		}
	}

	private void HideInformationPanel() {
		informationPanel.SetActive(false);
	}

	public void SendMessage(string message){
		base.Send (message);
	}
}

