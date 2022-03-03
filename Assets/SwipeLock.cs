using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeLock : MonoBehaviour, IInteractable
{
    private bool unlockStarted = false;

    [SerializeField] private SwipeLockController swipeLockController;
    [SerializeField] private bool isLockPick = false;
    private Animator animator;

    public bool unlocked = false;
    public float unlockDuration;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void Interact()
    {
        if (unlockStarted) return;

        StartCoroutine(UnlockAfter(unlockDuration));
    }

    private IEnumerator UnlockAfter(float delay)
    {
        unlockStarted = true;

        if(isLockPick)
            GameManager.SoundManager.PlaySE("LockPick", GetComponent<AudioSource>());
        else
            GameManager.SoundManager.PlaySE("Lever", GetComponent<AudioSource>());
        
        animator.SetTrigger("Trigger");
        yield return new WaitForSeconds(delay);

        unlocked = true;
        swipeLockController.SwipeLockUnlocked();
    }
}
