using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumpadController : MonoBehaviour, IInteractable
{
    private KeyPadController keyPadController;
    private bool isWorking = false;
    public int Num;

    void Awake()
    {
        keyPadController = gameObject.GetComponentInParent<KeyPadController>();
    }
    public void Interact()
    {
        if (!isWorking)
        {
            keyPadController.addString(Num);
            StartCoroutine(cooltime());
        }
    }
    IEnumerator cooltime()
    {
        isWorking = true;
        LeanTween.moveLocalZ(gameObject, -0.5f, 0.05f);
        LeanTween.moveLocalZ(gameObject, -0.7f, 0.05f).setDelay(0.05f);
        yield return new WaitForSeconds(0.1f);
        isWorking = false;
    }
}
