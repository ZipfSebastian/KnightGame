using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Login : Reciver {
    
    public const string PREF_LOGIN_SESSION = "pref.login.session";
    public InputField usernameInput;
    public InputField passwordInput;
    public Button btnLogin;
    private Button btnEvent;
    private Text informationText;
    private Text btnEventText;


    // Use this for initialization
    void Start () {
        
        canvas = GameObject.FindGameObjectWithTag("Canvas").GetComponent<Canvas>();
        btnLogin.onClick.AddListener(() => OnLoginClick());
        informationPanel = canvas.transform.FindChild("InformationPanel").gameObject;
        frontPanel = informationPanel.transform.FindChild("FrontPanel").gameObject;
        btnEvent = frontPanel.transform.FindChild("ButtonOk").GetComponent<Button>();
        btnEventText = btnEvent.transform.FindChild("Text").GetComponent<Text>();
        
        informationText = frontPanel.transform.FindChild("MainText").GetComponent<Text>();
        StartCoroutine(Connect());
    }

    private void OnLoginClick() {
        //Application.LoadLevel("Game");
		LoginRequest loginRequest = new LoginRequest();
		loginRequest.password = passwordInput.text;
		loginRequest.userName = usernameInput.text;
		loginRequest.type = CommunicationTypes.LOGIN_REQUEST_TYPE;
		Send(JsonUtility.ToJson(loginRequest));
    }

    public void OnFacebookLogin() {

    }

    public void OnGoogleLogin() {
        //GooglePlayController.Init();
    }

    public override void OnRecive(string data) {
        try{
			LoginResponse resp = JsonUtility.FromJson<LoginResponse>(data);
			if(resp.succes){
				Debug.Log("Success");
			}else{
				informationPanel.SetActive(true);
				informationText.text = "Bad username or password!";
				btnEvent.onClick.RemoveAllListeners();
				btnEvent.onClick.AddListener(() => HideInformationPanel());
				btnEventText.text = "Ok";
			}
		}catch(Exception e){
			Debug.Log (e);
		}
    }

    private void HideInformationPanel() {
        informationPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update () {

	}


}
