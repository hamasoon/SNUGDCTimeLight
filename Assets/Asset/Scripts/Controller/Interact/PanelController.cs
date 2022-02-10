using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

public class PanelController : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject Panel;
    [SerializeField] private Camera MainCamera;
    [SerializeField] private Camera PuzzleCamera;
    [SerializeField] private bool useMousePuzzle = false;
    
    void Start()
    {
        Panel.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Panel.activeSelf)
            {
                Panel.SetActive(false);
                GameManager.PlayerController.canMove = true;
                GameManager.PlayerController.canCameraMove = true;
                MainCamera.gameObject.SetActive(true);
                PuzzleCamera.gameObject.SetActive(false);
                if (useMousePuzzle)
                {
                    Cursor.lockState = CursorLockMode.Locked;
                    Cursor.visible = false;
                }
            }
        }
    }

    public void Interact()
    {
        Debug.Log(!Panel.activeSelf);
        if (!Panel.activeSelf)
        {
            GameManager.PlayerController.canMove = false;
            GameManager.PlayerController.canCameraMove = false;
            Panel.SetActive(true);
            MainCamera.gameObject.SetActive(false);
            PuzzleCamera.gameObject.SetActive(true);
            if (useMousePuzzle)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
        }
    }
}
