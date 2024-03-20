using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StageManager : MonoBehaviour
{
    public static StageManager instance = null;

    private StageAdapter stageAdapter;

    [SerializeField]
    private int[] stageClearCheckList; //0:lock, 1:unlock, 2~4:start_count

    public int stageNum;
    public int stageProgressStep;

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
    private void Start()
    {
        stageAdapter = GameObject.Find("StageAdapter").GetComponent<StageAdapter>();
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("OnScenenLoaded: " + scene.name);
        switch (scene.name)
        {
            case "Menu":
                stageAdapter = GameObject.Find("StageAdapter").GetComponent<StageAdapter>();
                break;
            case "MainGame":
                stageAdapter = GameObject.Find("StageAdapter").GetComponent<StageAdapter>();
                stageProgressStep = 1;
                break;
        }
    }
    public void SetStageClearCheckList(int[] arr)
    {
        stageClearCheckList = arr;
    }
    public int[] GetStageClearCheckList()
    {
        return stageClearCheckList;
    }
    public int GetStageClearCheckValue(int index)
    {
        return stageClearCheckList[index];
    }
    

    public void MenuStageSetUp()
    {
        stageAdapter.MenuStageSetUp();
    }
    public void StageProgressSetUp()
    {
        stageClearCheckList[stageNum] = Mathf.Max(stageClearCheckList[stageNum], stageProgressStep);
        if(stageNum < stageClearCheckList.Length - 1)
        {
            if (stageClearCheckList[stageNum] >= 1 && stageClearCheckList[stageNum+1] == 0)
            {
                stageClearCheckList[stageNum + 1] = 1;
            } 
        }
    }
}