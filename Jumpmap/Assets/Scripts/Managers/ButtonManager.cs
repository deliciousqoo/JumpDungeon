using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEditor.PackageManager.UI;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] pages;

    public int pageCount = 0;
    private int pageOrder = 0;

    [SerializeField]
    private GameObject settingBoard, blackBoard;
    [SerializeField]
    private Button levelLeft, levelRight;

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
                if (pageCount == pages.Length) pageCount %= pages.Length;
                else if (pageCount < 0) pageCount = pages.Length-1;

                SetLevelPage();
            }
            if(clickObject.tag == "OutGameSetting")
            {
                switch (clickObject.name)
                {
                    case "Setting":
                        settingBoard.SetActive(true);
                        blackBoard.SetActive(true);
                        break;
                    case "Cancel":
                        settingBoard.SetActive(false);
                        blackBoard.SetActive(false);
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
    }


}
