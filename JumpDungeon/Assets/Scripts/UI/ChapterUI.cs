using MoreMountains.Tools;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public enum ChapterType
{
    GROUND,
    FROZEN,
    SAND,
    LAVA,
    SWAMP,
    WATER,
    CLOUD,
    COUNT
}

public class ChapterUIData : BaseUIData
{
    public ChapterType ChapterType;
    public string ChapterNameTxt;
}

public class ChapterUI : BaseUI
{
    //public Transform ChapterCanvasTrs;

    public Image ChapterTitle;
    public TextMeshProUGUI ChapterNameTxt;
    public GameObject[] Stages = new GameObject[12];

    public GameObject StageStar;

    private ChapterUIData m_ChapterUIData;

    public override void Init(Transform parent)
    {
        //parent = GameObject.Find("ChapterTrs").GetComponent<Transform>();

        base.Init(parent);
    }

    public override void SetInfo(BaseUIData uiData)
    {
        base.SetInfo(uiData);

        m_ChapterUIData = uiData as ChapterUIData;
        if (m_ChapterUIData == null)
        {
            Logger.LogError("ChapterUIData is invalid");
            return;
        }

        ChapterNameTxt.text = m_ChapterUIData.ChapterNameTxt;
        var chapterTitleSprites = Resources.LoadAll<Sprite>("Art/UI/ChapterUI");
        if((int)m_ChapterUIData.ChapterType > UserDataManager.Instance.GetUserData<UserPlayData>().MaxClearedChapter + 1)
        {
            ChapterTitle.sprite = chapterTitleSprites[(int)ChapterType.COUNT];
        }
        else
        {
            if (chapterTitleSprites != null)
            {
                ChapterTitle.sprite = chapterTitleSprites[(int)m_ChapterUIData.ChapterType];
            }
        }

        SetStageButton(chapterTitleSprites);
        SetStageStars();
    }

    public void SetStageButton(Sprite[] stageImges)
    {
        var userPlayData = UserDataManager.Instance.GetUserData<UserPlayData>();
        if(userPlayData == null)
        {
            Logger.LogError("UserPlayData does not exist.");
        }

        

        foreach (var item in Stages)
        {
            var stageNums = item.GetComponentsInChildren<Image>();
            if ((int)m_ChapterUIData.ChapterType <= userPlayData.MaxClearedChapter 
                || ((int)m_ChapterUIData.ChapterType == userPlayData.MaxClearedChapter + 1 
                    && int.Parse(item.name) <= userPlayData.MaxClearedStage + 1 ))
            {
                item.GetComponent<Image>().sprite = stageImges[(int)m_ChapterUIData.ChapterType + 8];
                item.GetComponent<Button>().interactable = true;
                for (int i = 1; i < stageNums.Length; i++)
                {
                    stageNums[i].gameObject.SetActive(true);
                }
            }
            else
            {
                item.GetComponent<Image>().sprite = stageImges[(int)ChapterType.COUNT + 8];
                item.GetComponent<Button>().interactable = false;
                for (int i = 1; i < stageNums.Length; i++)
                {
                    stageNums[i].gameObject.SetActive(false);
                }
            }
        }
    }

    public void SetStageStars()
    {
        var userStagesData = UserDataManager.Instance.GetUserData<UserStagesData>();
        if(userStagesData == null)
        {
            Logger.LogError("UserStagesData does not exist.");
        }

        var stagesClearInfo = userStagesData.UserStageClearDataDic;
        foreach (var item in stagesClearInfo)
        {
            var stageInfo = item.Key.Split('_');
            if (int.Parse(stageInfo[2]) == (int)m_ChapterUIData.ChapterType)
            {
                for (int i = 0; i < item.Value.StarAmount; i++)
                {
                    GameObject newStar = Instantiate(StageStar, Stages[int.Parse(stageInfo[3]) - 1].transform);
                    newStar.transform.localPosition = new Vector3(-60f + (60f*i), -65f, 0f);
                }
            }
        }
    }

    public void OnClickStageBtn()
    {
        if (EventSystem.current.currentSelectedGameObject != null)
        {
            GameObject clickObject = EventSystem.current.currentSelectedGameObject;

            var userStagesData = UserDataManager.Instance.GetUserData<UserStagesData>();
            if (userStagesData == null)
            {
                Logger.LogError("UserStagesData does not exist.");
            }

            var stagesClearInfo = userStagesData.UserStageClearDataDic;
            string keyValue = LoginManager.Instance.UserId + "_" + "0" + "_" + (int)m_ChapterUIData.ChapterType + "_" + clickObject.name;

            var uiData = new StageUIData();
            uiData.StartAmount = stagesClearInfo[keyValue].StarAmount;
            uiData.ChapterNum = (int)m_ChapterUIData.ChapterType;
            uiData.StageNum = int.Parse(clickObject.name);

            UIManager.Instance.OpenUI<StageUI>(uiData);
        }

        
    }
}
