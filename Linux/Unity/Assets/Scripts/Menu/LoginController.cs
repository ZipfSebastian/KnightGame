using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class LoginController : MonoBehaviour {
    
    public const string PREF_LOGIN_SESSION = "pref.login.session";
    public InputField usernameInput;
    public InputField passwordInput;
    public Button btnLogin;
	public Animator canvasAnimator;
	public MainMenuController mainController;


    // Use this for initialization
    void Start () {
        btnLogin.onClick.AddListener(() => OnLoginClick());
    }

    private void OnLoginClick() {
        //Application.LoadLevel("Game");
		LoginRequest loginRequest = new LoginRequest();
		loginRequest.password = passwordInput.text;
		loginRequest.userName = usernameInput.text;
		loginRequest.type = CommunicationTypes.LOGIN_REQUEST_TYPE;
		mainController.SendMessage(JsonUtility.ToJson(loginRequest));
    }


    public void OnRecive(string data) {
		LoginResponse resp = JsonUtility.FromJson<LoginResponse>(data);
		if(resp.succes){
			mainController.session = resp.session;
			PlayerPrefs.SetString(LoginController.PREF_LOGIN_SESSION,resp.session);
			canvasAnimator.SetBool(AnimationNames.LOGIN_TO_SEARCH, true);
		}else{
			mainController.informationPanel.SetActive(true);
			mainController.informationText.text = "Bad username or password!";
			mainController.btnEvent.onClick.RemoveAllListeners();
			mainController.btnEvent.onClick.AddListener(() => HideInformationPanel());
			mainController.btnEventText.text = "Ok";
		}
    }

	private void HideInformationPanel() {
		mainController.informationPanel.SetActive(false);
	}



}
