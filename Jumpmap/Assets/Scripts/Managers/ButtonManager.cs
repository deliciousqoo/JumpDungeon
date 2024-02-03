using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager.UI;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class ButtonManager : MonoBehaviour
{
    [SerializeField]
    private UIManager uiManager;


    [SerializeField]
    private GameObject pauseBoard;
    [SerializeField]
    private GameObject blackBoard;

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
                        break;
                    case "Restart":
                        break;
                    case "Continue":
                        pauseBoard.SetActive(false);
                        blackBoard.SetActive(false);
                        Time.timeScale = 1f;
                        break;
                }
            }
        }
    }
}
