using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverController : MonoBehaviour, IInteractable
{
    public bool Activated = false;

    [SerializeField] GameObject LeverHandler;
    [SerializeField] float SpinAngle = 50;
    
    private bool Workon = false;

    public void Interact()
    {
        if (!Workon) StartCoroutine(LeverMove(Activated));
    }

    IEnumerator LeverMove(bool a)
    {
        Workon = true;
        if (a) 
            LeanTween.rotateY(LeverHandler, SpinAngle, 0.5f);
        else
            LeanTween.rotateY(LeverHandler, -SpinAngle, 0.5f);

        yield return new WaitForSeconds(0.5f);

        Activated = !Activated;
        Workon = false;
    }
}