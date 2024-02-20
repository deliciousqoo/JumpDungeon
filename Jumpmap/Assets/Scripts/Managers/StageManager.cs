using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] stages;

    [SerializeField]
    private int[] stageClearCheckList;

    private int stageNum;

    private void Start()
    {
        stageNum = GameManager.instance.stageNum;
        StageSetUp(stageNum);
    }

    public int[] GetStageClearCheckList()
    {
        return stageClearCheckList;
    }

    public void SetStageClearCheckList(int[] arr)
    {
        stageClearCheckList = arr;
    }

    private void StageSetUp(int stageNum)
    {
        for (int i = 0; i < stages.Length; i++)
        {
            if (stageNum == i) { stages[i].SetActive(true); }
            else { stages[i].SetActive(false); }
        }
    }
}