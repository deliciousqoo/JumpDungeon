using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    [SerializeField]
    private GameManager GM;

    [SerializeField]
    private GameObject[] Stages;

    private void Start()
    {
        StageSetUp(0);
    }

    private void StageSetUp(int stageNum)
    {
        Stages[stageNum].SetActive(true);
    }
}

