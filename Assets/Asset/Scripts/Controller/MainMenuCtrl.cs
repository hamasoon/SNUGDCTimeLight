using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenuCtrl : MonoBehaviour
{
    [SerializeField] private GameObject SpotLight;
    [SerializeField] private SpriteRenderer TitleImage;
    [SerializeField] private SpriteRenderer FlashLight;
    [SerializeField] private TextMeshProUGUI Title;
    [SerializeField] private TextMeshProUGUI ESC;
    [SerializeField] private List<float> LightDelays;
    [SerializeField] private List<float> OffTime;
    private float tik = 0;
    private bool isOkaytoPlay = false;
    private int idx = 0;

    void Start()
    {
        TitleImage.color = new Color(1, 1, 1, 0);
        FlashLight.color = new Color(1, 1, 1, 0);
        Title.color = new Color(1, 1, 1, 0);
        ESC.color = new Color(1, 1, 1, 0);
        StartCoroutine(OpenTitle());
    }

    void Update()
    {
        tik += Time.deltaTime;
        if (tik > LightDelays[idx])
        {
            tik = 0;
            idx = (idx + 1) % LightDelays.Count;
            StartCoroutine(SpotLighting(OffTime[idx]));
        }

        if (isOkaytoPlay)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                SceneManager.LoadScene("Hotel");
            }
            if(Input.GetKeyDown(KeyCode.Escape))
                Application.Quit();
        }
    }

    IEnumerator SpotLighting(float seconds)
    {
        SpotLight.SetActive(false);

        yield return new WaitForSeconds(seconds);
        
        SpotLight.SetActive(true);
    }

    IEnumerator OpenTitle()
    {
        yield return new WaitForSeconds(1.0f);
        
        while (TitleImage.color.a < 1)
        {
            TitleImage.color += new Color(0, 0, 0, Time.deltaTime / 2);
            yield return null;
        }

        yield return new WaitForSeconds(0.5f);
        
        while (FlashLight.color.a < 1)
        {
            FlashLight.color += new Color(0, 0, 0, Time.deltaTime / 3);
            yield return null;
        }
        
        yield return new WaitForSeconds(0.5f);
        
        while (Title.color.a < 1)
        {
            Title.color += new Color(0, 0, 0, Time.deltaTime);
            ESC.color += new Color(0, 0, 0, Time.deltaTime);
            yield return null;
        }

        isOkaytoPlay = true;
    }
}
