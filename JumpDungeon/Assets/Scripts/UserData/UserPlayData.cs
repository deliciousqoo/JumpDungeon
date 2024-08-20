using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserPlayData : IUserData
{
    public int MaxClearedChapter { get; set; }
    public int MaxClearedStage { get; set; }
    public int SelectedChapter { get; set; } = 1;

    public void SetDefaultData()
    {
        Logger.Log($"{GetType()}::SetDefaultData");

        //MaxClearedChapter = -1;
        //MaxClearedStage = 0;
        MaxClearedChapter = 0;
        MaxClearedStage = 2;
        SelectedChapter = 0;
    }
    public bool LoadData()
    {
        Logger.Log($"{GetType()}::LoadData");

        bool result = false;

        try
        {
            MaxClearedChapter = ES3.Load<int>("MaxClearedChapter");
            MaxClearedStage = ES3.Load<int>("MaxClearedStage");

            SelectedChapter = MaxClearedChapter + 1;

            Logger.Log($"MaxClearedChapter:{MaxClearedChapter} / MaxClearedStage:{MaxClearedStage}");
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
            //PlayerPrefs.SetInt("MaxClearedChapter", MaxClearedChapter);
            //PlayerPrefs.SetInt("MaxClearedStage", MaxClearedStage);
            //PlayerPrefs.Save();
            ES3.Save("MaxClearedChapter", MaxClearedChapter);
            ES3.Save("MaxClearedStage", MaxClearedStage);

            result = true;

            Logger.Log($"MaxClearedChapter:{MaxClearedChapter} / MaxClearedStage:{MaxClearedStage}");
        }
        catch (System.Exception e)
        {
            Logger.Log($"Save failed. (" + e.Message + ")");
        }

        return result;
    }
}
