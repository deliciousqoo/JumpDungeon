using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEditor.PackageManager.UI;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] pages;

    public int pageCount = 0;
    private int pageOrder = 0;

    [SerializeField]
    private GameObject settingBoard, blackBoard, languageBoard, clearBoard, characterBoard, gameBoard;
    [SerializeField]
    private Button levelLeft, levelRight;
    [SerializeField]
    private GameObject[] clearStars;

    public void Start()
    {
        switch (SceneManager.GetActiveScene().name)
        {
            case "Menu":
                break;
            case "MainGame":
                break;
        }
    }

    public void ButtonEvent()
    {
        if (EventSystem.current.currentSelectedGameObject != null)
        {
            GameObject clickObject = EventSystem.current.currentSelectedGameObject;
            if(clickObject.tag == "InGameSetting")
            {
                switch(clickObject.name)
                {
                    case "Pause":
                        settingBoard.SetActive(true);
                        blackBoard.SetActive(true);
                        Time.timeScale = 0f;
                        break;
                    case "Exit":
                        SceneManager.LoadScene(0);
                        Time.timeScale = 1f;
                        break;
                    case "Cancel":
                        settingBoard.SetActive(false);
                        blackBoard.SetActive(false);
                        Time.timeScale = 1f;
                        break;
                    case "Restart":
                        SceneManager.LoadScene(1);
                        Time.timeScale = 1f;
                        break;
                    case "Continue":
                        settingBoard.SetActive(false);
                        blackBoard.SetActive(false);
                        Time.timeScale = 1f;
                        break;
                }
            }
            else if(clickObject.tag == "Menu")
            {
                switch(clickObject.name)
                {
                    case "LevelLeft":
                        pageCount--;
                        break;
                    case "LevelRight":
                        pageCount++;
                        break;
                    case "Store":
                        break;
                    case "Ranking":
                        break;
                    case "Infinity":
                        break;
                    case "Character":
                        characterBoard.SetActive(true);
                        break;
                    default:
                        GameManager.instance.stageNum = int.Parse(clickObject.name)-1;
                        gameBoard.SetActive(true);
                        blackBoard.SetActive(true);
                        for(int i=0;i<clearStars.Length;i++)
                        {
                            if (i < StageManager.instance.GetStageClearCheckValue(GameManager.instance.stageNum) - 1)
                            {
                                clearStars[i].SetActive(true);
                            }
                            else clearStars[i].SetActive(false);
                        }
                        break;
                }
                if (pageCount == pages.Length) pageCount %= pages.Length;
                else if (pageCount < 0) pageCount = pages.Length-1;

                SetLevelPage();
            }
            else if(clickObject.tag == "OutGameSetting")
            {
                switch(clickObject.name)
                {
                    case "Setting":
                        settingBoard.SetActive(true);
                        blackBoard.SetActive(true);
                        break;
                    case "Cancel1": // Setting Board
                        settingBoard.SetActive(false);
                        blackBoard.SetActive(false);
                        break;
                    case "Cancel2": // Game Board
                        gameBoard.SetActive(false);
                        blackBoard.SetActive(false);
                        break;
                    case "Language":
                        languageBoard.SetActive(true);
                        settingBoard.SetActive(false);
                        break;
                    case "Ad":
                        break;
                    case "Start":
                        SceneManager.LoadScene(1);
                        break;
                }
            }
            else if(clickObject.tag == "LanguageSetting")
            {
                switch(clickObject.name)
                {
                    case "Cancel":
                        settingBoard.SetActive(true);
                        languageBoard.SetActive(false);
                        break;
                    default:
                        break;
                }
            }
            else if(clickObject.tag == "ClearSetting")
            {
                switch(clickObject.name)
                {
                    case "Button1":
                        break;
                    case "Button2":
                        SceneManager.LoadScene(0);
                        break;
                }
            }
        }
    }

    public void SetLevelPage()
    {
        for(int i=0;i<pages.Length;i++)
        {
            if (pageCount == i) { pages[i].SetActive(true); }
            else { pages[i].SetActive(false); }
        }
        GameManager.instance.menuCount = pageCount;
        StageManager.instance.MenuStageSetUp();
    }

    public void OnCompletedPanelCall()
    {
        clearBoard.SetActive(true);
        blackBoard.SetActive(true);
        StartCoroutine("OnCompletedPanel", 2);
    }

    private IEnumerator OnCompletedPanel(int step)
    {
        clearStars[step - 2].GetComponent<Animator>().SetTrigger("Clear");
        yield return new WaitForSecondsRealtime(1.0f);

        if(step < StageManager.instance.GetStageClearCheckValue(GameManager.instance.stageNum))
        {
            StartCoroutine("OnCompletedPanel", step + 1);
        }
    }
}
