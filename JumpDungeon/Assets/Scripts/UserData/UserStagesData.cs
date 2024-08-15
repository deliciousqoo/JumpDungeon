using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


[Serializable]
public class UserStagesProgressData
{
    public string StageID;
    /* 
     * StageID Rule : UserID + GameMode(Normal:0, Hard:1) + ChapterNum + StageNum
     * ex) asdfdsdafweiourq_0_1_1
    */
    public UserStageClearInfo ClearInfo;

    public UserStagesProgressData(string stageID, UserStageClearInfo clearInfo)
    {
        StageID = stageID;
        ClearInfo = clearInfo;
    }
}

[Serializable]
public class UserStagesProgressDataListWrapper
{
    public List<UserStagesProgressData> StagesProgressDataList = new List<UserStagesProgressData>();
}

[Serializable]
public class UserStageClearInfo
{
    public int StarAmount;
    public float ClearTime;

    public UserStageClearInfo(int starAmount, float clearTime)
    {
        StarAmount = starAmount;
        ClearTime = clearTime;
    }
}

public class UserStagesData : IUserData
{
    public List<UserStagesProgressData> StagesProgressDataList { get; set; } = new List<UserStagesProgressData>();
    public Dictionary<string, UserStageClearInfo> UserStageClearDataDic { get; set; } = new Dictionary<string, UserStageClearInfo>();

    public void SetDefaultData()
    {
        Logger.Log($"{GetType()}::SetDefaultData");

        StagesProgressDataList.Add(new UserStagesProgressData(LoginManager.Instance.UserId + "_0_0_1", new UserStageClearInfo(2, 0f)));
        StagesProgressDataList.Add(new UserStagesProgressData(LoginManager.Instance.UserId + "_0_0_2", new UserStageClearInfo(1, 0f)));
        StagesProgressDataList.Add(new UserStagesProgressData(LoginManager.Instance.UserId + "_0_0_3", new UserStageClearInfo(3, 0f)));
        StagesProgressDataList.Add(new UserStagesProgressData(LoginManager.Instance.UserId + "_0_0_4", new UserStageClearInfo(3, 0f)));
        StagesProgressDataList.Add(new UserStagesProgressData(LoginManager.Instance.UserId + "_0_0_5", new UserStageClearInfo(3, 0f)));
        StagesProgressDataList.Add(new UserStagesProgressData(LoginManager.Instance.UserId + "_0_0_6", new UserStageClearInfo(3, 0f)));
        StagesProgressDataList.Add(new UserStagesProgressData(LoginManager.Instance.UserId + "_0_0_7", new UserStageClearInfo(3, 0f)));
        StagesProgressDataList.Add(new UserStagesProgressData(LoginManager.Instance.UserId + "_0_0_8", new UserStageClearInfo(3, 0f)));
        StagesProgressDataList.Add(new UserStagesProgressData(LoginManager.Instance.UserId + "_0_0_9", new UserStageClearInfo(3, 0f)));
        StagesProgressDataList.Add(new UserStagesProgressData(LoginManager.Instance.UserId + "_0_0_10", new UserStageClearInfo(3, 0f)));
        StagesProgressDataList.Add(new UserStagesProgressData(LoginManager.Instance.UserId + "_0_0_11", new UserStageClearInfo(3, 0f)));
        StagesProgressDataList.Add(new UserStagesProgressData(LoginManager.Instance.UserId + "_0_0_12", new UserStageClearInfo(3, 0f)));
        StagesProgressDataList.Add(new UserStagesProgressData(LoginManager.Instance.UserId + "_0_1_1", new UserStageClearInfo(3, 0f)));
        StagesProgressDataList.Add(new UserStagesProgressData(LoginManager.Instance.UserId + "_0_1_2", new UserStageClearInfo(3, 0f)));

        foreach (var item in StagesProgressDataList)
        {
            UserStageClearDataDic.Add(item.StageID, item.ClearInfo);
        }
    }

    public bool LoadData()
    {
        Logger.Log($"{GetType()}::LoadData");

        bool result = false;

        try
        {
            string stagesProgressDataListJson = 
                ES3.Load<string>("StagesProgressList");
            if(!string.IsNullOrEmpty(stagesProgressDataListJson))
            {
                UserStagesProgressDataListWrapper stagesProgressListWrapper = JsonUtility.FromJson<UserStagesProgressDataListWrapper>(stagesProgressDataListJson);
                StagesProgressDataList = stagesProgressListWrapper.StagesProgressDataList;

                Logger.Log("StagesProgressDataList");
                foreach (var item in StagesProgressDataList)
                {
                    Logger.Log($"StageID:{item.StageID} / StartAmount:{item.ClearInfo.StarAmount} / ClearTime:{item.ClearInfo.ClearTime}");

                    UserStageClearDataDic.Add(item.StageID, item.ClearInfo);
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

        try
        {
            UserStagesProgressDataListWrapper stagesProgressListWrapper = new UserStagesProgressDataListWrapper();
            foreach (var item in UserStageClearDataDic)
            {
                stagesProgressListWrapper.StagesProgressDataList.Add(new UserStagesProgressData(item.Key, item.Value));
            }
            string stagesProgressListJson = JsonUtility.ToJson(stagesProgressListWrapper);
            ES3.Save("StagesProgressList", stagesProgressListJson);

            result = true;
        }
        catch (System.Exception e)
        {
            Logger.Log("Load failed (" + e.Message + ")");
        }

        return result;
    }

    public void AddStageData(int mode, int chapterNum, int stageNum, UserStageClearInfo userStageClearData)
    {
        UserStageClearDataDic.Add(LoginManager.Instance.UserId + "_" + mode + "_" + chapterNum + "_" + stageNum, userStageClearData);
    }

    public void UpdateStageData(int mode, int chapterNum, int stageNum, UserStageClearInfo userStageClearData)
    {
        UserStageClearDataDic[LoginManager.Instance.UserId + "_" + mode + "_" + chapterNum + "_" + stageNum].StarAmount = userStageClearData.StarAmount;
        UserStageClearDataDic[LoginManager.Instance.UserId + "_" + mode + "_" + chapterNum + "_" + stageNum].ClearTime = userStageClearData.ClearTime;
    }
}
