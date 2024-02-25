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
    private int[] stageClearCheckList;

    private int stageNum;

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

    public int[] GetStageClearCheckList()
    {
        return stageClearCheckList;
    }

    public void SetStageClearCheckList(int[] arr)
    {
        stageClearCheckList = arr;
    }

    
}