using UnityEngine;

public class BookController : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject book;

    private void Start()
    {
        book.SetActive(false);
    }
    
    private void Update()
    {
        if (book.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                book.SetActive(false);
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                GameManager.PlayerController.canMove = true;
                GameManager.PlayerController.canCameraMove = true;
            }
        }
    }

    public void Interact()
    {
        book.SetActive(true);
        GameManager.PlayerController.canMove = false;
        GameManager.PlayerController.canCameraMove = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}