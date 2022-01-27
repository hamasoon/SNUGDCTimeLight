using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    static GameManager instance;

    [SerializeField]
    public InputManager inputManager;
    [SerializeField]
    public ResourceManager resourceManager;
    [SerializeField]
    public UIManager uiManager;
    [SerializeField]
    public SoundManager soundManager;
    [SerializeField]
    GameObject gameManager;

    public static InputManager InputManager {get {return instance.inputManager;} }
    public static ResourceManager ResourceManager {get {return instance.resourceManager;} }
    public static UIManager UIManager {get {return instance.uiManager;} }
    public static SoundManager SoundManager {get {return instance.soundManager;}}

    public GameObject Player;

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
}
