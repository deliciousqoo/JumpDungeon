using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StageUIData : BaseUIData
{
    public int StartAmount;
    public int ChapterNum;
    public int StageNum;
    public float ClearTime;
}

public class StageUI : BaseUI
{
    public Image[] Stars = new Image[3];
    public TextMeshProUGUI StageInfoTxt;
    public TextMeshProUGUI ClearTime;

    private StageUIData m_StageUIData;

    public override void SetInfo(BaseUIData uiData)
    {
        base.SetInfo(uiData);

        m_StageUIData = uiData as StageUIData;
        if(m_StageUIData == null)
        {
            Logger.LogError("StageUIData is invalid");
            return;
        }

        for (int i = 0; i < Stars.Length; i++)
        {
            Stars[i].gameObject.SetActive(i < m_StageUIData.StartAmount);
        }

        StageInfoTxt.text = $"{((ChapterType)m_StageUIData.ChapterNum).ToString()} - {m_StageUIData.StageNum}";
        float clearTime = m_StageUIData.ClearTime;
        ClearTime.text = $"{(int)clearTime/3600:D2}:{(int)clearTime%3600/60:D2}:{clearTime%60:00.00}";
    }

    public void OnClickAdBtn()
    {
        UIManager.Instance.Fade(false, true, () =>
        {
            SceneLoader.Instance.LoadScene(SceneType.MainGame);
        });

        CloseUI();
    }

    public void OnClickStartBtn()
    {
        UIManager.Instance.Fade(false, false, () =>
        {
            SceneLoader.Instance.LoadScene(SceneType.MainGame);
            UIManager.Instance.Fade(true, true);
        });

        CloseUI();
    }
}
