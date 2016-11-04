using UnityEngine;
using System;

[Serializable]
public class Request{
	public string type;
}

[Serializable]
public class Response{
	public string type;
}

[Serializable]
public class LoginRequest: Request{
	
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
public class SearchRequest : Request{
	public bool state;
	public string session;
}

[Serializable]
public class SearchResponse : Response{
	public string message;
	public bool success;
	public bool matchFind;
}

public class CommunicationTypes{
	public const string LOGIN_REQUEST_TYPE = "handlers.LoginRequest";
	public const string PING_REQUEST = "handlers.PingRequest";
	public const string SEARCH_REQUEST = "handlers.SearchRequest";


	public const string LOGIN_RESPONSE = "LoginResponse";
	public const string SEARCH_RESPONSE = "SearchResponse";
}
