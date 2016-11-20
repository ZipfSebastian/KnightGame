using UnityEngine;
using System.Collections;
using System.Net.Sockets;
using System.IO;
using System.Text;
using System.Timers;
using System;
using System.Net;
using System.Xml;
using AOT;

[RequireComponent(typeof(NetworkInterface))]
public class NetworkInterfaceUDP : MonoBehaviour
{
	public int maxReconnectTry = 1;
	public const int UNABLE_TO_CONNECT = 1;
	public const int CONNECTION_SUCESSFULL = 2;
	public const int ALLREDY_CONNECTED = 3;
	public const int CONNECTION_ABORT = 4;

//	private int nullMessageCount = 0;
	private Reciver reciver;
	private UdpClient mySocket;
	public int connectionStatus;
	private bool socketRedy = false;
	private int reconnectTry = 0;
	public bool active = true;
	public bool compressCommunication = true;
	private NetworkInterface interfaceTCP;
	private string ip;
	public int port;

	//private bool keyMessage = true;

	// Use this for initialization
	void Start()
	{
		interfaceTCP = GetComponent<NetworkInterface> ();
		reciver = GameObject.FindGameObjectWithTag("GameController").GetComponent<Reciver>();
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
	}

	public int Init()
	{
		ip = PlayerPrefs.GetString(interfaceTCP.ip);

		if (active == false)
		{
			return CONNECTION_SUCESSFULL;
		}
		if (!socketRedy)
		{
			try
			{
				mySocket = new UdpClient(ip, port);
				mySocket.Client.ReceiveTimeout = 1;
				socketRedy = true;
				connectionStatus = CONNECTION_SUCESSFULL;
				reciver.ConnectionResultUDP(CONNECTION_SUCESSFULL);
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
			//ICSharpCode.SharpZipLib.GZip.
			if (!socketRedy)
			{
				connectionStatus = UNABLE_TO_CONNECT;
				return;
			}
			string foo = "";
			if (compressCommunication)
			{
				foo = Gzip.Compress(message) + "\r\n";
			}

			else
			{
				foo = message + "\r\n";
			}

			byte[] data = GetBytes(foo);
			Debug.Log(data.Length);
			mySocket.Send(data, data.Length);
			//Debug.Log(foo);
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

	public byte[] GetBytes(string data)
	{
		MemoryStream fs = new MemoryStream();
		TextWriter tx = new StreamWriter(fs);

		tx.WriteLine(data);

		tx.Flush();
		fs.Flush();
		byte[] bytes = fs.ToArray();
		return bytes;
	}

	public string GetString(byte[] data)
	{
		char[] chars = new char[data.Length / sizeof(char)];
		System.Buffer.BlockCopy(data, 0, chars, 0, data.Length);
		return new string(chars);
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
			try
			{
				if (mySocket.Available>0)
				{

					IPEndPoint endPoint = new IPEndPoint(IPAddress.Any, port);
					byte[] messageData = mySocket.Receive(ref endPoint);
					string message = System.Text.Encoding.Default.GetString(messageData);
					//Debug.Log(message);
					if (compressCommunication)
					{
						//Debug.Log(Gzip.Decompress(message));
						reciver.OnRecive(Gzip.Decompress(message));

					}
					else
					{
						reciver.OnRecive(message);
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