using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;

public class ConnectToServer : MonoBehaviourPunCallbacks
{
    void Start()
    {
        print("Connecting to server.");
        if (MasterManager.GameSettings != null)
        {
            PhotonNetwork.NickName = MasterManager.GameSettings.NickName;
            PhotonNetwork.GameVersion = MasterManager.GameSettings.GameVersion;
            PhotonNetwork.ConnectUsingSettings();
        }
        else
        {
            Debug.LogError("GameSettings is null. Make sure it's properly initialized.");
        }
    }
    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
        Debug.Log("Connected to server");
        print("Connected to server");
        print(PhotonNetwork.LocalPlayer.NickName);

    }

    

    public override void OnJoinedLobby()
    {
        //string userId = PhotonNetwork.LocalPlayer.UserId; //Unique Id create by photon when player connect to server
        //Debug.Log(userId);
        SceneManager.LoadScene("Lobby");
    }

}
