using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UpDown
{
    Up = 1, Down = -1
}

public class UpDownButtonController : MonoBehaviour, IInteractable
{
    private UpDownController upDownController;
    private bool isWorking = false;
    public UpDown ways;
    public int index;
    
    void Start()
    {
        upDownController = gameObject.GetComponentInParent<UpDownController>();
    }
    
    public void Interact()
    {
        if(!isWorking)
        {
            StartCoroutine(ClickButton());
        }
    }

    IEnumerator ClickButton()
    {
        isWorking = !isWorking;

        upDownController.UpDownButtonInput(index, (int)ways);
        LeanTween.moveLocalZ(gameObject, 0, 0.25f);
        LeanTween.moveLocalZ(gameObject, -1.5f, 0.25f).setDelay(0.25f);
        yield return new WaitForSeconds(0.5f);

        isWorking = !isWorking;
    }
}
