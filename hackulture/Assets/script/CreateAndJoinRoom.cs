using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using Photon.Realtime;
using Photon.Pun.Demo.Cockpit;

public class CreateAndJoinRoom : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    [SerializeField] private int maxPlayers = 4;
    public InputField createField;
    public InputField joinField;
    GameManager gameManager = GameManager.Instance;
    public void CreateRoom()
    {
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = maxPlayers;
        PhotonNetwork.CreateRoom(createField.text,roomOptions,null);

    }
    public void JoinRoom()
    {
        PhotonNetwork.JoinRoom(joinField.text);
    }

    // Update is called once per frame
    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        int userId = PhotonNetwork.LocalPlayer.ActorNumber; //Unique Id create by photon when player connect to server
        Debug.Log("UserId in OnJoinedRoom "+ userId);
        PhotonNetwork.LoadLevel("mainscine");
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log("Disconnected from server for reason" + cause.ToString());
    }
}
