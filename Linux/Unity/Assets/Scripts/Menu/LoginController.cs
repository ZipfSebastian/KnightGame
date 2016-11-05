using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class LoginController : MonoBehaviour {
    
    public const string PREF_LOGIN_SESSION = "pref.login.session";
	public const string PREF_LOGIN_USERNAME = "pref.login.userName";
	public const string PREF_LOGIN_PASSWORD = "pref.login.password";
	public const string PREF_REMEMBER_ME = "pref.remember.me";
    public InputField usernameInput;
    public InputField passwordInput;
    public Button btnLogin;
	public Animator canvasAnimator;
	public MainMenuController mainController;
	public Toggle rememberMe;


    // Use this for initialization
    void Start () {
        btnLogin.onClick.AddListener(() => OnLoginClick());
		if (PlayerPrefs.GetInt (PREF_REMEMBER_ME, 0) == 1) {
			usernameInput.text = PlayerPrefs.GetString (PREF_LOGIN_USERNAME);
			passwordInput.text = PlayerPrefs.GetString (PREF_LOGIN_PASSWORD);
			rememberMe.isOn = true;
		}
    }

    private void OnLoginClick() {
        //Application.LoadLevel("Game");
		if (rememberMe.isOn) {
			PlayerPrefs.SetString (PREF_LOGIN_USERNAME, usernameInput.text);
			PlayerPrefs.SetString (PREF_LOGIN_PASSWORD, passwordInput.text);
			PlayerPrefs.SetInt (PREF_REMEMBER_ME, 1);
		} else {
			PlayerPrefs.SetString (PREF_LOGIN_USERNAME, "");
			PlayerPrefs.SetString (PREF_LOGIN_PASSWORD, "");
			PlayerPrefs.SetInt (PREF_REMEMBER_ME, 0);
		}
		LoginRequest loginRequest = new LoginRequest ();
		loginRequest.password = passwordInput.text;
		loginRequest.userName = usernameInput.text;
		loginRequest.type = CommunicationTypes.LOGIN_REQUEST_TYPE;
		mainController.SendMessage (JsonUtility.ToJson (loginRequest));
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
