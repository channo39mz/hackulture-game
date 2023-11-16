using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptActiveBtn : MonoBehaviour
{
    public GameObject LeftOrRight;
    public void SetBtnActive()
    {
        
        if (LeftOrRight != null && !LeftOrRight.activeSelf)
        {
            // Activate the GameObject
            LeftOrRight.SetActive(true);

            Debug.Log("Found and activated the GameObject");
        }
        else
        {
            // The GameObject was either not found or already active
            Debug.LogWarning("GameObject not found or is already active");
        }
    }
}
