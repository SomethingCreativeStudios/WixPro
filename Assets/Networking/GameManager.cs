using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using Photon;

/****************************************************
*
* THIS IS A COPY OF THE ONLINE DOCS
* http://doc.photonengine.com/en-us/pun/current/tutorials/pun-basics-tutorial/game-scenes
*
*****************************************************/
public class GameManager : PunBehaviour{

    #region Photon Messages

    private bool loadedLobby = false;
    /// <summary>
    /// Called when the local player left the room. We need to load the launcher scene.
    /// </summary>
    public void OnLeftRoom()
    {
        SceneManager.LoadScene(0);
    }

    public override void OnPhotonPlayerConnected(PhotonPlayer other)
    {
        Debug.Log("OnPhotonPlayerConnected() " + other.name); // not seen if you're the player connecting


        if (PhotonNetwork.isMasterClient)
        {
            Debug.Log("OnPhotonPlayerConnected isMasterClient " + PhotonNetwork.isMasterClient); // called before OnPhotonPlayerDisconnected


            LoadArena();
        }
    }


    public override void OnPhotonPlayerDisconnected(PhotonPlayer other)
    {
        Debug.Log("OnPhotonPlayerDisconnected() " + other.name); // seen when other disconnects


        if (PhotonNetwork.isMasterClient)
        {
            Debug.Log("OnPhotonPlayerConnected isMasterClient " + PhotonNetwork.isMasterClient); // called before OnPhotonPlayerDisconnected


            LoadArena();
        }
    }

    #endregion


    #region Public Methods


    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }


    #endregion

    #region Private Methods

    void LoadArena()
    {
        if ( !PhotonNetwork.isMasterClient )
        {
            Debug.LogError("PhotonNetwork : Trying to Load a level but we are not the master Client");

                Debug.Log("PhotonNetwork : Loading Level : " + PhotonNetwork.room.playerCount);
                loadedLobby = true;
                PhotonNetwork.LoadLevel("BattleGround");
        }
    }


    #endregion
}
