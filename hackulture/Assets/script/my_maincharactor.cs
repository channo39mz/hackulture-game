using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class my_maincharactor : MonoBehaviour
{
    public ArrayList My_hand = new ArrayList();
    public ArrayList My_love_item = new ArrayList();
    public List<Image> imagehand = new List<Image>();
    public List<Sprite> all_work_card = new List<Sprite>();
    public List<Sprite> all_action_card = new List<Sprite>();
    public int my_scor = 0;
    public GameObject myBody;
    public GameObject targetpoin;
    public static ArrayList My_dack = new ArrayList();
    public List<GameObject> Leftway_Layer1 = new List<GameObject>();
    public List<GameObject> Rightway_Layer1 = new List<GameObject>();
    public List<GameObject> Leftway_Layer2 = new List<GameObject>();
    public List<GameObject> Rightway_Layer2 = new List<GameObject>();
    public List<GameObject> Leftway_Layer3 = new List<GameObject>();
    public List<GameObject> Rightway_Layer3 = new List<GameObject>();
    public List<GameObject> Myway = new List<GameObject>();
    public int myposition = 0;
    public GameObject cam;
    public int[] check = { 0, 0, 0 };

    // Start is called before the first frame update
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
        print(My_dack[0] + " mydack1");
        print(My_dack[1] + " mydack2");
        print(My_dack[2] + " mydack3");
        for (int i = 0; i < 3; i++)
        {
            draw();
        }
        Debug.Log(My_hand[0] + " myhand1");
        Debug.Log(My_hand[1] + " myhand2");
        Debug.Log(My_hand[2] + " myhand3");
        print(My_dack[0] + " mydack1");
        print(My_dack[1] + " mydack2");
        print(My_dack[2] + " mydack3");
        sethand();
    }

    public void draw()
    {
        My_hand.Add(My_dack[0]);
        My_dack.Remove(My_dack[0]);

    }

    public void select_L_way_layer1()
    {
        foreach(GameObject i in Leftway_Layer1)
        {
            Myway.Add(i);
        }
        cam.transform.Rotate(0.0f, 90.0f, 0.0f);
        check[0] = 1;
    }
    public void select_R_way_layer1()
    {
        foreach (GameObject i in Rightway_Layer1)
        {
            Myway.Add(i);
        }
        check[0] = 2;
    }
    public void select_L_way_layer2()
    {
        foreach (GameObject i in Leftway_Layer2)
        {
            Myway.Add(i);
        }
        if(check[0] == 1)
        {
            cam.transform.Rotate(0.0f, 0.0f, 0.0f);
        }
        else
        {
            cam.transform.Rotate(0.0f, -90.0f, 0.0f);
        }
        check[1] = 1;
    }
    public void select_R_way_layer2()
    {
        foreach (GameObject i in Rightway_Layer2)
        {
            Myway.Add(i);
        }
        if (check[0] == 1)
        {
            cam.transform.Rotate(0.0f, 90.0f, 0.0f);
        }
        else
        {
            cam.transform.Rotate(0.0f, 0.0f, 0.0f);
        }
        check[1] = 2;
    }
    public void select_L_way_layer3()
    {
        foreach (GameObject i in Leftway_Layer3)
        {
            Myway.Add(i);
        }
        if (check[1] == 1)
        {
            cam.transform.Rotate(0.0f, 0.0f, 0.0f);
        }
        else
        {
            cam.transform.Rotate(0.0f, 90.0f, 0.0f);
        }
    }
    public void select_R_way_layer3()
    {
        foreach (GameObject i in Rightway_Layer3)
        {
            Myway.Add(i);
        }
        if (check[1] == 1)
        {
            cam.transform.Rotate(0.0f, -90.0f, 0.0f);
        }
        else
        {
            cam.transform.Rotate(0.0f, 0.0f, 0.0f);
        }
    }

    public void sethand()
    {
        for (int i = 0; i < 3; i++)
        {
            if (My_hand[i].GetType() == typeof(work_card))
            {
                int numOfWork = ((work_card)My_hand[i]).NumOfWork;
                if (numOfWork == 1)
                {
                    imagehand[i].sprite = all_work_card[0];
                }
                else if (numOfWork == 2)
                {
                    imagehand[i].sprite = all_work_card[1];
                }
                else if (numOfWork == 3)
                {
                    imagehand[i].sprite = all_work_card[2];
                }
                else if (numOfWork == 4)
                {
                    imagehand[i].sprite = all_work_card[3];
                }
                else if (numOfWork == 5)
                {
                    imagehand[i].sprite = all_work_card[4];
                }
            }
            else if (My_hand[i].GetType() == typeof(action_card))
            {
                int numOfaction = ((action_card)My_hand[i]).action;
                Debug.Log(numOfaction);
                if(numOfaction == 1)
                {
                    imagehand[i].sprite = all_action_card[0];
                }
                else if (numOfaction == 2)
                {
                    imagehand[i].sprite = all_action_card[1];
                }
                else if (numOfaction == 3)
                {
                    imagehand[i].sprite = all_action_card[2];
                }
            }
        }
        
    }

    public void walk(int i)
    {
        myposition += i;

        if(myposition > Myway.Count-1)
        {
            myposition = Myway.Count-1;
        }
        targetpoin = Myway[myposition];
    }

    public void playcard1()
    {
        Debug.Log("play1");
        var temp = My_hand[0];
        My_hand.Remove(temp);
        My_dack.Add(temp);
        draw();
        sethand();
        if (temp.GetType() == typeof(work_card))
        {
            int numOfWork = ((work_card)temp).NumOfWork;
            Debug.Log("walk " + numOfWork);
            walk(numOfWork);
        }
        else if (temp.GetType() == typeof(action_card))
        { 
            ((action_card)temp).act();
        }
    }
    public void playcard2()
    {
        Debug.Log("play2");
        var temp = My_hand[1];
        My_hand.Remove(temp);
        My_dack.Add(temp);
        draw();
        sethand();
        if (temp.GetType() == typeof(work_card))
        {
            int numOfWork = ((work_card)temp).NumOfWork;
            Debug.Log("walk " + numOfWork);
            walk(numOfWork);
        }
        else if (temp.GetType() == typeof(action_card))
        {
            ((action_card)temp).act();
        }
    }
    public void playcard3()
    {
        Debug.Log("play3");
        var temp = My_hand[2];
        My_hand.Remove(temp);
        My_dack.Add(temp);
        draw();
        sethand();
        if (temp.GetType() == typeof(work_card))
        {
            int numOfWork = ((work_card)temp).NumOfWork;
            Debug.Log("walk " + numOfWork);
            walk(numOfWork);
        }
        else if (temp.GetType() == typeof(action_card))
        {
            ((action_card)temp).act();
        }
    }

    public void moveAtoB(GameObject targetpion)
    {
        myBody.transform.position = Vector3.MoveTowards(myBody.transform.position, 
            targetpion.transform.position, Time.deltaTime );
        Debug.Log("move");
    }
    public void shiff()
    {
        for (int i = 0; i < My_dack.Count; i++)
        {
            int rannum = Random.Range(0, 9);
            var temp = My_dack[i];
            My_dack[i] = My_dack[rannum];
            My_dack[rannum] = temp;
        }
    }

    public void viewdeack()
    {
        for (int i = 0; i < My_dack.Count; i++)
        {
            Debug.Log(My_dack[i].ToString());
        }
    }
    // Update is called once per frame
    void Update()
    {
        if(myBody.transform.position != targetpoin.transform.position)
        {
            moveAtoB(targetpoin);
        }
    }
}
