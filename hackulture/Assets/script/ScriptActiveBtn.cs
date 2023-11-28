using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptActiveBtn : MonoBehaviour
{
    //public GameObject LeftOrRight;
  
    public void SetBtnActive(GameObject LeftOrRight)
    {
        PhotonView playerPhotonView = GetComponentInParent<PhotonView>();

        
            if (LeftOrRight != null && !LeftOrRight.activeSelf)
            {
                // Activate the GameObject
               
                LeftOrRight.SetActive(!LeftOrRight.activeSelf);
                        
                Debug.Log("Found and activated the GameObject");
            }
            else
            {
                // The GameObject was either not found or already active
                Debug.LogWarning("GameObject not found or is already active");
            }

            
            Debug.Log(LeftOrRight != null);
    }
}
