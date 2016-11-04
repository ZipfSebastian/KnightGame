using UnityEngine;
using System.Collections;
using System.Net.Sockets;
using System.IO;
using System.Text;
using System.Timers;
using System;
using System.Net;

public class NetworkInterface : MonoBehaviour{

	public string ip;
	public int port;
	public int maxReconnectTry = 1;
	public const int UNABLE_TO_CONNECT = 1;
	public const int CONNECTION_SUCESSFULL = 2;
	public const int ALLREDY_CONNECTED = 3;
	public const int CONNECTION_ABORT = 4;

	private int nullMessageCount = 0;
	private Reciver reciver;
	private TcpClient mySocket;
	public int connectionStatus;
	private NetworkStream theStream;
	private StreamWriter theWriter;
	private StreamReader theReader;
	private bool socketRedy = false;
	private int reconnectTry = 0;
	public bool active = true;
	public bool compressCommunication = true;
	private float lastUpdateTime;
	public float pingTime;

	//private bool keyMessage = true;

	// Use this for initialization
	void Start()
	{
		lastUpdateTime = Time.time;
	}

	// Update is called once per frame
	void FixedUpdate()
	{
		if (socketRedy)
		{
			Recive();
		}
		else if (connectionStatus != UNABLE_TO_CONNECT)
		{
			connectionStatus = UNABLE_TO_CONNECT;
		}
		//Debug.Log ("test");
		if (connectionStatus != UNABLE_TO_CONNECT)
		{
			//Debug.Log ("Ping0");
			if (Time.time > lastUpdateTime + pingTime)
			{
				lastUpdateTime = Time.time;
				Request request = new Request();
				request.type = CommunicationTypes.PING_REQUEST;
				//Debug.Log ("Ping");
				Send(JsonUtility.ToJson(request));
			}
		}
	}

	public int Init()
	{
		reciver = GameObject.FindGameObjectWithTag("GameController").GetComponent<Reciver>();
		if (active == false)
		{
			return CONNECTION_SUCESSFULL;
		}
		if (!socketRedy)
		{
			try
			{
				mySocket = new TcpClient(ip, port);
				//mySocket.
				theStream = mySocket.GetStream();
				theWriter = new StreamWriter(theStream);
				theReader = new StreamReader(theStream);
				mySocket.ReceiveTimeout = 1;
				mySocket.ReceiveBufferSize = 2048;
				socketRedy = true;
				connectionStatus = CONNECTION_SUCESSFULL;
				reciver.ConnectionResult(CONNECTION_SUCESSFULL);
				//reconnectTry = 0;
				//SendPublicKey();
				return CONNECTION_SUCESSFULL;
			}
			catch (Exception)
			{
				connectionStatus = UNABLE_TO_CONNECT;
				reciver.ConnectionResult(UNABLE_TO_CONNECT);
				if (reconnectTry < maxReconnectTry)
				{
					reconnectTry++;
					return Init();
				}
				else
				{
					return UNABLE_TO_CONNECT;
				}
			}
		}
		else
		{
			connectionStatus = ALLREDY_CONNECTED;
			reciver.ConnectionResult(ALLREDY_CONNECTED);
			return ALLREDY_CONNECTED;
		}
	}

	public void Disconnect()
	{
		if (active == false)
		{
			return;
		}
		if (!socketRedy)
		{
			connectionStatus = UNABLE_TO_CONNECT;
			return;
		}
		theWriter.Close();
		theReader.Close();
		mySocket.Close();
		mySocket = null;
	}

	public void Send(string message)
	{
		if (active == false)
		{
			return;
		}
		//string sendingMessage = message;
		try
		{
			if (!socketRedy)
			{
				connectionStatus = UNABLE_TO_CONNECT;
				return;
			}
			string foo = "";
			if (compressCommunication)
			{
				// foo = Gzip.Compress(message) + "\r\n";
			}
			else
			{
				foo = message + "\r\n";
			}
			//Debug.Log(message);
			theWriter.Write(foo);
			theWriter.Flush();
		}
		catch (Exception e)
		{
			//Server closed the socket!
			socketRedy = false;
			connectionStatus = UNABLE_TO_CONNECT;
			if (reconnectTry < maxReconnectTry)
			{
				reconnectTry++;
				Init();
			}
			Debug.Log(e.Message);
			socketRedy = false;
			connectionStatus = UNABLE_TO_CONNECT;
			reciver.ConnectionResult(UNABLE_TO_CONNECT);
			Disconnect();
		}
	}
	//Timeot, flush, readToEnd
	public void Recive()
	{
		if (active == false)
		{
			return;
		}
		if (socketRedy)
		{
			string message = "";
			try
			{
				if (theStream.CanRead && theStream.DataAvailable)
				{
					message += (char)theReader.Read();
					message += theReader.ReadLine();
					//message = encriptor.Decryption(message);

					Debug.Log("RECV DATA: " + message);
					if (message != null && message != string.Empty && message != " " && message.Length > 1)
					{
						//Debug.Log("RECV: " + Gzip.Decompress(message));
						if (compressCommunication)
						{
							// reciver.OnRecive(Gzip.Decompress(message));
						}
						else
						{
							reciver.OnRecive(message);
						}
						nullMessageCount = 0;
					}
					else
					{
						nullMessageCount++;

						if (nullMessageCount > 5)
						{
							socketRedy = false;
							connectionStatus = UNABLE_TO_CONNECT;
							reciver.ConnectionResult(UNABLE_TO_CONNECT);
							Disconnect();
						}
					}
				}
			}
			catch (IOException)
			{
				//Recv time out.. it's okey
			} /*catch (System.ArgumentException ea) {
                Debug.Log(ea.Message + ": " + message);
            }catch (Exception e) {
                //Recv exception unable to send to server
                socketRedy = false;
                connectionStatus = UNABLE_TO_CONNECT;
                if (reconnectTry < maxReconnectTry) {
                    Init();
                }
                Debug.Log(e.Message +"; "+ e.GetType());
            }*/
		}
		else
		{
			//Debug.Log("Socket not redy!");
			connectionStatus = UNABLE_TO_CONNECT;
			return;
		}
	}


	/*
    private void ReciveServerKey() {
        byte[] serverKey = new byte[140];
        theStream.Read(serverKey, 0, 140);
        string keyString = "<RSAKeyValue><Modulus>";
        keyString += Convert.ToBase64String(serverKey);
        keyString += "</Modulus><Exponent>EQ==</Exponent></RSAKeyValue>";
        encriptor.serverPublicKey = keyString;
        Debug.Log(Convert.ToBase64String(serverKey));
        keyMessage = false;
        SendPublicKey();
        
        
    }*/
}
