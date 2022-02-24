using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class LightController : MonoBehaviour
{
    private PlayerController playerController;

    private Camera mainCamera;

    private Transform spotLightParent;
    private Transform spotLightT;
    
    private bool lightActive;
    private bool lightFixed;

    private int doTweenId;
    
    private Vector3 cameraPositionOffset;
    
    [SerializeField] private Light spotLight;
    [SerializeField] private Transform cameraT;

    public void Initialize(PlayerController pc)
    {
        playerController = pc;
        
        mainCamera = Camera.main;
        
        spotLightT = spotLight.transform;
        spotLightParent = spotLightT.parent;
        
        spotLight.enabled = false; 

        cameraT.parent = null;
        cameraPositionOffset = cameraT.position - mainCamera.transform.position;
    }

    public void ManagedUpdate()
    {
        if (Input.GetMouseButtonDown(1))
        {
            EnableLight();
        }

        if (lightActive)
        {
            if (Input.GetMouseButtonUp(1))
            {
                DisableLight();
            }
        
            if (Input.GetMouseButtonDown(0))
            {
                FixLight();
            }
        }

        cameraT.position = mainCamera.transform.position + cameraPositionOffset;
        cameraT.rotation = mainCamera.transform.rotation;
    }

    private void EnableLight()
    {
        UnfixLight();
        lightActive = true;

        spotLight.enabled = true;

        spotLight.innerSpotAngle = 0f;
        spotLight.spotAngle = 0f;

       DOTween.Kill(doTweenId);
        doTweenId = DOTween.To(() => spotLight.spotAngle, x =>
        {
            spotLight.innerSpotAngle = x;
            spotLight.spotAngle = x;
        }, 30f, 0.25f).SetEase(Ease.OutBack).intId;
    }

    private void DisableLight()
    {
        lightActive = false;

        DOTween.Kill(doTweenId);
        doTweenId = DOTween.To(() => spotLight.spotAngle, x =>
            {
                spotLight.innerSpotAngle = x;
                spotLight.spotAngle = x;
            }, 0f, 0.14f).SetEase(Ease.OutSine)
            .OnComplete(() => { spotLight.enabled = false; }).intId;
    }

    private void UnfixLight()
    {
        lightFixed = false;

        spotLightT.SetParent(spotLightParent);
        spotLightT.localPosition = Vector3.zero;
        spotLightT.localEulerAngles = Vector3.zero;
    }

    private void FixLight()
    {
        lightActive = false;
        lightFixed = true;

        spotLightT.parent = null;
    }
}