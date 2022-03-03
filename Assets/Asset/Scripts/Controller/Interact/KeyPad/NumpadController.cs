using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumpadController : MonoBehaviour, IInteractable
{
    private KeyPadController keyPadController;
    private bool isWorking = false;
    public int Num;

    void Start()
    {
        keyPadController = gameObject.GetComponentInParent<KeyPadController>();
    }
    public void Interact()
    {
        if (!isWorking && gameObject.GetComponentInParent<FocusController>().isFocused)
        {
            keyPadController.addString(Num);
            StartCoroutine(cooltime());
        }
    }
    IEnumerator cooltime()
    {
        isWorking = true;
        GameManager.SoundManager.PlaySE("ButtonClick", GetComponent<AudioSource>());
        LeanTween.moveLocalZ(gameObject, -0.01f, 0.05f);
        LeanTween.moveLocalZ(gameObject, -0.03f, 0.05f).setDelay(0.05f);
        yield return new WaitForSeconds(0.1f);
        isWorking = false;
    }
}
