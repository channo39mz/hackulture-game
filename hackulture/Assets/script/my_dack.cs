using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class my_dack : MonoBehaviour
{
    public ArrayList My_dack = new ArrayList();
    void Start()
    {
        work_card card1 = new work_card(1);
        work_card card2 = new work_card(2);
        work_card card3 = new work_card(2);
        work_card card4 = new work_card(2);
        work_card card5 = new work_card(3);
        work_card card6 = new work_card(4);
        action_card card7 = new action_card(1);
        action_card card8 = new action_card(2);
        action_card card9 = new action_card(3);
        My_dack.Add(card1);
        My_dack.Add(card2);
        My_dack.Add(card3);
        My_dack.Add(card4);
        My_dack.Add(card5);
        My_dack.Add(card6);
        My_dack.Add(card7);
        My_dack.Add(card8);
        My_dack.Add(card9);
        shiff();
        //viewdeack();
        print(My_dack[0]);
        print(My_dack[1]);
        print(My_dack[2]);
    }
    public void shiff()
    {
        for(int i = 0; i < My_dack.Count; i++)
        {
            int rannum = Random.Range(0, 9);
            var temp = My_dack[i];
            My_dack[i] = My_dack[rannum];
            My_dack[rannum] = temp;
        }
    }

    public void viewdeack()
    {
        for(int i = 0; i < My_dack.Count; i++)
        {
            Debug.Log(My_dack[i].ToString());
        }
    }
    void Update()
    {
        
    }
}
