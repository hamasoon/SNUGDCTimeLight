using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.TextCore;

public class CylinderController : MonoBehaviour
{
    [SerializeField] List<GameObject> Panel;
    [SerializeField] DoorController LookDoor;
    [SerializeField] List<int> Password = new List<int>(4);
    //private List<float> Angles = new List<float>() {-16f, -5.5f, 5.5f, 16f};
    private List<float> PanelAngles = new List<float>() {0f, 90f, 180f, 270f};
    private List<int> PanelPos = new List<int>() {0, 0, 0, 0};
    private bool timelock = false;
    private int pos = 5;

    public void SpinCylinder(GameObject go, int num)
    {
        if(!timelock)
        {
            StartCoroutine(SpinningCylinder(go, num));
        }
    }

    private void CheckCorrect()
    {
        for(int i = 0; i < 4; i++)
        {
            if(PanelPos[i] != Password[i]) return;
        }
        
        timelock = true;
        LookDoor.disableLock();
    } 

    IEnumerator SpinningCylinder(GameObject go, int num)
    {
        timelock = true;
        PanelPos[num] = (PanelPos[num] - 1) % 4;
        if (PanelPos[num] < 0) PanelPos[num] += 4;
        LeanTween.rotateLocal(go, new Vector3(0, PanelAngles[PanelPos[num]], 0), 0.5f)
            .setEase(LeanTweenType.easeInOutQuad);
        yield return new WaitForSeconds(0.5f);
        timelock = false;
        CheckCorrect();
    }

    /*void Update()
    {
        if(gameObject.GetComponent<FocusController>().isFocused)
        {
            if (!timelock)
            {
                if (Input.GetKeyDown(KeyCode.D))
                {
                    StartCoroutine(SetCylinderPos(1));
                }
                else if (Input.GetKeyDown(KeyCode.A))
                {
                    StartCoroutine(SetCylinderPos(-1));
                }
                else if (Input.GetKeyDown(KeyCode.W) && pos != 5)
                {
                    StartCoroutine(SpinCylinder(Panel[pos], 1));
                }
                else if (Input.GetKeyDown(KeyCode.S) && pos != 5)
                {
                    StartCoroutine(SpinCylinder(Panel[pos], -1));
                }
            }
        }
        else
        {
            pos = 5;
        }
    }

    IEnumerator SpinCylinder(GameObject go, int i)
    {
        timelock = true;
        PanelPos[pos] = (PanelPos[pos] - i) % 4;
        if (PanelPos[pos] < 0) PanelPos[pos] += 4;
        LeanTween.rotateLocal(go, new Vector3(0, PanelAngles[PanelPos[pos]], 0), 0.5f)
            .setEase(LeanTweenType.easeInOutQuad);
        yield return new WaitForSeconds(0.5f);
        timelock = false;
    }

    IEnumerator SetCylinderPos(int i)
    {
        timelock = true;
        if((i == 1 && pos == 3) || (i == -1 && pos == 0)){}
        else
        {
            if (pos == 5) pos = (int)(1.5 + 0.5 * i);
            else pos = (pos + i) % 4;
            Debug.Log(pos);
            LeanTween.rotateY(CameraHandler, Angles[pos], 0.5f).setEase(LeanTweenType.easeOutCubic);
            yield return new WaitForSeconds(0.5f);
            timelock = false;
        }
        timelock = false;
    }*/
}
