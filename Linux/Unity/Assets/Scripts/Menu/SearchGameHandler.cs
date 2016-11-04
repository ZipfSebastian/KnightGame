using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SearchGameHandler : MonoBehaviour {

	public MainMenuController mainController;
	public GameObject searchPanel;

	public void OnSearchClick(){
		SearchRequest request = new SearchRequest ();
		request.type = CommunicationTypes.SEARCH_REQUEST;
		request.state = true;
		request.session = mainController.session;
		mainController.SendMessage(JsonUtility.ToJson(request));
	}

	public void OnRecive(string message){
		SearchResponse resp = JsonUtility.FromJson<SearchResponse> (message);
		if (resp.matchFind == false) {
			searchPanel.SetActive (true);
		} else {
			SceneManager.LoadScene (SceenNames.GAME_SCEEN);
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
}
