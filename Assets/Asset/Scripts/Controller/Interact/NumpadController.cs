using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumpadController : MonoBehaviour, IInteractable
{
    private KeyPadController keyPadController;
    public int Num;

    void Awake()
    {
        keyPadController = gameObject.GetComponentInParent<KeyPadController>();
    }
    public void Interact()
    {
        keyPadController.addString(Num);
    }
}
