using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class activeObject : MonoBehaviour
{
    public GameObject a;
    void Start()
    {
        // Get the PhotonView component from the parent player GameObject
        PhotonView playerPhotonView = GetComponentInParent<PhotonView>();

        // Check if the playerPhotonView is not null and belongs to the local player
        if (playerPhotonView != null && playerPhotonView.IsMine)
        {
            Canvas canvas = GetComponent<Canvas>();
            if (canvas != null)
            {
                canvas.enabled = true;
            }
        }
        else
        {
            // Disable the canvas for remote players
            Canvas canvas = GetComponent<Canvas>();
            if (canvas != null)
            {
                canvas.enabled = false;
            }
        }
    }
    public void setfalse()
    {
        a.SetActive(false);
    }
    public void settrue()
    {
        a.SetActive(true);
    }

}
