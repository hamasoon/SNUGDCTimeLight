using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeLockController : MonoBehaviour
{
    [SerializeField] private List<SwipeLock> swipeLocks;
    [SerializeField] private DoorController door;

    public void SwipeLockUnlocked()
    {
        foreach (var swipeLock in swipeLocks)
        {
            if (!swipeLock.unlocked) return;
        }

        door.disableLock();
    }
}
