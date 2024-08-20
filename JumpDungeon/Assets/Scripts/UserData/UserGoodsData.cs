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
            Heart = ES3.Load<int>("Gem");
            Gold = ES3.Load<int>("Gold");
            Score = ES3.Load<int>("Score");

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
            ES3.Save("Heart", Heart);
            ES3.Save("Gold", Gold);
            ES3.Save("Score", Score);

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
