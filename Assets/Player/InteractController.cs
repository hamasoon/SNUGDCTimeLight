using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractController : MonoBehaviour
{
    private Vector3 ScreenCenter;
    [SerializeField, Range(0f, 15f)] float interactRange = 5f;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E)){
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(new Vector3(Camera.main.pixelWidth/2, Camera.main.pixelHeight/2));//카메라 정중앙
            if(Physics.Raycast(ray, out hit, interactRange, 1 << LayerMask.NameToLayer("Interactable")))//Interactable Layer를 가진 요소만 Raycast되도록
            {
                GameManager.SubtitleManager.showSubtitle();
                IInteractable script = hit.collider.gameObject.GetComponent<IInteractable>();
                script.Interact();
            }
        }
    }
}
