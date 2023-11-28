using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon.StructWrapping;
using System.Collections.Generic;
using Unity.VisualScripting;
using Photon.Pun.UtilityScripts;


    [System.Serializable]
    public class EachPlayer
    {
        public string NickName;
        public int Score;
        public string Place;
        public int Steps;
    }
public class GameManager : MonoBehaviourPunCallbacks
{
    public Dictionary<int, EachPlayer> allPlayers = new Dictionary<int, EachPlayer>();
    public static GameManager Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

    }
    private void Update()
    {
        
        foreach(var kvp in allPlayers)
        {
            Debug.Log($"Player: {kvp.Key}, NickName: {kvp.Value.NickName}, Score: {kvp.Value.Score}, Place: {kvp.Value.Place}, Steps: {kvp.Value.Steps}");
        }
    }
    public override void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer)
    {
        allPlayers[newPlayer.ActorNumber] = new EachPlayer
        {
            NickName = newPlayer.NickName,
            Score = 0,

        };
    }

    public override void OnPlayerLeftRoom(Photon.Realtime.Player otherPlayer)
    {
        allPlayers.Remove(otherPlayer.ActorNumber);
    }
    [PunRPC]
    public void UpdatePlayerInfo(int actorNumber,string nickName,int score,string place,int steps)
    {
        if (allPlayers.ContainsKey(actorNumber))
        {
            EachPlayer playerInfo = allPlayers[actorNumber];
            playerInfo.NickName = nickName;
            playerInfo.Score = score;
            playerInfo.Place = place;
            playerInfo.Steps = steps;
        }
    }
}
