using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    private CameraManager cameraManager;
    private StageManager stageManager;

    [SerializeField]
    private Player player;

    public int check = 5;
    public int stageNum;
    public int menuCount;

    private void Awake() 
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            if (instance != this) { Destroy(this.gameObject); }
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
                break;
            case "MainGame":
                cameraManager = GameObject.Find("CameraManager").GetComponent<CameraManager>();
                player = GameObject.Find("Player").GetComponent<Player>();
                break;
        }
    }


    /*********************** Player ***********************/

    public void OnCompletedCall(Vector2 targetPos)
    {
        player.OnCompletedCall(targetPos);
    }

    /******************************************************/
}
