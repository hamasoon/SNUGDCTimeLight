using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.TextCore;

public class CylinderController : MonoBehaviour
{
    //[SerializeField] List<GameObject> Panel;
    [SerializeField] DoorController LookDoor;
    [SerializeField] List<int> Password = new List<int>(4);
    
    private List<float> PanelAngles = new List<float>() {0f, 90f, 180f, 270f};
    private List<int> PanelPos = new List<int>() {0, 0, 0, 0};
    private bool timelock = false;
    private int pos = 5;

    public void SpinCylinder(GameObject go, int num)
    {
        if (!timelock)
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
}
