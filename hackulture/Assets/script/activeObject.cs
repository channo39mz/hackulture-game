using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class activeObject : MonoBehaviour
{
    public GameObject a;
    public void setfalse()
    {
        a.SetActive(false);
    }
    public void settrue()
    {
        a.SetActive(true);
    }
}
