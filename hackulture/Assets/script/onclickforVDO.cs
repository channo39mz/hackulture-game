using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class onclickforVDO : MonoBehaviour
{
    public GameObject imageObject;  // กำหนดให้เป็น GameObject ของรูปภาพ
    public GameObject item;
    public GameObject event1;
    public TMP_Text t;
    private bool isImageVisible = false;  // ตรวจสอบว่ารูปภาพปรากฏหรือไม่
    private float timer = 0f;  // ตัวนับเวลา
    private float duration = 0.5f;  // ระยะเวลาที่ต้องการให้รูปภาพปรากฏ
    public int scor = 0;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            scor += 2;
            t.text = scor.ToString();
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            scor += 3;
            t.text = scor.ToString();
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            scor += 4;
            t.text = scor.ToString();
        }


        if (Input.GetKeyDown(KeyCode.K))
        {
            imageObject = item;
            // เปลี่ยนสถานะปรากฏหรือซ่อนรูปภาพ
            isImageVisible = !isImageVisible;

            // เริ่มต้นตัวนับเวลา
            timer = 0f;
        }

        if (Input.GetKeyDown(KeyCode.J))
        {
            imageObject = event1;
            // เปลี่ยนสถานะปรากฏหรือซ่อนรูปภาพ
            isImageVisible = !isImageVisible;

            // เริ่มต้นตัวนับเวลา
            timer = 0f;
        }

        // หากรูปภาพปรากฏ
        if (isImageVisible)
        {
            // ปรับเวลา
            timer += Time.deltaTime;

            // ปรับขนาดของรูปภาพ
            float scaleFactor = Mathf.Clamp01(timer / duration);  // ให้ scaleFactor เป็นค่าระหว่าง 0 ถึง 1
            imageObject.transform.localScale = new Vector3(1f * scaleFactor, 1f * scaleFactor, 2f);

           
        }
      
        if (!isImageVisible)
        {
            // ปรับเวลา
            timer += Time.deltaTime;

            // ปรับขนาดของรูปภาพ
            float scaleFactor = Mathf.Clamp01(timer / duration);  // ให้ scaleFactor เป็นค่าระหว่าง 0 ถึง 1
            imageObject.transform.localScale = new Vector3(0f * scaleFactor, 0f * scaleFactor, 2f);
        }
    }
}
