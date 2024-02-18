using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEditor.PackageManager.UI;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    [SerializeField]
    private UIManager uiManager;

    [SerializeField]
    private GameObject pauseBoard;
    [SerializeField]
    private GameObject blackBoard;

    [SerializeField]
    private GameObject[] pages;

    private int pageCount = 0;
    private int pageOrder = 0;

    private Button levelLeft, levelRight;

    public void Start()
    {
        switch(SceneManager.GetActiveScene().name)
        {
            case "Menu":
                levelLeft = GameObject.Find("LevelLeft").GetComponent<Button>();
                levelRight = GameObject.Find("LevelRight").GetComponent<Button>();
                SetLevelPage();
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
                        pauseBoard.SetActive(true);
                        blackBoard.SetActive(true);
                        Time.timeScale = 0f;
                        break;
                    case "Exit":
                        SceneManager.LoadScene(0);
                        Time.timeScale = 1f;
                        break;
                    case "Restart":
                        SceneManager.LoadScene(1);
                        Time.timeScale = 1f;
                        break;
                    case "Continue":
                        pauseBoard.SetActive(false);
                        blackBoard.SetActive(false);
                        Time.timeScale = 1f;
                        break;
                }
            }
            if(clickObject.tag == "Menu")
            {
                switch(clickObject.name)
                {
                    case "LevelLeft":
                        pageCount--;
                        break;
                    case "LevelRight":
                        pageCount++;
                        break;
                    default:
                        GameManager.instance.stageNum = int.Parse(clickObject.name)-1;
                        SceneManager.LoadScene(1);
                        break;
                }

                SetLevelPage();
            }
        }
    }

    public void SetLevelPage()
    {
        if (pageCount == 0) { levelLeft.interactable = false; }
        else if (pageCount == pages.Length) { levelRight.interactable = false; }
        else { levelLeft.interactable = true; levelRight.interactable = true; }

        for(int i=0;i<pages.Length;i++)
        {
            if (pageCount == i) { pages[i].SetActive(true); }
            else { pages[i].SetActive(false); }
        }
    }


}
