using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class UserGoodsData : IUserData
{
    public int Heart { get; set; }
    public int Gold { get; set; }
    public int Score { get; set; }

    public void SetDefaultData()
    {
        Logger.Log($"{GetType()}::SetDefaultData");

        Heart = 0;
        Gold = 0;
        Score = 0;   
    }
    public bool LoadData()
    {
        Logger.Log($"{GetType()}::LoadData");

        bool result = false;

        try
        {
            Heart = PlayerPrefs.GetInt("Gem");
            Gold = PlayerPrefs.GetInt("Gold");
            Score = PlayerPrefs.GetInt("Score");

            result = true;

            Logger.Log($"Heart:{Heart} / Gold:{Gold} / Score:{Score}");
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
            PlayerPrefs.SetInt("Heart", Heart);
            PlayerPrefs.SetInt("Gold", Gold);
            PlayerPrefs.SetInt("Score", Gold);
            PlayerPrefs.Save();

            result = true;

            Logger.Log($"Heart:{Heart} / Gold:{Gold} / Score:{Score}");
        }
        catch (System.Exception e)
        {
            Logger.Log($"Save failed. (" + e.Message + ")");
        }

        return result;
    }
}
