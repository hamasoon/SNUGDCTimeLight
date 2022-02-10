using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    static GameManager instance;

    [SerializeField] ResourceManager resourceManager;
    [SerializeField] UIManager uiManager;
    [SerializeField] SoundManager soundManager;
    [SerializeField] GameObject gameManager;
    [SerializeField] SubtitleManager subtitleManager;
    [SerializeField] PlayerController playerController;

    public static ResourceManager ResourceManager {get {return instance.resourceManager;} }
    public static UIManager UIManager {get {return instance.uiManager;} }
    public static SoundManager SoundManager {get {return instance.soundManager;}}
    public static SubtitleManager SubtitleManager {get {return instance.subtitleManager;}}
    public static PlayerController PlayerController {get {return instance.playerController;}}

    public GameObject Player;

    private List<string> Inventory = new List<string>();

    static public GameManager Instance{
        get{
            if(instance == null){
                return null;
            }
            return instance;
        }
    }

    void Awake() {
        if(instance == null){
            instance  = gameManager.GetComponent<GameManager>();
        }
        else
        {
            Destroy(this.gameObject);
        }

        //LoadingManager.Instance.OnSceneLoaded += Initialize;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void addInventory(string name)
    {
        Inventory.Add(name);    
        Debug.Log(String.Format("아이템 {0} | 인벤토리에 들어왔음 {1}", name, Inventory.Contains(name)));
    }

    public bool isHaveItem(string name)
    {
        return Inventory.Contains(name);
    }

    public bool useItem(string name)
    {   
        return Inventory.Remove(name);
    }
}
