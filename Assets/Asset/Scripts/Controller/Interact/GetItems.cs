using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetItems : MonoBehaviour, IGetable
{
    private string itemName;
    private bool isGot = false;
    private Transform pTransform;
    [SerializeField, Range(0, 10f)] float speed = 5f;
    [SerializeField, Range(0, 1f)] float deletingDist = 0.5f;

    void Start()
    {
        itemName = gameObject.name;
        pTransform = GameManager.Instance.Player.transform;
    }

    void Update()
    {
        if(isGot){   
            Vector3 moveDir = pTransform.localPosition - transform.localPosition;
            if(moveDir.magnitude < deletingDist) Destroy(gameObject);
            transform.localPosition += moveDir * speed * Time.deltaTime;
        }
    }

    public void Interact()
    {
        if(!isGot){
            isGot = true;
            getItem();
        }
    }

    public void getItem(){
        GameManager.Instance.addInventory(itemName);
    }
}
