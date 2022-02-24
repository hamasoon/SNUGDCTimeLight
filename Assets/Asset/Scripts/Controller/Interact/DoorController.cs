using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour, IInteractable
{
    private Animator doorAnim;
    [SerializeField] bool isLock = false;
    private bool isWorking = false;
    [SerializeField] string KeyName = "SampleKey";
    public bool open = false;
    public float seconds = 3.0f;

    void Awake()
    {
        doorAnim = gameObject.GetComponentInChildren<Animator>();
    }

    public void Interact()
    {
        if(!isLock)
        {
            if(!isWorking)
                PlayAnimation();
        }
        else
        {
            if(GameManager.Instance.UseItem(KeyName))
                disableLock();
        }
    }

    public void PlayAnimation()
    {
        if(!open)
        {
            doorAnim.Play("DoorOpen", 0, 0.0f);
        }
        else
        {
            doorAnim.Play("DoorClose", 0, 0.0f);
        }
        open = !open;
    }

    public void disableLock(){ isLock = !isLock; }

    void OnTriggerEnter(Collider col)//플레이어가 문을 지나가면 자동으로 닫히게
    {   
        if(col.gameObject.CompareTag("Player"))
        {
            StartCoroutine(DoorCorourtine());
        }
    }

    IEnumerator DoorCorourtine()
    {
        isWorking = !isWorking;
        yield return new WaitForSeconds(seconds);

        PlayAnimation();
        isWorking = !isWorking;
    }
}
