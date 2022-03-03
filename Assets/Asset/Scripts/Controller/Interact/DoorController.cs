using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PP
{
    Pull = -1, Push = 1
}

public class DoorController : MonoBehaviour, IInteractable
{
    [SerializeField] GameObject AnotherDoor;
    [SerializeField] bool isLock = false;
    [SerializeField] bool dualLocked = false;
    [SerializeField] string KeyName = "SampleKey";
    [SerializeField] private PP pp = PP.Push;
    private bool isWorking = false;
    public bool open = false;
    public float seconds = 0.7f;
    private float origin;

    public float rotationMultiplier = 1f;

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
            if (!dualLocked && GameManager.Instance.UseItem(KeyName))
                disableLock();
            else
                GameManager.SubtitleManager.showSubtitle("Locked");
        }
    }

    private void PlayAnimation()
    {
        if(!open)
        {
            LeanTween.rotateY(gameObject, origin - (int)pp * 120, seconds).setEase(LeanTweenType.easeInSine);

            if (AnotherDoor)
                LeanTween.rotateY(AnotherDoor, origin - (int)pp * 120, seconds).setEase(LeanTweenType.easeInSine);
            
            GameManager.SoundManager.PlaySE("DoorOpen", GetComponent<AudioSource>());
        }
        else
        {
            LeanTween.rotateY(gameObject, origin, seconds).setEase(LeanTweenType.easeInSine);

            if (AnotherDoor)
                LeanTween.rotateY(AnotherDoor, origin, seconds).setEase(LeanTweenType.easeInSine);
            
            GameManager.SoundManager.PlaySE("DoorClose", GetComponent<AudioSource>());
        }
        open = !open;
    }

    public void disableLock()
    {
        isLock = !isLock;
        GameManager.SoundManager.PlaySE("DoorUnlock", gameObject.GetComponent<AudioSource>());
    }

    void OnTriggerEnter(Collider col)//플레이어가 문을 지나가면 자동으로 닫히게
    {   
        if(col.gameObject.CompareTag("Player"))
        {
            StartCoroutine(DoorCorourtine());
        }
    }

    IEnumerator DoorCorourtine()
    {
        if (!dualLocked || (dualLocked && GameManager.Instance.UseItem(KeyName)))
        {
            isWorking = !isWorking;
            PlayAnimation();
            yield return new WaitForSeconds(seconds);
            isWorking = !isWorking;
        } else
        {
            GameManager.SubtitleManager.showSubtitle("Key Locked");
        }
    }
}
