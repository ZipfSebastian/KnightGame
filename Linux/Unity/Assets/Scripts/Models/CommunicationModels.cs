using UnityEngine;
using System;
using System.Collections.Generic;

[Serializable]
public class Request{
	public string type;
	public string session;
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

[Serializable]
public class MoveRequest : Request{
	public Vector2 newPosition;
	public Vector2 moveDirection;
}

[Serializable]
public class LoadGameResponse : Response{
	public int mapID;

}

[Serializable]
public class StartGameResponse : Response{
	public List<Enemy> enemyList;
	public Vector2 position;
}

[Serializable]
public class Enemy{
	public int id;
	public Vector2 position;
}

[Serializable]
public class PositionResponse : Response{
	public int id;
	public Vector2 newPosition;
	public Vector2 moveDirection;
}


public class CommunicationTypes{
	public const string LOGIN_REQUEST_TYPE = "handlers.LoginRequest";
	public const string PING_REQUEST = "handlers.PingRequest";
	public const string SEARCH_REQUEST = "handlers.SearchRequest";
	public const string INIT_REQUEST = "handlers.InitRequest";
	public const string MOVE_REQUEST = "handlers.MoveRequest";

	public const string LOGIN_RESPONSE = "LoginResponse";
	public const string SEARCH_RESPONSE = "SearchResponse";
	public const string LOAD_GAME_RESPONSE = "LoadGameResponse";
	public const string START_GAME_RESPONSE = "StartGameResponse";
	public const string INIT_RESPONSE = "InitResponse";
	public const string POSITION_RESPONSE = "PositionResponse";
}
