using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class provinceitem : MonoBehaviour
{
    private int id = 0;
    public string description = "";
    public int scor = -1;
    

    public provinceitem(int id)
    {
        this.id = id;
        Debug.Log($"Creating provinceitem with id: {id}");
        if (id == 1)
        {
            scor = 2;
            description = "item1";
        }
        else if (id == 2)
        {
            scor = 3;
            description = "item2";
        }
        else if (id == 4)
        {
            scor = 4;
            description = "item4";
        }
        else if (id == 5)
        {
            scor = 3;
            description = "item5";
        }
        else if (id == 7)
        {
            scor = 1;
            description = "item7";
        }
        else if (id == 8)
        {
            scor = 3;
            description = "item8";
        }
        else if (id == 10)
        {
            scor = 2;
            description = "item10";
        }
        else if (id == 11)
        {
            scor = 2;
            description = "item11";
        }
        else if (id == 13)
        {
            scor = 3;
            description = "item13";
        }
        else if (id == 14)
        {
            scor = 4;
            description = "item14";
        }
        else if (id == 16)
        {
            scor = 4;
            description = "item15";
        }
        else if (id == 17)
        {
            scor = 3;
            description = "item17";
        }
        else if (id == 19)
        {
            scor = 2;
            description = "item19";
        }
        else if (id == 20)
        {
            scor = 3;
            description = "item20";
        }
        else if (id == 22)
        {
            scor = 1;
            description = "item22";
        }
        else if (id == 23)
        {
            scor = 2;
            description = "item23";
        }
        else if (id == 25)
        {
            scor = 3;
            description = "item25";
        }
        else if (id == 26)
        {
            scor = 3;
            description = "item26";
        }
        else if (id == 28)
        {
            scor = 4;
            description = "item28";
        }
        else if (id == 29)
        {
            scor = 4;
            description = "item29";
        }
        else if (id == 31)
        {
            scor = 3;
            description = "item31";
        }
        else if (id == 32)
        {
            scor = 2;
            description = "item32";
        }
        else if (id == 34)
        {
            scor = 4;
            description = "item34";
        }
        else if (id == 35)
        {
            scor = 5;
            description = "item35";
        }

    }
    public int getscor()
    {
        return scor;
    }
    public int getid()
    {
        return id;
    }
}
