using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum UpDown
{
    Up, Down
}

public class UpDownButtonController : MonoBehaviour, IInteractable
{
    [SerializeField] private Text number;
    private UpDownController upDownController;
    public UpDown ways;
    public int num;
    
    void Start()
    {
        upDownController = gameObject.GetComponentInParent<UpDownController>();
    }
    
    public void Interact()
    {
        if (num == 0) return;
        else if (num == 9) return;

        switch (ways)
        {
            case UpDown.Up:
                num++;
                break;
            case UpDown.Down:
                num--;
                break;
        }

        upDownController.Check();
        
        number.text = num.ToString();
    }
}
