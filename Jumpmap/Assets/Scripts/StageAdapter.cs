using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StageAdapter : MonoBehaviour
{
    [SerializeField]
    private Button[] menuStages;
    [SerializeField]
    private GameObject[] gameStages;

    public void Start()
    {
        switch (SceneManager.GetActiveScene().name)
        {
            case "Menu":
                break;
            case "MainGame":
                //StageSetUp(GameManager.instance.stageNum);
                break;
        }
    }

    public void StageSetUp(int stageNum)
    {
        Debug.Log(stageNum);

        for (int i = 0; i < gameStages.Length; i++)
        {
            if (stageNum == i) { gameStages[i].SetActive(true); }
            else { gameStages[i].SetActive(false); }
        }
    }
}
