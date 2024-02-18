using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    private CameraManager cameraManager;
    private StageManager stageManager;
    private UIManager uiManager;

    private Player player;

    public int check = 5;
    public int stageNum;

    private void Awake() 
    {
        if(instance == null)
        {
            Debug.Log("1");
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Debug.Log("2");
            if (instance != this) { Destroy(this.gameObject); }
        }
    }

    private void Start()
    {
        switch (SceneManager.GetActiveScene().name)
        {
            case "Menu":
                uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();
                stageManager = GameObject.Find("StageManager").GetComponent<StageManager>();
                break;
            case "MainGame":
                cameraManager = GameObject.Find("CameraManager").GetComponent<CameraManager>();
                stageManager = GameObject.Find("StageManager").GetComponent<StageManager>();
                uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();
                player = GameObject.Find("Player").GetComponent<Player>();
                break;
        }
    }

    /******************** EffectManager ********************/



    /*******************************************************/
}
