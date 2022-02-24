using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    [SerializeField] ResourceManager resourceManager;
    [SerializeField] UIManager uiManager;
    [SerializeField] SoundManager soundManager;
    [SerializeField] GameObject gameManager;
    [SerializeField] SubtitleManager subtitleManager;
    [SerializeField] PlayerController playerController;

    public static ResourceManager ResourceManager => instance.resourceManager;
    public static UIManager UIManager => instance.uiManager;
    public static SoundManager SoundManager => instance.soundManager;
    public static SubtitleManager SubtitleManager => instance.subtitleManager;
    public static PlayerController PlayerController => instance.playerController;

    public GameObject Player;

    private List<string> Inventory = new List<string>();

    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                return null;
            }

            return instance;
        }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = gameManager.GetComponent<GameManager>();
        }
        else
        {
            Destroy(this.gameObject);
        }

        DOTween.Init(false, false, LogBehaviour.Default).SetCapacity(100, 20);
        //LoadingManager.Instance.OnSceneLoaded += Initialize;
    }
    
    public void AddInventory(string id)
    {
        Inventory.Add(id);
        Debug.Log(String.Format("아이템 {0} | 인벤토리에 들어왔음 {1}", id, Inventory.Contains(id)));
    }
    public bool HasItem(string id)
    {
        return Inventory.Contains(id);
    }
    public bool UseItem(string id)
    {
        return Inventory.Remove(id);
    }
}