using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FocusController : MonoBehaviour, IInteractable
{
    [SerializeField] private bool useMousePuzzle = false;
    [SerializeField] private Vector3 loc;
    [SerializeField] private Vector3 rotateAngle;
    [SerializeField, Range(0f, 3.0f)] private float transTime = 1f;
    
    private Camera MainCamera;
    private Vector3 originPos;
    private Vector3 originAngle;
    private float originHSpeed;
    private float originVSpeed;
    public bool isFocused = false;
    
    void Start()
    {
        MainCamera = Camera.main;
    }

    void Update()
    {
        if(isFocused)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                StartCoroutine(ClosedOut());
            }
        }
    }

    public void Interact()
    {
        StartCoroutine(ClosedIn());
    }

    IEnumerator ClosedIn()
    {
        originPos = MainCamera.transform.position;
        originAngle = MainCamera.transform.eulerAngles;

        GameManager.PlayerController.canMove = false;
        GameManager.PlayerController.canCameraMove = false;

        originHSpeed = GameManager.PlayerController.hSpeed;
        originVSpeed = GameManager.PlayerController.vSpeed;
        GameManager.PlayerController.hSpeed = 1f;
        GameManager.PlayerController.vSpeed = 1f;

        Vector3 target = transform.position + loc;

        MainCamera.transform.parent.transform.DOMove(target, transTime);
        MainCamera.transform.parent.transform.DORotate(rotateAngle, transTime);

        yield return new WaitForSeconds(transTime+0.5f);

        if (useMousePuzzle)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        GameManager.PlayerController.canCameraMove = true;
        isFocused = true;

        gameObject.GetComponent<Collider>().enabled = false;
    }

    IEnumerator ClosedOut()
    {
        isFocused = false;
        GameManager.PlayerController.canCameraMove = false;
        if (useMousePuzzle)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        MainCamera.transform.parent.transform.DOMove(originPos, transTime);
        MainCamera.transform.parent.transform.DORotate(originAngle, transTime);
        
        yield return new WaitForSeconds(transTime+0.5f);

        GameManager.PlayerController.hSpeed = originHSpeed;
        GameManager.PlayerController.vSpeed = originVSpeed;

        GameManager.PlayerController.canMove = true;
        GameManager.PlayerController.canCameraMove = true;
        gameObject.GetComponent<Collider>().enabled = true;
    }
}
