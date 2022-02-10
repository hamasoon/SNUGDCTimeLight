using System;
using System.Net.Mime;
using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyPadController : MonoBehaviour
{
    [SerializeField] TextMeshPro[] text = new TextMeshPro[4];
    [SerializeField] string password = "3812";//비밀번호
    [SerializeField] DoorController LookDoor;
    private int count = 0;
    private bool waiting = false; //DeadLock 방지

    public void addString(int num)
    {
        if(!waiting)
        {
            if(count < 3)
            {
                text[count].text = num.ToString(); 
                count++;
            }
            else
            {
                text[count].text = num.ToString(); 
                if(!isCorrect()) StartCoroutine(checkCoolTime());
                else
                {
                    waiting = true; //맞았을 경우 고정시키는 용도
                    LookDoor.disableLock();
                    LookDoor.PlayAnimation();
                }
                count = 0;
            }
        }
    }

    private bool isCorrect()
    {
        for(int i=0; i<4; i++)
        {
            string t = text[i].text;
            string p = password[i].ToString();
            if(!String.Equals(t,p)) return false;
        }
        return true;
    }

    IEnumerator checkCoolTime(){
        waiting = true;
        for(int i=0; i<3; i++){
            foreach(TextMeshPro t in text)  t.enabled = false;
            yield return new WaitForSeconds(0.1f);
            foreach(TextMeshPro t in text)  t.enabled = true;
            yield return new WaitForSeconds(0.1f);
        }
        foreach(TextMeshPro t in text) t.text = "0";
        waiting = false;
    }
}
