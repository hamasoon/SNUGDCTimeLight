using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpDownController : MonoBehaviour
{
    [SerializeField] private List<int> Password;
    [SerializeField] private List<UpDownButtonController> buttons;
    [SerializeField] private DoorController doorController;

    public void Check()
    {
        for (int i = 0; i < Password.Count; i++)
        {
            if (Password[i] != buttons[i].num) return;
        }
        
        doorController.disableLock();
    }
}
