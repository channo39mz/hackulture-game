using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class endGame : MonoBehaviour
{
    public GameObject endcanvas;
    private void OnTriggerEnter(Collider other)
    {
        Destroy(other.gameObject);
        endcanvas.SetActive(true);
    }
}
