using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopyTransform : MonoBehaviour
{
    [SerializeField] private Transform targetT;

    [SerializeField] private Vector3 positionOffset;

    private void Awake()
    {
        positionOffset = transform.position - targetT.position;
    }

    private void LateUpdate()
    {
        transform.position = targetT.position + positionOffset;
        transform.rotation = targetT.rotation;
    }
}
