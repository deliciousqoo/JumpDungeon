using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class UserStagesProgressData
{
    public int ChapterNum;
    public int StageNum;
    public int StarAmount;
    public float ClearTime;

    public UserStagesProgressData(int chapterNum, int stageNum, int starAmount, float clearTime)
    {
        ChapterNum = chapterNum;
        StageNum = stageNum;
        StarAmount = starAmount;
        ClearTime = clearTime;
    }
}

[Serializable]
public class UserStagesProgressDataListWrapper
{
    public List<UserStagesProgressData> StagesProgressDataList;
}

public class UserStagesData : IUserData
{
    public List<UserStagesProgressData> StagesProgressDataList { get; set; } = new List<UserStagesProgressData>();

    public void SetDefaultData()
    {
        Logger.Log($"{GetType()}::SetDefaultData");

        StagesProgressDataList.Add(new UserStagesProgressData(1, 1, 0, 0f));
    }

    public bool LoadData()
    {
        Logger.Log($"{GetType()}::LoadData");

        bool result = false;

        try
        {
            string stagesProgressDataListJson = PlayerPrefs.GetString("StagesProgressDataList");
            if(string.IsNullOrEmpty(stagesProgressDataListJson))
            {
                UserStagesProgressDataListWrapper stagesProgressListWrapper = JsonUtility.FromJson<UserStagesProgressDataListWrapper>(stagesProgressDataListJson);
                StagesProgressDataList = stagesProgressListWrapper.StagesProgressDataList;

                Logger.Log("StagesProgressDataList");
                foreach (var item in StagesProgressDataList)
                {
                    Logger.Log($"Chapter,StageNum:{item.ChapterNum},{item.StageNum} / StartAmount:{item.StarAmount} / ClearTime:{item.ClearTime}");
                }

                result = true;
            }
        }
        catch (System.Exception e)
        {
            Logger.Log($"Load failed. (" + e.Message + ")");
        }

        return result;
    }

    public bool SaveData()
    {
        Logger.Log($"{GetType()}::SaveData");

        bool result = false;

        return result;
    }

    public void AddStageData()
    {

    }

    public void UpdateStageData()
    {

    }
}
