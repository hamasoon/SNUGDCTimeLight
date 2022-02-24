using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;

public class KeyCardController : MonoBehaviour, IInteractable
{
    [SerializeField] private List<LeverController> Levers;
    [SerializeField] private DoorController doorController;
    [SerializeField] private string KeyCardName;

    public void Interact()
    {
        if (Check())
            if(GameManager.Instance.UseItem(KeyCardName))
                doorController.disableLock();
    }

    private bool Check()
    {
        foreach (var lever in Levers)
        {
            if (!lever.Activated) return false;
        }

        return true;
    }
}
