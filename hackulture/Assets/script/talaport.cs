using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class talaport : MonoBehaviour
{
    public GameObject target;
    public GameObject show;
    public void OnTriggerEnter(Collider other)
    {
        other.gameObject.transform.position = target.transform.position;
        my_maincharactor mainCharacter = other.gameObject.GetComponent<my_maincharactor>();
        if (mainCharacter != null)
        {
            mainCharacter.targetpoin = target;
        }
        if(show != null)
        {
            show.SetActive(true);
        }
    }
}
