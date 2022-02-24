using UnityEngine;

public class InteractController : MonoBehaviour
{
    private PlayerController playerController;

    private Camera mainCamera;
    private Vector3 screenCenter;
    [SerializeField, Range(0f, 15f)] float interactRange = 5f;
    
    [SerializeField] private LayerMask playerLayerMask;

    public void Initialize(PlayerController pc)
    {
        playerController = pc;

        mainCamera = Camera.main;
    }

    public void ManagedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Ray ray = new Ray(mainCamera.transform.position, mainCamera.transform.forward);

            if (Interact(ray) || !playerController.lightFixed) return;

            if (!Physics.Raycast(ray, out RaycastHit hit, interactRange)) return;

            Vector3 hitPosition = hit.point;

            Vector3 lightPosition = playerController.lightOrigin;
            Vector3 lightDirection = playerController.lightDirection;

            float distanceFromLight = Vector3.Distance(lightPosition, hitPosition);

            if (!(Vector3.Angle(lightDirection, hitPosition - lightPosition) <= 30f)) return;
            if (Physics.Raycast(new Ray(lightPosition, hitPosition - lightPosition), out RaycastHit hita,
                distanceFromLight * 0.99f, ~playerLayerMask))
            {
                Debug.Log(hita.collider.gameObject.name);
                return;
            }

            ray = new Ray(playerController.cameraOrigin, playerController.cameraDirection);
            Interact(ray);
        }
    }

    private bool Interact(Ray ray)
    {
        bool rayHit = false;

        if (Physics.Raycast(ray, out var hit, interactRange,
            1 << LayerMask.NameToLayer("Interactable"))) //Interactable Layer를 가진 요소만 Raycast되도록
        {
            rayHit = true;

            IInteractable interactable = hit.collider.gameObject.GetComponent<IInteractable>();
            GameManager.SubtitleManager.showSubtitle();
            interactable.Interact();
        }

        return rayHit;
    }
}