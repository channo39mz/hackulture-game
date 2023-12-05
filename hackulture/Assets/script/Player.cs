using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using Photon.Pun.UtilityScripts;

using System.Drawing;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine.SocialPlatforms.Impl;

public class Player : MonoBehaviourPun
{
    public int myScore = 0;
    private bool isRight = true;
    private int floor = 1;
    private int stepsCount = 0;
    private bool isClicked = true;
    private bool cardWait = false;
    private string searchString = "start_1_floor";
    private bool firstRotatePerFloor = true;
    private int playedCardNumber = 0;
    private bool _isTurned;
    /*private int currentStep = 0;
    private int previousStep = 0;*/
    //private bool stepChanged =true;
    private ArrayList My_dack = new ArrayList();
    private ArrayList My_hand = new ArrayList();
    private ArrayList My_item = new ArrayList();
    [SerializeField] private List<Image> imagehand = new List<Image>();
    [SerializeField] private List<Sprite> all_work_card = new List<Sprite>();
    [SerializeField] private List<Sprite> all_action_card = new List<Sprite>();
    [SerializeField] Image image1;
    [SerializeField] private Text TextLog;
    [SerializeField] GameObject LeftOrRight;
    [SerializeField] private GetPlayerProperties _StatKeeper;
    //[SerializeField] private GameManager _GameManager;
    GameManager gameManager;

    void Start()
    {
        work_card card1 = new work_card(1);
        work_card card2 = new work_card(2);
        work_card card3 = new work_card(2);
        work_card card4 = new work_card(2);
        work_card card5 = new work_card(3);
        work_card card6 = new work_card(4);
        work_card card7 = new work_card(4);
        work_card card8 = new work_card(5);
        //GameManager.Instance.allPlayers[PhotonNetwork.LocalPlayer.ActorNumber];
        action_card card9 = new action_card(1);
        action_card card10 = new action_card(2);
        action_card card11 = new action_card(3);
        My_dack.Add(card9);
        My_dack.Add(card10);
        My_dack.Add(card11);

        My_dack.Add(card1);
        My_dack.Add(card2);
        My_dack.Add(card3);
        My_dack.Add(card4);
        My_dack.Add(card5);
        My_dack.Add(card6);
        My_dack.Add(card7);
        My_dack.Add(card8);
        
        shiff();
        /*print(My_dack[0] + " mydack1");
        print(My_dack[1] + " mydack2");
        print(My_dack[2] + " mydack3");*/
        for (int i = 0; i < 3; i++)
        {
            draw();
        }
        /*Debug.Log(My_hand[0] + " myhand1");
        Debug.Log(My_hand[1] + " myhand2");
        Debug.Log(My_hand[2] + " myhand3");*/
        sethand();
        gameManager = FindObjectOfType<GameManager>();
        if(gameManager == null)
        {
            Debug.Log("GameManager script not found");
            return;
        }
        //GameObject gameManager = GameObject.Find("GameManager");
        GameObject thisGameObject = gameObject;
        string userID = PhotonNetwork.LocalPlayer.UserId; //Unique Id create by photon when player connect to server
        int actorID = PhotonNetwork.LocalPlayer.ActorNumber; //Unique Id create by photon when player connect to server
        //gameManager.addPlayers(actorID,thisGameObject);
        //StartCoroutine(walkPlayer(20));
        TextLog.text = $"{actorID}";
        //Debug.Log(userId);
        /*PhotonView photonView = GetComponent<PhotonView>();
        if (photonView != null && photonView.IsMine)
        {
            // Access the View ID
            int viewID = photonView.ViewID;

            // Use the viewID as needed
            Debug.Log($"Player's PhotonView ID: {viewID}");
        }
        else
        {
            Debug.LogError("PhotonView component not found on this GameObject, or it doesn't belong to the local player.");
        }*/
        //Create List when Player class was instantiate
        if (photonView.IsMine)
        {
            int userId = PhotonNetwork.LocalPlayer.ActorNumber; //Unique Id create by photon when player connect to server
            GameManager.Instance.allPlayers[userId] = new EachPlayer();
            string nickName = PhotonNetwork.LocalPlayer.NickName;
            GameManager.Instance.photonView.RPC("addEachPlayerToDict", RpcTarget.All, userId);
        }
    }
    void Update()
    {

        if (photonView.IsMine)
        {
            int actorNumber = photonView.Owner.ActorNumber;
            string nickName = photonView.Owner.NickName;
            int score = photonView.Owner.GetScore();
            Vector3 position = transform.position;
            string side = $"{(isRight ? "Right" : "Left")}";
            int floor = this.floor;
            int steps = stepsCount;
            int pos = floor*100 + steps;
            string character = $"{actorNumber}";
            int photonViewId = PhotonView.Get(gameObject).ViewID;
            //Debug.Log($"Player: {actorNumber}, NickName: {nickName}, Score: {score}, Position: {position}");
            GameManager.Instance.photonView.RPC("UpdatePlayerInfo",RpcTarget.All,actorNumber,photonViewId,nickName,score,side,floor,steps,pos,character);
            int userId = PhotonNetwork.LocalPlayer.ActorNumber;
            _isTurned = GameManager.Instance.allPlayers[userId].IsTurn;
            foreach (var player in PhotonNetwork.PlayerList)
            {
                if (GameManager.Instance.allPlayers.ContainsKey(player.ActorNumber) == false)
                {
                    photonView.RPC("addEachPlayerToDict", RpcTarget.All, player.ActorNumber);
                    GameManager.Instance.photonView.RPC("UpdatePlayerInfo", RpcTarget.All, actorNumber, photonViewId, nickName, score, side, floor, steps, pos, character);
                }
                
            }

           
            /*if( _isTurned )
            {
                int targetPhotonViewID = PhotonView.Get(gameObject).ViewID;
                Debug.Log(targetPhotonViewID);
                gameObject.tag = "SpotLight";
                GameManager.Instance.photonView.RPC("SwitchCameraToPlayer", RpcTarget.All, actorNumber, targetPhotonViewID);

            }
            else
            {
                gameObject.tag = "Player";
            }*/
        }

        endTurn();

    }
    public int GetScore()
    {
        return myScore;
    }
    public void SetScore(int newScore)
    {
        myScore = newScore;
    }

