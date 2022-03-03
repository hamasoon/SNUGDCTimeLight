using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeLock : MonoBehaviour, IInteractable
{
    private bool unlockStarted = false;

    [SerializeField] private SwipeLockController swipeLockController;
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

        animator.SetTrigger("Trigger");
        yield return new WaitForSeconds(delay);

        unlocked = true;
        swipeLockController.SwipeLockUnlocked();
    }
}
