using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverLcokController : MonoBehaviour
{
    [SerializeField] private List<LeverController> Ctrls;
    public bool Locked = true;
    public void Check()
    {
        foreach (var ctrl in Ctrls)
        {
            if (!ctrl.Activated) return;
        }

        Locked = false;
    }
}
