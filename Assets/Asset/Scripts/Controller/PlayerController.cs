using System;
using System.Threading;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public bool canMove = true;
    [Range(0, 30)] public float mySpeed = 15f;
    [Range(0, 5)] public float vSpeed = 2f;
    [Range(0, 5)] public float hSpeed = 2f;

    [SerializeField] Camera myCamera;
    [SerializeField] GameObject cameraHolder;
    [SerializeField] bool enableBob = true;
    [SerializeField, Range(0, 10f)] float bobAmplitude = 5f;
    [SerializeField, Range(0, 30f)] float bobFrequency = 10f;

    private Vector3 startPos;
    private CharacterController controller;
    private float yaw = 0.0f;
    private float pitch = 0.0f;

    void Start()
    {
        if (Cursor.lockState != CursorLockMode.Locked)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        startPos = myCamera.transform.localPosition;
        controller = gameObject.GetComponent<CharacterController>();
    }

    void Update()
    {
        Vector3 input = cameraHolder.transform.right * Input.GetAxisRaw("Horizontal") + cameraHolder.transform.forward*Input.GetAxisRaw("Vertical");
        Vector3 moveDir = transform.TransformDirection(input.normalized);

        yaw += hSpeed * Input.GetAxis("Mouse X");
        pitch -= vSpeed * Input.GetAxis("Mouse Y");

        cameraHolder.transform.eulerAngles = new Vector3(pitch, yaw, 0f);

        if(canMove){
            if(input == Vector3.zero) enableBob = false;
            else enableBob = true;
            CheckMotion();
            ResetMotion();
            controller.Move(moveDir * mySpeed * Time.deltaTime);
            controller.Move(Physics.gravity);
        }
    }

    private Vector3 FootStepMotion()
    {
        Vector3 pos = Vector3.zero;
        pos.y += Mathf.Sin(Time.time * bobFrequency) * (bobAmplitude/1000);
        pos.x = 0;
        return pos;
    }

    private void PlayerMotion(Vector3 motion){
        myCamera.transform.localPosition += motion;
    }

    private void CheckMotion()
    {
        if(!controller.isGrounded) return;
        if(!enableBob) return;

        GameManager.SoundManager.PlaySE("footstep");

        PlayerMotion(FootStepMotion());
    }

    private void ResetMotion()
    {
        if((myCamera.transform.localPosition - startPos).magnitude < 0.01f) return;
        myCamera.transform.localPosition = Vector3.Lerp(myCamera.transform.localPosition, startPos, Time.deltaTime * 10);
    }
}
