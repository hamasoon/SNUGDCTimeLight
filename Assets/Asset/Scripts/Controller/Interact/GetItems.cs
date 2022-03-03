using UnityEngine;

public class GetItems : MonoBehaviour, IGetable
{
    [SerializeField, Range(0, 10f)] float speed = 5f;
    [SerializeField, Range(0, 1f)] float deletingDist = 0.5f;
    [SerializeField] private bool pastItem;
    [SerializeField] private Transform pastTransform;
    private string itemName;
    private bool isGot = false;
    private Transform playerTransform;

    void Start()
    {
        itemName = gameObject.name;
        playerTransform = GameManager.Instance.Player.transform;
        
    }

    void Update()
    {
        if (isGot)
        {
            if (pastItem)
            {
                Debug.Log(pastTransform.position);
                Vector3 moveDir = pastTransform.position - transform.position + new Vector3(0, 1f, 0);
                if (moveDir.magnitude < deletingDist) Destroy(gameObject);
                transform.position += moveDir * speed * Time.deltaTime;
            }
            else
            {
                Vector3 moveDir = playerTransform.position - transform.position + new Vector3(0, 1f, 0);
                if (moveDir.magnitude < deletingDist) Destroy(gameObject);
                transform.position += moveDir * speed * Time.deltaTime;
            }
            
            GameManager.SoundManager.PlaySE("GetItem", GetComponent<AudioSource>());
        }
    }

    public void Interact()
    {
        if (!isGot)
        {
            isGot = true;
            Debug.Log(1);
            GetItem();
        }
    }

    public void GetItem()
    {
        GameManager.Instance.AddInventory(itemName);
    }
}