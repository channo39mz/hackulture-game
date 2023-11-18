using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public int myScore = 0;
    private bool isRight = true;
    private int floor = 1;
    private int stepsCount = 0;
    private bool isClicked = true;
    private ArrayList My_dack = new ArrayList();
    private ArrayList My_hand = new ArrayList();
    [SerializeField] private List<Image> imagehand = new List<Image>();
    [SerializeField] private List<Sprite> all_work_card = new List<Sprite>();
    [SerializeField] private List<Sprite> all_action_card = new List<Sprite>();
    [SerializeField] Image image1;
    void Start()
    {
        Debug.Log("PASS");

        work_card card1 = new work_card(1);
        work_card card2 = new work_card(2);
        work_card card3 = new work_card(2);
        work_card card4 = new work_card(2);
        work_card card5 = new work_card(3);
        work_card card6 = new work_card(4);
        action_card card7 = new action_card(1);
        action_card card8 = new action_card(2);
        action_card card9 = new action_card(3);
        My_dack.Add(card1);
        My_dack.Add(card2);
        My_dack.Add(card3);
        My_dack.Add(card4);
        My_dack.Add(card5);
        My_dack.Add(card6);
        My_dack.Add(card7);
        My_dack.Add(card8);
        My_dack.Add(card9);
        shiff();
        print(My_dack[0] + " mydack1");
        print(My_dack[1] + " mydack2");
        print(My_dack[2] + " mydack3");
        for (int i = 0; i < 3; i++)
        {
            draw();
        }
        Debug.Log(My_hand[0] + " myhand1");
        Debug.Log(My_hand[1] + " myhand2");
        Debug.Log(My_hand[2] + " myhand3");
        sethand();
        
        //StartCoroutine(walkPlayer(20));

    }

    public void shiff()
    {
        for (int i = 0; i < My_dack.Count; i++)
        {
            int rannum = Random.Range(0, 9);
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
                Debug.Log(numOfaction);
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
    public void playcard1()
    {
        /*if (view.IsMine)
        {*/
        Debug.Log("play1");
        var temp = My_hand[0];
        My_hand.Remove(temp);
        My_dack.Add(temp);
        draw();
        sethand();
        if (temp.GetType() == typeof(work_card))
        {
            int numOfWork = ((work_card)temp).NumOfWork;
            Debug.Log("walk " + numOfWork);
            StartCoroutine(walkPlayer(numOfWork));
        }
        else if (temp.GetType() == typeof(action_card))
        {
            ((action_card)temp).act();
        }

        /*}*/
    }
    public void playcard2()
    {
        /*f (view.IsMine)
        {*/
        Debug.Log("play2");
        var temp = My_hand[1];
        My_hand.Remove(temp);
        My_dack.Add(temp);
        draw();
        sethand();
        if (temp.GetType() == typeof(work_card))
        {
            int numOfWork = ((work_card)temp).NumOfWork;
            Debug.Log("walk " + numOfWork);
            StartCoroutine(walkPlayer(numOfWork));
        }
        else if (temp.GetType() == typeof(action_card))
        {
            ((action_card)temp).act();
        }

        /*}*/
    }
    public void playcard3()
    {
        /*if (view.IsMine)
        {*/
        Debug.Log("play3");
        var temp = My_hand[2];
        My_hand.Remove(temp);
        My_dack.Add(temp);
        draw();
        sethand();
        if (temp.GetType() == typeof(work_card))
        {
            int numOfWork = ((work_card)temp).NumOfWork;
            Debug.Log("walk " + numOfWork);
            StartCoroutine(walkPlayer(numOfWork));
        }
        else if (temp.GetType() == typeof(action_card))
        {
            ((action_card)temp).act();
        }
        /*}*/
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

    IEnumerator walkPlayer(int steps)
    {
        yield return StartCoroutine(waitForChooseRightLeft());
        
        string searchString = $"{(isRight ? "Right" : "Left")}_{floor}_floor";
        Debug.Log(searchString);
        GameObject myObject = GameObject.Find(searchString);
        

        //Count searchString's child || all platform
        Transform myObjectCount = myObject.transform;
        int countChild = myObjectCount.childCount; //variable of child
        if (myObject != null)
        {
            for (int i = 0; i < steps; i++)
            {
                Debug.Log(countChild);
                Debug.Log(floor);
                if (stepsCount == countChild)
                {
                    GameObject lastObject = GameObject.Find($"Last_{floor}");
                    yield return StartCoroutine(MoveToTarget(lastObject));
                    stepsCount += 1;
                    continue;
                }
                else if (stepsCount > countChild) //move until next step dont have Object
                {
                    stepsCount = 0; // Reset stepsCount prepare to next floor
                    floor++;
                    GameObject nextStart = GameObject.Find($"Start_{floor}");// Find start of next floor
                    yield return StartCoroutine(MoveToTarget(nextStart));
                    yield return StartCoroutine(RotateByTarget(nextStart));
                    //make LeftorRight button active
                    ScriptActiveBtn scriptActiveBtn = nextStart.GetComponent<ScriptActiveBtn>();  
                    if (scriptActiveBtn != null)
                    {
                        scriptActiveBtn.SetBtnActive();
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
                //Get next position by get child at stepsCount
                Transform myTransform = myObject.transform.GetChild(stepsCount);
                GameObject targetPoint = myTransform.gameObject;
                yield return StartCoroutine(MoveToTarget(targetPoint)); //Move Function
                if(targetPoint.tag == "Corner")
                {
                    yield return StartCoroutine(RotateByTarget(targetPoint));
                }

                /*transform.position = myTransform.position;*/
                stepsCount += 1;

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

    IEnumerator RotateByTarget(GameObject targetObject)
    {
        float totalTime = 0.3f; // Adjust this based on how long you want the rotation to take
        float elapsedTime = 0f;


        
        Quaternion startRotation = transform.rotation;
        Quaternion targetRotation = targetObject.transform.rotation;
        

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
