using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SpawnPosition : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject playerPrefab;

    

    private void Start()
    {
        Vector3 randomPosition = transform.position;
        PhotonNetwork.Instantiate(playerPrefab.name, randomPosition, Quaternion.identity);
    }

}
