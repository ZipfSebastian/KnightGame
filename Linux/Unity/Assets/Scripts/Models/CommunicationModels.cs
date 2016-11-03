using UnityEngine;
using System;

[Serializable]
public class LoginRequest{
	public string type;
	public string userName;
	public string password;
}

[Serializable]
public class LoginResponse{
	public string type;
	public bool succes;
	public string session;
}

[Serializable]
public class PingRequest{
	public string type;
}

public class CommunicationTypes{
	public const string LOGIN_REQUEST_TYPE = "handlers.LoginRequest";
	public const string PING_REQUEST = "handlers.PingRequest";
}