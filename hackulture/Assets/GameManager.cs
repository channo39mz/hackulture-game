using Photon.Pun;
using System.Collections.Generic;
using UnityEngine;
//using static UnityEditor.PlayerSettings;

using ThreadingTimer = System.Threading.Timer;

[System.Serializable]
public class EachPlayer
{
    public string NickName;
    public int Score;
    public string Side;
    public int Floor;
    public int Steps;
    public int Pos;
    public int Rank;
    public bool IsTurn = false;
}   
public class GameManager : MonoBehaviourPunCallbacks
{
    public Dictionary<int, EachPlayer> allPlayers = new Dictionary<int, EachPlayer>();
    public static GameManager Instance;
    public static int currentPlayer = 1;

    private static int time = 0;
    private ThreadingTimer timer;
    private bool timerExecuted = false;
    private bool gameStart = false;
    private static int timePerTurn = 45;
    //int maxPlayer = PhotonNetwork.CurrentRoom.MaxPlayers;
    int maxPlayer = 2;
    public void Start()
    {
        sortPosition(allPlayers);
    }
    private void Update()
    {

        foreach (var kvp in allPlayers)
        {
           //Debug.Log($"Player: {kvp.Key}, NickName: {kvp.Value.NickName}, Score: {kvp.Value.Score}, Side: {kvp.Value.Side},Floor: {kvp.Value.Floor}, Steps: {kvp.Value.Steps}, Rank: {kvp.Value.Rank}, isTurn: {kvp.Value.IsTurn}");
        }
        //sortPosition(allPlayers);
        if (gameStart && !timerExecuted)
        {
            timer = new ThreadingTimer(Count, null, 0, 1000);
            timerExecuted = true;
        }
        if (time == timePerTurn)
        {
            Photon.Realtime.Player _currentPlayer = PhotonNetwork.CurrentRoom.GetPlayer(currentPlayer, false);
            int nextActorNumber = _currentPlayer.GetNext().ActorNumber;
            GameManager.Instance.photonView.RPC("setIsTurn", RpcTarget.All, currentPlayer, false);
            GameManager.Instance.photonView.RPC("setIsTurn", RpcTarget.All, nextActorNumber, true);
            Debug.Log("currentPlayer" + currentPlayer);
            time = 0;
            Debug.Log("3Sec");
        }

    }
   
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

    public override void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer)
    {
        allPlayers[newPlayer.ActorNumber] = new EachPlayer
        {
            NickName = newPlayer.NickName,
            Score = 0,
        };

        //check that room is full?
        
        if (PhotonNetwork.CurrentRoom.PlayerCount == maxPlayer)
        {
            if (gameStart == false)
            {
                GameManager.Instance.photonView.RPC("setIsTurn", RpcTarget.All, 1, true);
            }
            gameStart = true;
            Debug.Log("Room is full!");
        }
        else
        {
            Debug.Log("Room is not full. Current players: " + PhotonNetwork.CurrentRoom.PlayerCount);
        }
    }

    public override void OnPlayerLeftRoom(Photon.Realtime.Player otherPlayer)
    {
        allPlayers.Remove(otherPlayer.ActorNumber);
    }

    private static void Count(object state)
    {
        if (time == timePerTurn)
        {
            time = 0;
        }
        Debug.Log(time);
        time++;
        
    }
    [PunRPC]
    public void UpdatePlayerInfo(int actorNumber, string nickName, int score, string side, int floor, int steps, int pos)
    {
        if (allPlayers.ContainsKey(actorNumber))
        {
            EachPlayer playerInfo = allPlayers[actorNumber];
            playerInfo.NickName = nickName;
            playerInfo.Score = score;
            playerInfo.Side = side;
            playerInfo.Floor = floor;
            playerInfo.Steps = steps;
            playerInfo.Pos = pos;
        }
    }

    //sort to find rank
    public void sortPosition(Dictionary<int, EachPlayer> allPlayers)
    {
        List<KeyValuePair<int, EachPlayer>> list = new List<KeyValuePair<int, EachPlayer>>(allPlayers); //convert DIctionary to List

        list.Sort((x, y) => y.Value.Pos.CompareTo(x.Value.Pos)); //Like lambda


        Dictionary<int, EachPlayer> sortedDictioanry = new Dictionary<int, EachPlayer>(list); //convert List back to Dictionary
        int index = 0;
        foreach (var kvp in sortedDictioanry)
        {
            Debug.Log($"{kvp.Key}: {kvp.Value.Pos}");

            GameManager.Instance.photonView.RPC("setRank", RpcTarget.All, kvp.Key, index + 1); //Update value of Rank in main Dictionary
            index++;
        }

    }

    [PunRPC]
    public void setRank(int actorNumber, int rank)
    {
        if (allPlayers.ContainsKey(actorNumber))
        {
            EachPlayer playerInfo = allPlayers[actorNumber];
            playerInfo.Rank = rank;

        }
    }
    [PunRPC]
    public void setIsTurn(int actorNumber, bool isTurn)
    {
        if (allPlayers.ContainsKey(actorNumber))
        {
            EachPlayer playerInfo = allPlayers[actorNumber];
            playerInfo.IsTurn = isTurn;
        }
        currentPlayer = actorNumber;
        time = 0;
        /*foreach (var kvp in allPlayers)
        {
            Debug.Log($"Player: {kvp.Key}, NickName: {kvp.Value.NickName}, Score: {kvp.Value.Score}, Side: {kvp.Value.Side},Floor: {kvp.Value.Floor}, Steps: {kvp.Value.Steps}, Rank: {kvp.Value.Rank}, isTurn: {kvp.Value.IsTurn}");
        }*/
    }
}
