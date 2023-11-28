using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerListingsMenu : MonoBehaviourPunCallbacks
{
    [SerializeField] private Transform _content;
    [SerializeField] private PlayerListing _playerListing;
    private List<PlayerListing> _listings = new List<PlayerListing>();
    private Player playerComponent;

    private void Awake()
    {
        GetCurrentRoomPlayer();
    }

    private void GetCurrentRoomPlayer()
    {
        foreach (KeyValuePair<int, Photon.Realtime.Player> playerInfo  in PhotonNetwork.CurrentRoom.Players) 
        {
            addPlayerListToDisplay(playerInfo.Value);
        }

    }
    private void addPlayerListToDisplay(Photon.Realtime.Player player)
    {
        PlayerListing listing = Instantiate(_playerListing, _content);
        if (listing != null)
        {
            listing.SetPlayerInfo(player);
            _listings.Add(listing);
        }
        Debug.Log("Someone enter the room" + player.ActorNumber);

    }
    
    public override void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer)
    {
        addPlayerListToDisplay(newPlayer);
        

    }

    public override void OnPlayerLeftRoom(Photon.Realtime.Player otherPlayer)
    {
        int index = _listings.FindIndex(x => x.Player == otherPlayer);
        if(index != -1)
        {
            Destroy(_listings[index].gameObject);
            _listings.RemoveAt(index);
        }
        Debug.Log("Someone leave the room");

    }
    
}
