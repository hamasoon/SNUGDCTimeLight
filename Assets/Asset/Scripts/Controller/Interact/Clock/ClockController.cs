using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockController : MonoBehaviour
{
    private Clock clock;    
    private bool timelock = false;

    void Start()
    {
        clock = gameObject.GetComponent<Clock>();
    }

    void Update()
    {
        if(!GameManager.PlayerController.canMove)
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
    }

    IEnumerator ClockMovement(int wise)
    {
        timelock = true;
        clock.minutes += wise;
        yield return new WaitForSeconds(0.1f);
        timelock = false;
    }
}
