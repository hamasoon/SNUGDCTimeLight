using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class SubtitleManager : MonoBehaviour
{
    [SerializeField] StringStringDictionary subtitles = new StringStringDictionary();
    [SerializeField] SerialilzeDicStringVector3 subsLocation = new SerialilzeDicStringVector3();
    [SerializeField] Text text;
    [SerializeField, Range(0f, 40f)] float fadeTime = 5f; 
    [SerializeField, Range(0, 10f)] float waitingTime = 5f; 

    private bool lockSubtitle = false;

    void Start()
    {
        subsLocation.Add("origin", text.transform.position);
    }

    // Update is called once per frame
    private void SetSubtitle(string key)
    {
        text.text = subtitles[key];
    }

    public void SetSubtitleLocation(string key)
    {
        text.transform.position = subsLocation[key];
    }

    public void SetSubtitleOrigin()
    {
        text.transform.position = subsLocation["origin"];
    }

    public void showSubtitle(string key)
    {
        if(!lockSubtitle){
            SetSubtitle(key);
            StartCoroutine(CoFadeInandOut(fadeTime));
        }
    }

    public void setTutorial(string key)
    {
        if(!lockSubtitle){
            SetSubtitle(key);
            StartCoroutine(CoFadeIn(fadeTime));
        }
    }

    public void destroyTutorial()
    {
        StartCoroutine(CoFadeOut(fadeTime));
    }
    
    IEnumerator CoFadeInandOut(float fadeOutTime)
    {
        lockSubtitle = true;
        Color tempColor = text.color;
        while(tempColor.a < 1){
            tempColor.a += Time.deltaTime * fadeOutTime;
            text.color = tempColor;

            if(tempColor.a >= 1) 
            {
                tempColor.a = 1;
                text.color = tempColor;   
            }

            yield return new WaitForSeconds(0.1f);
        }

        yield return new WaitForSeconds(waitingTime);

        while(tempColor.a > 0f){
            tempColor.a -= Time.deltaTime * fadeOutTime;
            text.color = tempColor;

            if(tempColor.a <= 0f) tempColor.a = 0f;

            yield return new WaitForSeconds(0.1f);
        }

        text.color = tempColor;
        lockSubtitle = false;
    }
    
    IEnumerator CoFadeIn(float fadeOutTime)
    {
        lockSubtitle = true;
        Color tempColor = text.color;
        while(tempColor.a < 1){
            tempColor.a += Time.deltaTime * fadeOutTime;
            text.color = tempColor;

            if(tempColor.a >= 1) 
            {
                tempColor.a = 1;
                text.color = tempColor;   
            }

            yield return new WaitForSeconds(0.1f);
        }
    }
    
    IEnumerator CoFadeOut(float fadeOutTime)
    {
        Color tempColor = text.color;
        while(tempColor.a > 0f){
            tempColor.a -= Time.deltaTime * fadeOutTime;
            text.color = tempColor;

            if(tempColor.a <= 0f) tempColor.a = 0f;

            yield return new WaitForSeconds(0.1f);
        }

        text.color = tempColor;
        lockSubtitle = false;
    }
}
