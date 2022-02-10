using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutlineDrawer : MonoBehaviour
{
    private Outline outline;

    void Start()
    {
        outline = gameObject.GetComponent<Outline>();
        outline.enabled = false;
    }

    private void OnMouseEnter()
    {
        outline.enabled = true;
    }

    private void OnMouseExit()
    {
        outline.enabled = false;
    }
}
