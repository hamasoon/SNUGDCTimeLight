using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour, IInteractable
{
    [SerializeField] GameObject AnotherDoor;
    [SerializeField] bool isLock = false;
    [SerializeField] string KeyName = "SampleKey";
    private bool isWorking = false;
    public bool open = false;
    public float seconds = 0.7f;
    private float origin;

    void Awake()
    {
        origin = transform.eulerAngles.y;
        Debug.Log(origin);
    }

    public void Interact()
    {
        if(!isLock)
        {
            if(!isWorking)
                StartCoroutine(DoorCorourtine());
        }
        else
        {
            if(GameManager.Instance.UseItem(KeyName))
                disableLock();
            else
                GameManager.SubtitleManager.showSubtitle("Lock");
        }
    }

    private void PlayAnimation()
    {
        if(!open)
        {
            LeanTween.rotateY(gameObject, origin - 90, seconds).setEase(LeanTweenType.easeInSine);
            LeanTween.rotateY(AnotherDoor, origin - 90, seconds).setEase(LeanTweenType.easeInSine);
        }
        else
        {
            LeanTween.rotateY(gameObject, origin, seconds).setEase(LeanTweenType.easeInSine);
            LeanTween.rotateY(AnotherDoor, origin, seconds).setEase(LeanTweenType.easeInSine);
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
        PlayAnimation();
        yield return new WaitForSeconds(seconds);
        isWorking = !isWorking;
    }
}
