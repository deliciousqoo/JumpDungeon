using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
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
    LOCK
}

public class ChapterUIData : BaseUIData
{
    public ChapterType ChapterType;
    public string ChapterNameTxt;
}

public class ChapterUI : BaseUI
{
    public Transform ChapterCanvasTrs;

    public Image ChapterTitle;
    public TextMeshProUGUI ChapterNameTxt;
    public GameObject[] Stages = new GameObject[12];

    private ChapterUIData m_ChapterUIData;

    public override void Init(Transform parent)
    {
        parent = ChapterCanvasTrs;

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
        if((int)m_ChapterUIData.ChapterType > UserDataManager.Instance.GetUserData<UserPlayData>().MaxClearedChapter)
        {
            ChapterTitle.sprite = chapterTitleSprites[(int)ChapterType.LOCK];
        }
        else
        {
            if (chapterTitleSprites != null)
            {
                ChapterTitle.sprite = chapterTitleSprites[(int)m_ChapterUIData.ChapterType];
            }
        }
    }

    public void SetStageButton(int clearedStage)
    {

    }
}
