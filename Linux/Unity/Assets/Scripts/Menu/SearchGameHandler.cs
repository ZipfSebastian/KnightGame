using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SearchGameHandler : MonoBehaviour {

	public MainMenuController mainController;
	public GameObject searchPanel;
	public Animator canvasAnimator;

	public void OnSearchClick(){
		SearchRequest request = new SearchRequest ();
		request.type = CommunicationTypes.SEARCH_REQUEST;
		request.state = true;
		request.session = mainController.session;
		mainController.SendMessage(JsonUtility.ToJson(request));
	}

	public void OnRecive(string message){
		Response res = JsonUtility.FromJson<Response> (message);
		if (res.type == CommunicationTypes.SEARCH_RESPONSE) {
			SearchResponse resp = JsonUtility.FromJson<SearchResponse> (message);
			if (resp.matchFind == false) {
				searchPanel.SetActive (true);
			} 
		}else if(res.type == CommunicationTypes.LOAD_GAME_RESPONSE){
			LoadGameResponse loadResponse = JsonUtility.FromJson<LoadGameResponse> (message);
			SceneManager.LoadScene ("OnlineGame");
		}
	}

	public void OnCancelClick(){
		SearchRequest request = new SearchRequest ();
		request.type = CommunicationTypes.SEARCH_REQUEST;
		request.state = false;
		request.session = mainController.session;
		mainController.SendMessage(JsonUtility.ToJson(request));
		searchPanel.SetActive (false);
	}

	public void OnBackClick(){
		canvasAnimator.SetBool(AnimationNames.LOGIN_TO_SEARCH, false);
	}
}
