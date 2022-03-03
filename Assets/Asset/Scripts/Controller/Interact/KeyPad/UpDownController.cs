using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpDownController : MonoBehaviour
{
    [SerializeField] private List<int> Password;
    [SerializeField] private DoorController doorController;
    [SerializeField] private List<TextMeshPro> Texts;
    private List<int> Input = Enumerable.Repeat(0, 3).ToList();

    private bool isClear = false;

    public void Check()
    {
        for (int i = 0; i < Password.Count; i++)
        {
            if (Password[i] != Input[i]) return;
        }
        
        isClear = !isClear;
        doorController.disableLock();
    }

    public void UpDownButtonInput(int idx, int ways)
    {
        if(!isClear)
        {
            if (Input[idx] == 0 && ways == -1) return;
            else if (Input[idx] == 9 && ways == 1) return;

            Input[idx] += ways;

            Check();

            Texts[idx].text = Input[idx].ToString();
        }
    }
}
