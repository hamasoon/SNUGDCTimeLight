using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelController : MonoBehaviour, IInteractable
{
    [SerializeField] int num;
    private CylinderController cylinderController;

    void Start()
    {
        cylinderController = gameObject.GetComponentInParent<CylinderController>();
    }

    public void Interact()
    {
        cylinderController.SpinCylinder(gameObject, num);
        GameManager.SoundManager.PlaySE("PanelMove", GetComponent<AudioSource>());
    }
}