    public void endTurn()
    {
        if (photonView.IsMine)
        {
            if(playedCardNumber == 2)
            {
                //gameObject.tag = "Player";
                int actorNumber = photonView.Owner.ActorNumber;
                GameManager.Instance.photonView.RPC("setIsTurn", RpcTarget.All, actorNumber, false);
                Photon.Realtime.Player playerOwn = PhotonNetwork.LocalPlayer;
                if (playerOwn.GetNext() != null)
                {
                    GameManager.Instance.photonView.RPC("setIsTurn",RpcTarget.All,playerOwn.GetNext().ActorNumber,true);
                    GameManager.currentPlayer = playerOwn.ActorNumber;
                }
                else
                {
                    Debug.Log("Dont have next Player");
                }
                playedCardNumber = 0;
               
            }
        }
    }
    public void shiff()
    {
        for (int i = 0; i < My_dack.Count; i++)
        {
            int rannum = Random.Range(0, 11);
            var temp = My_dack[i];
            My_dack[i] = My_dack[rannum];
            My_dack[rannum] = temp;
        }
    }
    public void draw()
    {
        My_hand.Add(My_dack[0]);
        My_dack.Remove(My_dack[0]);

    }

    public void sethand()
    {
        for (int i = 0; i < 3; i++)
        {
            if (My_hand[i].GetType() == typeof(work_card))
            {
                int numOfWork = ((work_card)My_hand[i]).NumOfWork;
                if (numOfWork == 1)
                {
                    imagehand[i].sprite = all_work_card[0];
                }
                else if (numOfWork == 2)
                {
                    imagehand[i].sprite = all_work_card[1];
                }
                else if (numOfWork == 3)
                {
                    imagehand[i].sprite = all_work_card[2];
                }
                else if (numOfWork == 4)
                {
                    imagehand[i].sprite = all_work_card[3];
                }
                else if (numOfWork == 5)
                {
                    imagehand[i].sprite = all_work_card[4];
                }
            }
            else if (My_hand[i].GetType() == typeof(action_card))
            {
                int numOfaction = ((action_card)My_hand[i]).action;
                /*                Debug.Log(numOfaction);*/
                if (numOfaction == 1)
                {
                    imagehand[i].sprite = all_action_card[0];
                }
                else if (numOfaction == 2)
                {
                    imagehand[i].sprite = all_action_card[1];
                }
                else if (numOfaction == 3)
                {
                    imagehand[i].sprite = all_action_card[2];
                }
            }
        }
    }
    public void playCard(int cardClicked)
    {
        int userId = PhotonNetwork.LocalPlayer.ActorNumber; //Unique Id create by photon when player connect to server
        _isTurned = GameManager.Instance.allPlayers[userId].IsTurn;
        if (cardWait== false && _isTurned)
        {
            cardWait = true;
            cardFunction(cardClicked);
            Debug.Log($"isTurned: {_isTurned}");
        }
        Debug.Log($"isTurned: {_isTurned}");

    }

    public void cardFunction(int cardIdx)
    {   
        var temp = My_hand[cardIdx-1];
        My_hand.Remove(temp);
        My_dack.Add(temp);
        draw();
        sethand();
        if (temp.GetType() == typeof(work_card))
        {
            int numOfWork = ((work_card)temp).NumOfWork;
            //Debug.Log("walk " + numOfWork);
            StartCoroutine(walkPlayer(numOfWork));
            

        }
        else if (temp.GetType() == typeof(action_card))
        {
            ((action_card)temp).act();
            cardWait = false;
            //need to end action with cardWait = false;
            playedCardNumber++;

        }
        
    }
   

