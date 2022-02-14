using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FocusController : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject aim;
    [SerializeField] private bool useMousePuzzle = false;
    [SerializeField] private Vector3 loc;
    [SerializeField] private Vector3 rotateAngle;
    [SerializeField, Range(0f, 3.0f)] private float transTime = 1f;
    private Camera MainCamera;
    private Vector3 originPos;
    private Vector3 originAngle;
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
        aim.SetActive(false);
        originPos = MainCamera.transform.position;
        originAngle = MainCamera.transform.eulerAngles;
        GameManager.PlayerController.canMove = false;
        GameManager.PlayerController.canCameraMove = false;

        Vector3 target = transform.position + loc;

        MainCamera.transform.parent.transform.DOMove(target, transTime);
        //Debug.Log(Quaternion.ToEulerAngles(Quaternion.LookRotation(new Vector3(originPos-transform.position))));
        MainCamera.transform.parent.transform.DORotate(rotateAngle, transTime);

        yield return new WaitForSeconds(transTime+0.5f);

        if (useMousePuzzle)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        
        isFocused = true;
    }

    IEnumerator ClosedOut()
    {

        if (useMousePuzzle)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        MainCamera.transform.parent.transform.DOMove(originPos, transTime);
        yield return new WaitForSeconds(transTime+0.5f);
        MainCamera.transform.parent.transform.DORotate(originAngle, transTime);

        GameManager.PlayerController.canMove = true;
        GameManager.PlayerController.canCameraMove = true;
        aim.SetActive(true);

        isFocused = false;
    }
}
