using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            if (instance != this) { Destroy(this.gameObject); }
        }
    }

    private CameraManager cameraManager;
    private StageManager stageManager;
    private UIManager uiManager;

    [SerializeField]
    private Player player;
    
    public Vector3 playerPos;
    public int check = 5, stageNum, menuCount, skinNum;
    public bool shieldCheck = false;

    private void Update()
    {
        switch(SceneManager.GetActiveScene().name)
        {
            case "MainGame":
                playerPos = player.gameObject.transform.position;
                break;
        }
    }

    private void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("OnScenenLoaded: " + scene.name);
        switch (scene.name)
        {
            case "Menu":
                uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();
                break;
            case "MainGame":
                cameraManager = GameObject.Find("CameraManager").GetComponent<CameraManager>();
                player = GameObject.Find("Player").GetComponent<Player>();
                uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();
                StageManager.instance.stageNum = stageNum;
                break;
        }
    }


    /*********************** Player ***********************/

    public void OnCompletedCall(Vector2 targetPos)
    {
        player.OnCompletedCall(targetPos);
    }
    public void PlayerHide()
    {
        player.PlayerHide();
    }

    /******************************************************/

    /*********************** ButtonManager ***********************/

    public void OnCompletedPanelCall()
    {
        uiManager.OnCompletedPanelCall();
    }

    /******************************************************/
}
