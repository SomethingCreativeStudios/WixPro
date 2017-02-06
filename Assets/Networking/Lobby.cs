using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using Photon;

public class Lobby : PunBehaviour {

    /// <summary>
    /// The PUN loglevel.
    /// </summary>
    public PhotonLogLevel Loglevel = PhotonLogLevel.Informational;


    /// <summary>
    /// The maximum number of players per room. When a room is full, it can't be joined by new players, and so new room will be created.
    /// </summary>  
    [Tooltip("The maximum number of players per room. When a room is full, it can't be joined by new players, and so new room will be created")]
    public byte MaxPlayersPerRoom = 2;

    // Use this for initialization
    void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {

	}

    void Awake()
    {
        
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinRandomRoom(); //look for non full rooms
    }


    public override void OnDisconnectedFromPhoton()
    {
        Debug.LogWarning("DemoAnimator/Launcher: OnDisconnectedFromPhoton() was called by PUN");
    }

    public override void OnPhotonRandomJoinFailed(object[] codeAndMsg)
    { 
        //Could not find room, fine I'll make my own... with blackjack and hookers
        PhotonNetwork.CreateRoom(null, new RoomOptions() { MaxPlayers = MaxPlayersPerRoom }, null); 
    }

    public override void OnJoinedRoom()
    {
       
    }
}
