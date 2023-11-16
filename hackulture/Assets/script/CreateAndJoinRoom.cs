using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class CreateAndJoinRoom : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    public InputField createField;
    public InputField joinField;
    public void CreateRoom()
    {
        PhotonNetwork.CreateRoom(createField.text);
    }
    public void JoinRoom()
    {
        PhotonNetwork.JoinRoom(joinField.text);
    }

    // Update is called once per frame
    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("mainscine");
    }
}
