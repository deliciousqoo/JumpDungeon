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

    private StageUIData _stageUIData;

    public override void SetInfo(BaseUIData uiData)
    {
        base.SetInfo(uiData);

        _stageUIData = uiData as StageUIData;
        if(_stageUIData == null)
        {
            Logger.LogError("StageUIData is invalid");
            return;
        }

        for (int i = 0; i < Stars.Length; i++)
        {
            Stars[i].gameObject.SetActive(i < _stageUIData.StartAmount);
        }

        StageInfoTxt.text = $"{((ChapterType)_stageUIData.ChapterNum).ToString()} - {_stageUIData.StageNum}";
        float clearTime = _stageUIData.ClearTime;
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
        var userPlayData = UserDataManager.Instance.GetUserData<UserPlayData>();
        if (userPlayData == null)
        {
            Logger.LogError("UserPlayData does not exist.");
            return;
        }

        userPlayData.SelectedChapter = _stageUIData.ChapterNum;
        userPlayData.SelectedStage = _stageUIData.StageNum;
        userPlayData.SaveData();

        UIManager.Instance.Fade(false, false, () =>
        {
            SceneLoader.Instance.LoadScene(SceneType.MainGame);
            UIManager.Instance.Fade(true, true);
        });

        CloseUI();
    }
}
