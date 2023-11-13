using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotatecam270 : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        my_maincharactor mainCharacter = other.gameObject.GetComponent<my_maincharactor>();
        if (mainCharacter != null)
        {
            mainCharacter.cam.transform.Rotate(0.0f, -90f, 0.0f);
        }
    }
}
