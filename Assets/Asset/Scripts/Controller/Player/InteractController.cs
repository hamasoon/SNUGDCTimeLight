using UnityEngine;

public class InteractController : MonoBehaviour
{
    private PlayerController playerController;
    
    private Camera mainCamera;
    private Vector3 screenCenter;
    [SerializeField, Range(0f, 15f)] float interactRange = 5f;

    public void Initialize(PlayerController pc)
    {
        playerController = pc;

        mainCamera = Camera.main;
    }

    public void ManagedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Ray ray = mainCamera.ScreenPointToRay(new Vector3(mainCamera.pixelWidth / 2, mainCamera.pixelHeight / 2)); //카메라 정중앙

            if (!Physics.Raycast(ray, out var hit, interactRange,
                1 << LayerMask.NameToLayer("Interactable"))) return;

            GameManager.SubtitleManager.showSubtitle();
            IInteractable script = hit.collider.gameObject.GetComponent<IInteractable>();
            script.Interact();
        }
    }

    private void CheckInteractable()
    {
        
    }
}