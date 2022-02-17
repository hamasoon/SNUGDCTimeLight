using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class LightController : MonoBehaviour
{
    private bool lightActive = true;
    private bool lightFixed = false;

    private Transform spotLightParent;
    private Transform spotLightT;
    [SerializeField] private Light spotLight;

    private void Awake()
    {
        spotLightT = spotLight.transform;
        spotLightParent = spotLightT.parent;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            EnableLight();
        }
        if (Input.GetMouseButtonUp(1) && lightActive)
        {
            DisableLight();
        }

        if (Input.GetMouseButtonDown(0) && lightActive)
        {
            FixLight();
        }
    }

    private void EnableLight()
    {
        UnfixLight();
        lightActive = true;

        spotLight.enabled = true;
        
        spotLight.innerSpotAngle = 0f;
        spotLight.spotAngle = 0f;
        
        DOTween.To(() => spotLight.spotAngle, x =>
        {
            spotLight.innerSpotAngle = x;
            spotLight.spotAngle = x;
        }, 30f, 0.25f).SetEase(Ease.OutBack);
    }

    private void DisableLight()
    {
        lightActive = false;

        DOTween.To(() => spotLight.spotAngle, x =>
            {
                spotLight.innerSpotAngle = x;
                spotLight.spotAngle = x;
            }, 0f, 0.14f).SetEase(Ease.OutSine)
            .OnComplete(() => { spotLight.enabled = false; });
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
