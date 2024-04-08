using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEditor.PackageManager.UI;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] pages;

    public int pageCount = 0;
    private int pageOrder = 0;

    [SerializeField]
    private GameObject settingBoard, blackBoard, languageBoard, clearBoard, characterBoard, gameBoard, choiceOutline, storeBoard;
    [SerializeField]
    private Button levelLeft, levelRight;
    [SerializeField]
    private GameObject[] clearStars;

    public void Start()
    {
        //Debug.Log(characterBoard.gameObject.transform.position);
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
                        //StartCoroutine(UIMovement_X(pages[pageCount], , pages[pageCount].transform.position));
                        pageCount--;
                        if (pageCount == pages.Length) pageCount %= pages.Length;
                        else if (pageCount < 0) pageCount = pages.Length - 1;
                        SetLevelPage();
                        break;
                    case "LevelRight":
                        pageCount++;
                        if (pageCount == pages.Length) pageCount %= pages.Length;
                        else if (pageCount < 0) pageCount = pages.Length - 1;
                        SetLevelPage();
                        break;
                    case "Setting":
                        //settingBoard.SetActive(true);
                        Debug.Log(settingBoard.transform.position);
                        StartCoroutine(UIMovement_X(settingBoard, new Vector3(151f, 304f, 0f), settingBoard.transform.position, 0));
                        blackBoard.SetActive(true);
                        break;
                    case "Store":

                        break;
                    case "Ranking":
                        break;
                    case "Infinity":
                        break;
                    case "Character":
                        //characterBoard.SetActive(true);
                        StartCoroutine(UIMovement_Y(characterBoard, new Vector3(171f, -10f, 0), characterBoard.transform.position, 0));
                        blackBoard.SetActive(true);
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
            }
            else if(clickObject.tag == "OutGameSetting")
            {
                switch(clickObject.name)
                {
                    case "Cancel1": // Setting Board
                        StartCoroutine(UIMovement_X(settingBoard, new Vector3(560f, 304f, 0f), settingBoard.transform.position, 2));
                        //settingBoard.SetActive(false);
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
                        GameManager.instance.shieldCheck = true;
                        SceneManager.LoadScene(1);
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
            else if(clickObject.tag == "CharacterSetting")
            {
                switch (clickObject.name)
                {
                    case "Cancel":
                        //characterBoard.SetActive(false);
                        StartCoroutine(UIMovement_Y(characterBoard, new Vector3(171f, -440f, 0), characterBoard.transform.position, 2));
                        blackBoard.SetActive(false);
                        break;
                    default:
                        Debug.Log(clickObject.transform.position);
                        choiceOutline.transform.position = clickObject.transform.position;
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

    private IEnumerator UIMovement_Y(GameObject target, Vector3 targetPos, Vector3 orgPos, int count)
    {
        if (count == 3) yield break;

        target.transform.position = orgPos;
        Vector3 temp_pos = target.transform.position;

        //Set Direction
        float dir;
        if (targetPos.y < orgPos.y) dir = -1;
        else dir = 1;

        float x = 1, slope = 0.08f, temp_value = 0;
        yield return new WaitForSecondsRealtime(0.03f);
        while ((targetPos.y - target.transform.position.y) * dir > 0)
        {
            //Debug.Log(Mathf.Abs(target.transform.position.y - targetPos.y));
            temp_pos.y = dir * slope * Mathf.Pow(x, 2) + orgPos.y;
            target.transform.position = temp_pos;
            x += 1f;
            yield return new WaitForSecondsRealtime(0.001f);
        }

        target.transform.position = targetPos;
        Debug.Log(target.transform.position);

        if (count == 0) StartCoroutine(UIMovement_Y(target, new Vector3(target.transform.position.x, target.transform.position.y + 30f * dir * -1, target.transform.position.z), target.transform.position, count + 1));
        else if(count == 1) StartCoroutine(UIMovement_Y(target, new Vector3(target.transform.position.x, target.transform.position.y + 10f * dir * -1, target.transform.position.z), target.transform.position, count + 1));
    }

    private IEnumerator UIMovement_X(GameObject target, Vector3 targetPos, Vector3 orgPos, int count)
    {
        if (count == 3) yield break;
        
        target.transform.position = orgPos;
        Vector3 temp_pos = target.transform.position;

        //Set Direction
        float dir = 0;
        if (targetPos.x < orgPos.x) dir = -1;
        else dir = 1;

        float x = 1, slope = 0.1f, temp_value = 0;
        yield return new WaitForSecondsRealtime(0.03f);
        while ((targetPos.x - target.transform.position.x) * dir > 0)
        {
            //Debug.Log(Mathf.Abs(target.transform.position.y - targetPos.y));
            temp_pos.x = dir * slope * Mathf.Pow(x, 2) + orgPos.x;
            target.transform.position = temp_pos;
            x += 1f;
            yield return new WaitForSecondsRealtime(0.001f);
        }

        target.transform.position = targetPos;
        Debug.Log(target.transform.position);
        

        if (count == 0) StartCoroutine(UIMovement_X(target, new Vector3(targetPos.x + 30f * dir * -1, target.transform.position.y, target.transform.position.z), target.transform.position, count + 1));
        else if (count == 1) StartCoroutine(UIMovement_X(target, new Vector3(targetPos.x + 10f * dir * -1, target.transform.position.y, target.transform.position.z), target.transform.position, count + 1));
    }
}
