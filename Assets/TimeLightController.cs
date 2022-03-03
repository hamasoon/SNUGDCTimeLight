using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeLightController : MonoBehaviour, IInteractable
{
    [SerializeField, Range(0, 10f)] float speed = 5f;
    [SerializeField, Range(0, 1f)] float deletingDist = 0.5f;
    private Transform playerTransform;

    private bool get;
    public void Interact()
    {
        if (get) return;
        get = true;
        FindObjectOfType<LightController>().lightTaken = true;
    }

    private void Start()
    {
        playerTransform = GameManager.Instance.Player.transform;
    }

    private void Update()
    {
        if (get)
        {
            Vector3 moveDir = playerTransform.position - transform.position + new Vector3(0, 1f, 0);
            if (moveDir.magnitude < deletingDist) Destroy(gameObject);
            transform.position += moveDir * speed * Time.deltaTime;
        }
    }
}
