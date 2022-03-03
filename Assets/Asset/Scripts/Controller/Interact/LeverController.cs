using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class LeverController : MonoBehaviour, IInteractable
{
    public bool Activated = false;
    [SerializeField] float SpinAngle = 110;
    [SerializeField] private LeverLcokController Ctrl;
    
    private bool Workon = false;
    private float originX;
    void Start()
    {
        originX = transform.eulerAngles.x;
    }

    public void Interact()
    {
        if (!Workon) StartCoroutine(LeverMove(Activated));
    }

    IEnumerator LeverMove(bool a)
    {
        Workon = true;
        
        LeverMoving(a);
        
        yield return new WaitForSeconds(0.2f);

        Activated = !Activated;
        
        Ctrl.Check();
        
        Workon = false;
    }

    private void LeverMoving(bool a)
    {
        if (a)
            transform.DOLocalRotate(new Vector3(originX, 0, 0), 0.2f);
        else
            transform.DOLocalRotate(new Vector3(originX - SpinAngle, 0, 0), 0.2f);
    }
}