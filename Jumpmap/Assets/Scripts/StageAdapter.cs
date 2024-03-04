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
    [SerializeField]
    private GameObject[] stageWalls;

    public void Start()
    {
        switch (SceneManager.GetActiveScene().name)
        {
            case "Menu":
                Debug.Log("Menu");
                MenuStageSetUp();
                break;
            case "MainGame":
                Debug.Log("MainGame");
                StageSetUp(GameManager.instance.stageNum);
                break;
        }
    }

    public void StageSetUp(int stageNum)
    {
        Debug.Log(stageNum);
        for (int i = 0; i < stageWalls.Length; i++)
        {
            if (GameManager.instance.menuCount == i) { stageWalls[i].SetActive(true); }
            else { stageWalls[i].SetActive(false); }
        }

        for (int i = 0; i < gameStages.Length; i++)
        {
            if (stageNum == i) { gameStages[i].SetActive(true); }
            else { gameStages[i].SetActive(false); }
        }
    }

    public void MenuStageSetUp()
    {
        int[] tempCheckList = StageManager.instance.GetStageClearCheckList();
        int pageCount = GameManager.instance.menuCount;

        for (int i = 0; i < 12; i++)
        {
            //Debug.Log(i + pageCount * 12 + ": " + tempCheckList[i + pageCount * 12]);
            int clearIndex = i + pageCount * 12;
            switch (tempCheckList[clearIndex])
            {
                case 0:
                    menuStages[clearIndex].GetComponent<ButtonImage>().MenuStageButtonChange(false);
                    break;
                case 1:
                    menuStages[clearIndex].GetComponent<ButtonImage>().MenuStageButtonChange(true);
                    break;
                default:
                    break;
            }
        }
    }
}
