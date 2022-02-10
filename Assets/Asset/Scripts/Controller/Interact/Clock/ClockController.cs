using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockController : MonoBehaviour
{
    [SerializeField] GameObject Panel;
    [SerializeField] Clock pClock;
    [SerializeField] Clock oClock;
    private bool timelock = false;

    void Update()
    {
        if (Panel.activeSelf)
        {
            if (!timelock)
            {
                if (Input.GetKeyDown(KeyCode.D))
                {
                    StartCoroutine(ClockMovement(1));
                }
                else if (Input.GetKeyDown(KeyCode.A))
                {
                    StartCoroutine(ClockMovement(-1));
                }
            }
        }
        
        oClock.minutes = pClock.minutes;
        oClock.hour = pClock.hour;
    }

    IEnumerator ClockMovement(int wise)
    {
        timelock = true;
        pClock.minutes += wise;
        yield return new WaitForSeconds(0.1f);
        timelock = false;
    }
}
