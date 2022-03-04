using DG.Tweening;
using UnityEngine;

public class LightController : MonoBehaviour
{
    private PlayerController playerController;

    private Camera mainCamera;

    private Transform spotLightParent;
    private Transform spotLightT;

    private int doTweenId;
    
    private Vector3 cameraPositionOffset;

    public bool lightTaken;
    private bool lighTutorial = false;
    private bool lightOnTutorial = false;
    private bool lightFixTutorial = false;
    
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
        if (lightTaken)
        {
            if (!lighTutorial)
            {
                GameManager.SubtitleManager.destroyTutorial();
                lighTutorial = true;
            }

            if (!lightOnTutorial)
            {
                GameManager.SubtitleManager.setTutorial("Tutorial Light");
            }
            else if (!lightFixTutorial)
            {
                GameManager.SubtitleManager.setTutorial("Tutorial Light Fix");
            }
            
            if (Input.GetMouseButtonDown(1))
            {
                if (!lightOnTutorial)
                {
                    GameManager.SubtitleManager.destroyTutorial();
                    lightOnTutorial = true;
                }
                EnableLight();
            }

            if (playerController.lightActive)
            {
                if (Input.GetMouseButtonUp(1))
                {
                    DisableLight();
                }

                if (Input.GetMouseButtonDown(0))
                {
                    if (!lightFixTutorial)
                    {
                        GameManager.SubtitleManager.destroyTutorial();
                        lightFixTutorial = true;
                    }
                    FixLight();
                }
            }
        }

        cameraT.position = mainCamera.transform.position + cameraPositionOffset;
        cameraT.rotation = mainCamera.transform.rotation;

        playerController.lightOrigin = spotLightT.position;
        playerController.lightDirection = spotLightT.forward;

        playerController.cameraOrigin = cameraT.position;
        playerController.cameraDirection = cameraT.forward;
    }

    private void EnableLight()
    {
        UnfixLight();
        playerController.lightActive = true;

        spotLight.enabled = true;

        spotLight.innerSpotAngle = 0f;
        spotLight.spotAngle = 0f;
        
        GameManager.SoundManager.PlaySE("Light", "LightOn");

        DOTween.Kill(doTweenId);
        doTweenId = DOTween.To(() => spotLight.spotAngle, x =>
        {
            spotLight.innerSpotAngle = x;
            spotLight.spotAngle = x;
        }, 30f, 0.25f).SetEase(Ease.OutBack).intId;
    }

    private void DisableLight()
    {
        playerController.lightActive = false;

        GameManager.SoundManager.PlaySE("Light", "LightOff");
        
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
        playerController.lightFixed = false;

        spotLightT.SetParent(spotLightParent);
        spotLightT.localPosition = Vector3.zero;
        spotLightT.localEulerAngles = Vector3.zero;
    }

    private void FixLight()
    {
        playerController.lightActive = false;
        playerController.lightFixed = true;

        spotLightT.parent = null;
    }
}