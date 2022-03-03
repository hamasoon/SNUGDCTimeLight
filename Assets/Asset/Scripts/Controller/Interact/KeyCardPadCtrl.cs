using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyCardPadCtrl : MonoBehaviour, IInteractable
{
    [SerializeField] private LeverLcokController Ctrl;
    [SerializeField] private DoorController Door;
    [SerializeField] private string keyname;

    public void Interact()
    {
        if (Ctrl.Locked)
        {
            if (GameManager.Instance.UseItem(keyname))
                Door.disableLock();
        }
    }
}
