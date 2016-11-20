using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Reciver : MonoBehaviour {
	protected NetworkInterface networkInterface;
	public InformationPanel informationPanel;
	private NetworkInterfaceUDP networkInterfaceUDP;
	public string session;
	protected float lastUpdateTime;
	protected float updateTime = 0.5f;
	protected bool connected = false;
	protected bool connectedUDP = false;
	public bool canSendPingTCP = true;
	public bool canSendPingUDP = false;
	private float lastUpdateTimeUDP;

	public virtual void OnRecive(string data) {

	}

	void Awake() {
		session = PlayerPrefs.GetString(LoginController.PREF_LOGIN_SESSION);
		networkInterface = GameObject.FindGameObjectWithTag("NetworkInterface").GetComponent<NetworkInterface>();
		networkInterfaceUDP = GameObject.FindGameObjectWithTag("NetworkInterface").GetComponent<NetworkInterfaceUDP>();

	}

	// Use this for initialization
	protected void Init() {
		if (networkInterface != null)
		{
			networkInterface.Init();
		}
		if(networkInterfaceUDP != null)
		{
			networkInterfaceUDP.Init();
		}
	}

	public virtual void ConnectionResult(int res) {
		int connectionStatus = networkInterface.connectionStatus;
		Debug.Log (res);
		if (connectionStatus == NetworkInterface.CONNECTION_SUCESSFULL) {
			connected = true;
			lastUpdateTime = Time.time;
			informationPanel.gameObject.SetActive(false);
			informationPanel.btnOk.gameObject.SetActive(true);
		} else if (connectionStatus == NetworkInterface.UNABLE_TO_CONNECT) {
			informationPanel.gameObject.SetActive(true);
			informationPanel.mainText.text = "Unable to connect to server!";
			informationPanel.buttonText.text = "Retry";
			informationPanel.btnOk.gameObject.SetActive(true);
			informationPanel.btnOk.onClick.RemoveAllListeners();
			informationPanel.btnOk.onClick.AddListener(() => StartCoroutine(Connect()));
			Transform backObject = informationPanel.backButtton.transform;
			if(backObject != null)
			{
				backObject.GetComponent<Button>().onClick.RemoveAllListeners();
				backObject.GetComponent<Button>().onClick.AddListener(() => BackToOptions());
				backObject.gameObject.SetActive(true);
			}
		}
	}

	public virtual void ConnectionResultUDP(int res)
	{
		int connectionStatus = networkInterface.connectionStatus;
		if (connectionStatus == NetworkInterface.CONNECTION_SUCESSFULL)
		{
			connectedUDP = true;
		}
	}

	public void BackToOptions()
	{
		//SceneManager.LoadScene(SceenNames.MAIN);
	}

	protected IEnumerator Connect() {
		informationPanel.gameObject.SetActive(true);
		informationPanel.mainText.text = "Connecting..";
		informationPanel.btnOk.gameObject.SetActive(false);
		yield return new WaitForSeconds(0.5f);
		Init();
	}

	public void Send(string message) {
		networkInterface.Send(message);
	}

	public void SendUDP(string message)
	{
		networkInterfaceUDP.Send(message);
	}


	// Update is called once per frame
	void FixedUpdate() {
		/*if(connected && lastUpdateTime + updateTime < Time.time && canSendPingTCP)
		{
			Send(new PingRequest(RequestTypes.PING_REQUEST, session).ToJson());
			lastUpdateTime = Time.time;
		}
		if(connectedUDP&& lastUpdateTimeUDP + updateTime < Time.time && canSendPingUDP)
		{
			SendUDP(new PingRequest(RequestTypes.PING_REQUEST, session).ToJson());
			lastUpdateTimeUDP = Time.time;
		}*/
	}

	public void ShowInformationPanelWithButton(string message)
	{
		informationPanel.gameObject.SetActive(true);
		informationPanel.mainText.text = message;
		informationPanel.buttonText.text = "Ok";
		informationPanel.btnOk.gameObject.SetActive(true);
		informationPanel.btnOk.onClick.RemoveAllListeners();
		informationPanel.btnOk.onClick.AddListener(() => OnInformationPanelOkClick());
		Transform backObject = informationPanel.backButtton.transform;
		if (backObject != null)
		{
			backObject.gameObject.SetActive(false);
		}
	}

	public void ShowInformationPanelWithoutButton(string message) {
		informationPanel.gameObject.SetActive(true);
		informationPanel.mainText.text = message;
		informationPanel.btnOk.gameObject.SetActive(false);
	}

	public void OnInformationPanelOkClick() {
		informationPanel.gameObject.SetActive(false);
	}
}
