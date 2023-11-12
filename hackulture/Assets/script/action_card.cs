using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class action_card
{
    public int action = 0;
    public action_card(int action)
    {
        this.action = action;
    }
    public void act()
    {
        if (this.action == 1){
            Debug.Log("act1");
        }
        if (this.action == 2)
        {
            Debug.Log("act2");
        }
        if (this.action == 3)
        {
            Debug.Log("act3");
        }
    }
}
   