    public void selectLeft()
    {
        isRight = false;
        isClicked = false;

    }
    public void selectRight()
    {   
        isRight = true;
        isClicked = false;
    }
    public int walkSteps()
    {
        return stepsCount;
    }
    IEnumerator walkPlayer(int steps)
    {
        yield return StartCoroutine(waitForChooseRightLeft());
        if(!isRight && firstRotatePerFloor )
        {
            yield return StartCoroutine(RotateByTarget(null,270f));

        }
        searchString = $"{(isRight ? "Right" : "Left")}_{floor}_floor";
        Debug.Log(searchString);
        GameObject myObject = GameObject.Find(searchString);
        

        //Count searchString's child || all platform
        Transform myObjectCount = myObject.transform;
        int countChild = myObjectCount.childCount; //variable of child
        if (myObject != null)
        {
            for (int i = 0; i < steps; i++)
            {
                /*Debug.Log(countChild);
                Debug.Log(floor);*/
                if (stepsCount == countChild)
                {
                    GameObject lastObject = GameObject.Find($"Last_{floor}");
                    yield return StartCoroutine(MoveToTarget(lastObject));
                    stepsCount += 1;
                    continue;
                }
                else if (stepsCount > countChild) //move until next step dont have Object
                {
                    if (floor < 3)
                    {
                        stepsCount = 0; // Reset stepsCount prepare to next floor
                        floor++;
                        GameObject nextStart = GameObject.Find($"Start_{floor}");// Find start of next floor
                        yield return StartCoroutine(MoveToTarget(nextStart));
                        yield return StartCoroutine(RotateByTarget(nextStart, 0f));
                        //make LeftorRight button active
                        ScriptActiveBtn scriptActiveBtn = nextStart.GetComponent<ScriptActiveBtn>();
                        if (scriptActiveBtn != null)
                        {
                            scriptActiveBtn.SetBtnActive(LeftOrRight);
                            firstRotatePerFloor = true;
                        }

                        isClicked = true;
                        yield return StartCoroutine(waitForChooseRightLeft());
                        //Set new Floor
                        searchString = $"{(isRight ? "Right" : "Left")}_{floor}_floor";
                        myObject = GameObject.Find(searchString);
                        myObjectCount = myObject.transform;
                        countChild = myObjectCount.childCount;
                        continue;
                    }
                    else
                    {
                        GameManager.Instance.DestroyAllPlayers();
                    }
                }
                //Get next position by get child at stepsCount
                Transform myTransform = myObject.transform.GetChild(stepsCount);
                GameObject targetPoint = myTransform.gameObject;
                yield return StartCoroutine(MoveToTarget(targetPoint)); //Move Function
                if(targetPoint.tag == "Corner")
                {
                    yield return StartCoroutine(RotateByTarget(targetPoint, 0f));
                }
                /*cardWait = false;*/
                /*transform.position = myTransform.position;*/
                stepsCount += 1;

            }
            
            cardWait = false;
            if (cardWait == false)
            {
                playedCardNumber++;
            }
            float s = stepsCount;
            Debug.Log(Mathf.CeilToInt(s / 3));
            float z = Mathf.CeilToInt(s / 3);
            int e = (int)Mathf.Round(z);
            if (photonView.IsMine && cardWait == false)
            {
                int userId = PhotonNetwork.LocalPlayer.ActorNumber; //Unique Id create by photon when player connect to server

                GameManager.Instance.photonView.RPC("givecard", RpcTarget.All, e, userId);
            }
        }
        else
        {
            Debug.LogError("Object with name 'ObjectName' not found.");
        }
    }
    IEnumerator MoveToTarget(GameObject targetObject)
    {
        float totalTime = 0.3f; // Adjust this based on how long you want the movement to take
        float elapsedTime = 0f;

        Vector3 startPosition = transform.position;
        
        Vector3 targetPosition = targetObject.transform.position;
        

        while (elapsedTime < totalTime)
        {
            transform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / totalTime);
            elapsedTime += Time.deltaTime;
            yield return null; // Wait for the next frame
        }
        

        // Ensure the final position is exactly the target position
        transform.position = targetPosition;

        Debug.Log("Move completed");
    }

    IEnumerator RotateByTarget(GameObject targetObject, float specificDegree)
    {
        float totalTime = 0.3f; // Adjust this based on how long you want the rotation to take
        float elapsedTime = 0f;
        Quaternion targetRotation;



        Quaternion startRotation = transform.rotation;
        if(specificDegree == 0f) { 
            targetRotation = targetObject.transform.rotation;
            Debug.Log("rotateByTarget");
        }
        else
        {
            targetRotation = Quaternion.Euler(0f,specificDegree, 0f);
            if(floor == 2)
            {
                targetRotation = Quaternion.Euler(0f, 90f, 0f);
            }
            firstRotatePerFloor = false;
        }


        while (elapsedTime < totalTime)
        {
            transform.rotation = Quaternion.Lerp(startRotation, targetRotation, elapsedTime / totalTime);
            elapsedTime += Time.deltaTime;
            yield return null; // Wait for the next frame
        }

        // Ensure the final rotation is exactly the target rotation
        transform.rotation = targetRotation;

        Debug.Log("Rotation completed");
    }

    IEnumerator waitForChooseRightLeft()
    {
        while(isClicked)
        {
            yield return null;
        }
    }





}
