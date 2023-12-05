using Cinemachine;
using Photon.Pun;
using System.Collections.Generic;
using UnityEngine;
//using static UnityEditor.PlayerSettings;

using ThreadingTimer = System.Threading.Timer;

[System.Serializable]
public class EachPlayer
{
    public string NickName;
    public int PhotonViewId;
    public int Score;
    public string Side;
    public int Floor;
    public int Steps;
    public int Pos;
    public int Rank;
    public string Character;
    public List<int> itemlist = new List<int>();
    public bool IsTurn = false;
}
public class GameManager : MonoBehaviourPunCallbacks
{

    [SerializeField] CinemachineVirtualCamera _virtualCamera;
    public Dictionary<int, EachPlayer> allPlayers = new Dictionary<int, EachPlayer>();
    public static GameManager Instance;
    public static int currentPlayer = 1;

    private static int time = 0;
    private ThreadingTimer timer;
    private bool timerExecuted = false;
    private bool gameStart = false;
    private static int timePerTurn = 45;
    //int maxPlayer = PhotonNetwork.CurrentRoom.MaxPlayers;
    [SerializeField] private List<Sprite> all_province_card_image = new List<Sprite>();
    private List<List<int>> ProvinceList = new List<List<int>>();
    int maxPlayer = 2;
    public void Start()
    {
        sortPosition(allPlayers);
        addprovinceDack();
        Debug.Log(ProvinceList);
    }
    private void Update()
    {
        
        foreach (var kvp in allPlayers)
        {
            if (kvp.Value.IsTurn)
            {
                int targetPhotonViewId = kvp.Value.PhotonViewId;
                GameManager.Instance.photonView.RPC("SwitchCameraFollowObj", RpcTarget.All, targetPhotonViewId);
            }
            //Debug.Log($"Player: {kvp.Key}, NickName: {kvp.Value.NickName}, Score: {kvp.Value.Score}, Side: {kvp.Value.Side},Floor: {kvp.Value.Floor}, Steps: {kvp.Value.Steps}, Rank: {kvp.Value.Rank}, isTurn: {kvp.Value.IsTurn}, playerCount: {PhotonNetwork.CurrentRoom.PlayerCount}");
            //Debug.Log($"Player: {kvp.Key}, NickName: {kvp.Value.NickName}");
            //Debug.Log();
        }
        foreach (var player in PhotonNetwork.PlayerList)
        {
            if (allPlayers.ContainsKey(player.ActorNumber) == false)
            {
                photonView.RPC("addEachPlayerToDict", RpcTarget.All, player.ActorNumber);
            }
            if(player.ActorNumber == currentPlayer && gameStart)
            {
                GameManager.Instance.photonView.RPC("setIsTurn", RpcTarget.All, currentPlayer, true);
            }
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
    public void addprovinceDack()
    {
        for (int i = 0; i < 12; i++)
        {
            List<int> sublist = new List<int>();
            ProvinceList.Add(sublist);
            for (int j = 0; j < 3; j++)
            {
                ProvinceList[i].Add(j);
            }
        }
    }
    private void PrintProvinceList()
    {
        for (int i = 0; i < ProvinceList.Count; i++)
        {
            for (int j = 0; j < ProvinceList[i].Count; j++)
            {
                Debug.Log($"ProvinceList[{i}][{j}] = {ProvinceList[i][j]}");
            }
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
    /*public void SwitchCameraToPlayer(int actorNumber)
    {
        if (allPlayers.ContainsKey(actorNumber))
        {
            EachPlayer playerInfo = allPlayers[actorNumber];
            GameObject playerGameObject = GetPlayerGameObjectByNickname(playerInfo.NickName);

            if (playerGameObject != null && _cameraController != null)
            {
                _cameraController.SwitchFollowTarget(playerGameObject);
            }
        }
    }*/

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
        time++;

    }
    [PunRPC]
    public void UpdatePlayerInfo(int actorNumber, int photonViewId, string nickName, int score, string side, int floor, int steps, int pos, string character)
    {
        if (allPlayers.ContainsKey(actorNumber))
        {
            EachPlayer playerInfo = allPlayers[actorNumber];
            playerInfo.NickName = nickName;
            playerInfo.PhotonViewId = photonViewId;
            playerInfo.Score = score;
            playerInfo.Side = side;
            playerInfo.Floor = floor;
            playerInfo.Steps = steps;
            playerInfo.Character = character;
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
    public void DestroyAllPlayers()
    {
        StartCoroutine(DestroyPlayersCoroutine());
    }

    private IEnumerator<object> DestroyPlayersCoroutine()
    {
        foreach (var player in PhotonNetwork.PlayerList)
        {
            photonView.RPC("DestroySelf", player);

            yield return new WaitForSeconds(0.1f);
        }
    }
    [PunRPC]
    private void DestroySelf()
    {
        string searchString2 = $"EndC";
        GameObject myObject = GameObject.Find(searchString2);
        Debug.Log(myObject);
        if (myObject != null) // ตรวจสอบว่าพบ GameObject หรือไม่
        {
            Debug.Log(myObject);
            Canvas canvasComponent = myObject.GetComponent<Canvas>();

            canvasComponent.enabled = true;
        }
        Debug.Log(myObject);

        //PhotonNetwork.Destroy(photonView.gameObject);
    }
    [PunRPC]
    public void SwitchCameraFollowObj(int targetPhotonViewID)
    {
        PhotonView targetPhotonView = PhotonView.Find(targetPhotonViewID);

        if (targetPhotonView != null)
        {
            GameObject playerGameObject = targetPhotonView.gameObject;

            if (playerGameObject != null && _virtualCamera != null)
            {
                // Set the Cinemachine camera's follow target
                _virtualCamera.Follow = playerGameObject.transform;
                _virtualCamera.LookAt = playerGameObject.transform;
                ClampCameraPosition();
            }
            else
            {
                Debug.LogError("Invalid playerGameObject or Cinemachine camera.");
            }
        }
        else
        {
            Debug.LogError("PhotonView with ID " + targetPhotonViewID + " not found.");
        }

    }
    private void ClampCameraPosition()
    {
        // Assuming your rectangle has a collider, you can use its bounds to define the limits
        Collider rectangleCollider = GameObject.Find("firstG").GetComponent<Collider>();
        if (rectangleCollider != null)
        {
            Bounds bounds = rectangleCollider.bounds;

            // Get the camera's position in world space
            Vector3 cameraPosition = _virtualCamera.transform.position;

            // Clamp the camera's x and z coordinates within the rectangle's bounds
            float clampedX = Mathf.Clamp(cameraPosition.x, bounds.min.x, bounds.max.x);
            float clampedZ = Mathf.Clamp(cameraPosition.z, bounds.min.z, bounds.max.z);

            // Update the camera's position with clamped values
            _virtualCamera.transform.position = new Vector3(clampedX, cameraPosition.y, clampedZ);
        }
        else
        {
            Debug.LogError("Rectangle collider not found.");
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
    public void givecard(int number, int actorNumber)
    {
        int randomRow = number;
        int randomColumn = Random.Range(0, ProvinceList[randomRow].Count);
        int removedValue = ProvinceList[randomRow][randomColumn];
        ProvinceList[randomRow].RemoveAt(randomColumn);
        foreach (var kvp in allPlayers)
        {
            Debug.Log($"Player: {kvp.Key}, NickName: {kvp.Value.NickName}");
        }
        if (allPlayers.ContainsKey(actorNumber))
        {
            EachPlayer playerInfo = allPlayers[actorNumber];
            Debug.Log("pass");
            
            playerInfo.itemlist.Add(removedValue);
        }
        else
        {
            Debug.Log($"Dont have player {actorNumber} in Dict");
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
    [PunRPC]
    public void addEachPlayerToDict(int actorNumber)
    {
        allPlayers[actorNumber] = new EachPlayer
        {
            Score = 0,
        };
    }

}
