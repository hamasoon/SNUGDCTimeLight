using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookController : MonoBehaviour, IInteractable
{
    [SerializeField] GameObject Book;

    void Start()
    {
        Book.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Book.activeSelf){
            if(Input.GetKeyDown(KeyCode.Escape))
            {
                Book.SetActive(false);
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                GameManager.PlayerController.canMove = true;
                GameManager.PlayerController.canCameraMove = true;
            }
        }
    }

    public void Interact(){
        Book.SetActive(true);
        GameManager.PlayerController.canMove = false;
        GameManager.PlayerController.canCameraMove = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
