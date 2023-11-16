using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int myScore = 0;
    private bool isRight = true;
    private int floor = 1;
    private int stepsCount = 0;
    private bool isClicked = true;
    void Start()
    {
        Debug.Log("PASS");
        
        StartCoroutine(walkPlayer(20));
        
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
