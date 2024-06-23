using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] pages;

    public int pageCount = 0;

    [SerializeField]
    private GameObject canvas, settingBoard, blackBoard1, blackBoard2, languageBoard, clearBoard, characterBoard, gameBoard, choiceOutline, storeBoard;
    [SerializeField]
    private Button levelLeft, levelRight;
    [SerializeField]
    private GameObject[] clearStars;

    public bool uiMovementCheck = false;
    public Stack<GameObject> activeBoardStack = new Stack<GameObject>();

    public void Start()
    {
        switch (SceneManager.GetActiveScene().name)
        {
            case "Menu":
                settingBoard.transform.position = new Vector3(canvas.transform.position.x * 2 + 224f * canvas.transform.localScale.x, canvas.transform.position.y, 0f);
                gameBoard.transform.position = new Vector3(canvas.transform.position.x * 2 + 252f * canvas.transform.localScale.x, canvas.transform.position.y, 0f);
                languageBoard.transform.position = new Vector3(canvas.transform.position.x * 2 + 224f * canvas.transform.localScale.x, canvas.transform.position.y, 0f);
                characterBoard.transform.position = new Vector3(canvas.transform.position.x, -763f * canvas.transform.localScale.x, 0f);
                storeBoard.transform.position = new Vector3(canvas.transform.position.x, -455f * canvas.transform.localScale.x, 0f);
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
            if (uiMovementCheck) return;
            uiMovementCheck = true;
            if (clickObject.tag == "InGameSetting")
            {
                switch(clickObject.name)
                {
                    case "Pause":
                        settingBoard.SetActive(true);
                        blackBoard1.SetActive(true);
                        Time.timeScale = 0f;
                        break;
                    case "Exit":
                        SceneManager.LoadScene(1);
                        Time.timeScale = 1f;
                        break;
                    case "Cancel":
                        settingBoard.SetActive(false);
                        blackBoard1.SetActive(false);
                        Time.timeScale = 1f;
                        break;
                    case "Restart":
                        SceneManager.LoadScene(2);
                        Time.timeScale = 1f;
                        break;
                    case "Continue":
                        settingBoard.SetActive(false);
                        blackBoard1.SetActive(false);
                        Time.timeScale = 1f;
                        break;
                }
            }
            else if (clickObject.tag == "OutGameSetting")
            {
                switch (clickObject.name)
                {
                    case "Cancel": // Setting Board
                        activeBoardStack.Pop();
                        StartCoroutine(UIMovement_X(settingBoard, new Vector3(canvas.transform.position.x * 2 + 224f * canvas.transform.localScale.x, canvas.transform.position.y, 0f), settingBoard.transform.position, 2, 1));
                        if (activeBoardStack.Count == 0)
                        {
                            blackBoard1.SetActive(false);
                        }
                        break;
                    case "Language":
                        StartCoroutine(UIMovement_X(languageBoard, new Vector3(canvas.transform.position.x - 20f, canvas.transform.position.y, 0f), languageBoard.transform.position, 0, 1));
                        activeBoardStack.Push(languageBoard);
                        break;
                    case "Ad":
                        GameManager.instance.shieldCheck = true;
                        SceneManager.LoadScene(2);
                        break;
                    case "Start":
                        SceneManager.LoadScene(2);
                        break;
                }
            }
            else if(clickObject.tag == "Menu")
            {
                switch(clickObject.name)
                {
                    case "LevelLeft":
                        StartCoroutine(UIMovement_X(pages[pageCount], new Vector3(canvas.transform.position.x * 2 + 206.5f * canvas.transform.localScale.x, pages[0].transform.position.y, 0f), pages[pageCount].transform.position, 2, 0));

                        SetLevelPage(--pageCount);

                        pages[pageCount].transform.position = new Vector3(-206.5f * canvas.transform.localScale.x, canvas.transform.position.y + 296f * canvas.transform.localScale.x, 0f);
                        StartCoroutine(UIMovement_X(pages[pageCount], new Vector3(canvas.transform.position.x + 20f, pages[pageCount].transform.position.y, 0f), pages[pageCount].transform.position, 0, 1));
                        break;
                    case "LevelRight":
                        StartCoroutine(UIMovement_X(pages[pageCount], new Vector3(-206.5f * canvas.transform.localScale.x, pages[pageCount].transform.position.y, 0f), pages[pageCount].transform.position, 2, 0));

                        SetLevelPage(++pageCount);

                        pages[pageCount].transform.position = new Vector3(canvas.transform.position.x * 2 + 206.5f * canvas.transform.localScale.x, canvas.transform.position.y + 296f * canvas.transform.localScale.x, 0f);
                        StartCoroutine(UIMovement_X(pages[pageCount], new Vector3(canvas.transform.position.x - 20f, pages[pageCount].transform.position.y, 0f), pages[pageCount].transform.position, 0, 1));
                        break;
                    case "Setting":
                        StartCoroutine(UIMovement_X(settingBoard, new Vector3(canvas.transform.position.x - 20f, canvas.transform.position.y, 0f), settingBoard.transform.position, 0, 1));
                        activeBoardStack.Push(settingBoard);
                        blackBoard1.SetActive(true);
                        break;
                    case "Store":
                        StartCoroutine(UIMovement_Y(storeBoard, new Vector3(canvas.transform.position.x, 126f * canvas.transform.localScale.x + 20f, 0f), storeBoard.transform.position, 0, 1));
                        activeBoardStack.Push(storeBoard);
                        blackBoard2.SetActive(true);
                        break;
                    case "Ranking":
                        break;
                    case "Infinity":
                        break;
                    case "Character":
                        StartCoroutine(UIMovement_Y(characterBoard, new Vector3(canvas.transform.position.x, -40f * canvas.transform.localScale.x, 0f), characterBoard.transform.position, 0, 1));
                        activeBoardStack.Push(characterBoard);
                        blackBoard1.SetActive(true);
                        break;
                    case "Box":

                        break;
                    case "BlackBoard1":
                    case "BlackBoard2":
                        switch(activeBoardStack.Pop().gameObject.name)
                        {
                            case "CharacterBoard":
                                StartCoroutine(UIMovement_Y(characterBoard, new Vector3(canvas.transform.position.x, -763f * canvas.transform.localScale.x, 0), characterBoard.transform.position, 2, 1));
                                break;
                            case "StoreBoard":
                                StartCoroutine(UIMovement_Y(storeBoard, new Vector3(canvas.transform.position.x, -455f * canvas.transform.localScale.x, 0), storeBoard.transform.position, 2, 1));
                                break;
                            case "OutGameSetting":
                                StartCoroutine(UIMovement_X(settingBoard, new Vector3(canvas.transform.position.x * 2 + 224f * canvas.transform.localScale.x, canvas.transform.position.y, 0f), settingBoard.transform.position, 2, 1));
                                break;
                            case "LanguageSetting":
                                StartCoroutine(UIMovement_X(languageBoard, new Vector3(canvas.transform.position.x * 2 + 224f * canvas.transform.localScale.x, canvas.transform.position.y, 0f), languageBoard.transform.position, 2, 1));
                                break;
                            case "GameBoard":
                                StartCoroutine(UIMovement_X(gameBoard, new Vector3(canvas.transform.position.x * 2 + 252f * canvas.transform.localScale.x, canvas.transform.position.y, 0f), gameBoard.transform.position, 2, 1));
                                break;
                        }
                        if(activeBoardStack.Count == 0)
                        {
                            blackBoard1.SetActive(false);
                            blackBoard2.SetActive(false);
                        }
                        break;
                    default:
                        GameManager.instance.stageNum = int.Parse(clickObject.name)-1;
                        StartCoroutine(UIMovement_X(gameBoard, new Vector3(canvas.transform.position.x - 20f, canvas.transform.position.y, 0f), settingBoard.transform.position, 0, 1));
                        activeBoardStack.Push(gameBoard);
                        blackBoard1.SetActive(true);
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
            else if(clickObject.tag == "LanguageSetting")
            {
                switch(clickObject.name)
                {
                    case "Cancel":
                        activeBoardStack.Pop();
                        StartCoroutine(UIMovement_X(languageBoard, new Vector3(canvas.transform.position.x * 2 + 224f * canvas.transform.localScale.x, canvas.transform.position.y, 0f), languageBoard.transform.position, 2, 1));
                        if (activeBoardStack.Count == 0)
                        {
                            blackBoard1.SetActive(false);
                        }
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
                        activeBoardStack.Pop();
                        StartCoroutine(UIMovement_Y(characterBoard, new Vector3(canvas.transform.position.x, -763f * canvas.transform.localScale.x, 0), characterBoard.transform.position, 2, 1));
                        if (activeBoardStack.Count == 0)
                        {
                            blackBoard1.SetActive(false);
                        }
                        break;
                    default:
                        Debug.Log(clickObject.transform.position);
                        choiceOutline.transform.position = clickObject.transform.position;
                        break;
                }
            }
            else if (clickObject.tag == "Store")
            {
                switch (clickObject.name)
                {
                    default:
                        break;
                }
            }
        }
    }

    public void SetLevelPage(int temp)
    {
        if (temp == pages.Length) pageCount %= pages.Length;
        else if (temp < 0) pageCount = pages.Length - 1;
        
        GameManager.instance.menuCount = pageCount;
        if (StageManager.instance.GetStageClearCheckValue(pageCount * 12) == 0) pages[pageCount].GetComponent<ImageChange>().SpriteChange(false);
        else pages[pageCount].GetComponent<ImageChange>().SpriteChange(true);
        StageManager.instance.MenuStageSetUp();
    }

    public void OnCompletedPanelCall()
    {
        clearBoard.SetActive(true);
        blackBoard1.SetActive(true);
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

    private IEnumerator UIMovement_Y(GameObject target, Vector3 targetPos, Vector3 orgPos, int count, int mode)
    {
        //Set Origin Position
        target.transform.position = orgPos;
        Vector3 temp_pos = target.transform.position;

        //Set Direction
        float dir;
        if (targetPos.y < orgPos.y) dir = -1;
        else dir = 1;

        //Move to Target Position
        float x = 1, slope = 0.08f;
        yield return new WaitForSecondsRealtime(0.03f);
        while ((targetPos.y - target.transform.position.y) * dir > 0)
        {
            temp_pos.y = dir * slope * Mathf.Pow(x, 2) + orgPos.y;
            target.transform.position = temp_pos;
            x += 1f;
            yield return new WaitForSecondsRealtime(0.001f);
        }

        target.transform.position = targetPos;

        //Repeat for Vibrate
        if (count == 0) StartCoroutine(UIMovement_Y(target, new Vector3(target.transform.position.x, target.transform.position.y + 30f * -dir, 0f), target.transform.position, count + 1, mode));
        else if(count == 1) StartCoroutine(UIMovement_Y(target, new Vector3(target.transform.position.x, target.transform.position.y + 10f * -dir, 0f), target.transform.position, count + 1, mode));
        else if (count == 2 && mode == 1) { uiMovementCheck = false; }
    }

    private IEnumerator UIMovement_X(GameObject target, Vector3 targetPos, Vector3 orgPos, int count, int mode)
    {
        //Set Origin Position
        target.transform.position = orgPos;
        Vector3 temp_pos = target.transform.position;

        //Set Direction
        float dir;
        if (targetPos.x < orgPos.x) dir = -1;
        else dir = 1;

        //Move to Target Position
        float x = 1, slope = 0.1f;
        yield return new WaitForSecondsRealtime(0.03f);
        while ((targetPos.x - target.transform.position.x) * dir > 0)
        {
            temp_pos.x = dir * slope * Mathf.Pow(x, 2) + orgPos.x;
            target.transform.position = temp_pos;
            x += 1f;
            yield return new WaitForSecondsRealtime(0.001f);
        }

        target.transform.position = targetPos;

        //Repeat for Vibrate
        if (count == 0) StartCoroutine(UIMovement_X(target, new Vector3(targetPos.x + 30f * -dir, target.transform.position.y, 0f), target.transform.position, count + 1, mode));
        else if (count == 1) StartCoroutine(UIMovement_X(target, new Vector3(targetPos.x + 10f * -dir, target.transform.position.y, 0f), target.transform.position, count + 1, mode));
        else if (count == 2 && mode == 1) { uiMovementCheck = false; }
    }
}
