using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractController : MonoBehaviour
{
    private Vector3 ScreenCenter;
    [SerializeField, Range(0f, 15f)] float interactRange = 5f;

    private LightController lightController;
    [SerializeField] private Transform spotlightT;
    [SerializeField] private Transform copyCameraT;
    [SerializeField] private LayerMask playerLayerMask;

    private void Awake()
    {
        lightController = GetComponent<LightController>();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);

            if (Interact(ray) || !lightController.LightFixed) return;
            
            if (!Physics.Raycast(ray, out RaycastHit hit, interactRange)) return;
            
            Vector3 hitPosition = hit.point;
            float distanceFromLight = Vector3.Distance(spotlightT.position, hitPosition);

            if (!(Vector3.Angle(spotlightT.forward, hitPosition - spotlightT.position) <= 30f)) return;
            if (Physics.Raycast(new Ray(spotlightT.position, hitPosition - spotlightT.position), out RaycastHit hita,
                distanceFromLight * 0.99f, ~playerLayerMask))
            {
                Debug.Log(hita.collider.gameObject.name);
                return;
            }
            
            ray = new Ray(copyCameraT.position, copyCameraT.forward);
            Interact(ray);
        }
    }

    private bool Interact(Ray ray)
    {
        bool rayHit = false;
        
        if (Physics.Raycast(ray, out var hit, interactRange, 1 << LayerMask.NameToLayer("Interactable")))//Interactable Layer를 가진 요소만 Raycast되도록
        {
            rayHit = true;
            
            IInteractable interactable = hit.collider.gameObject.GetComponent<IInteractable>();
            GameManager.SubtitleManager.showSubtitle();
            interactable.Interact();
        }

        return rayHit;
    }
}
