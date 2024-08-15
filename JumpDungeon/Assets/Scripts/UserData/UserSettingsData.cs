using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UserSettingsData : IUserData
{
    public float BGMValue { get; set; }
    public float SFXValue { get; set; }
    public int Language { get; set; }

    public void SetDefaultData()
    {
        Logger.Log($"{GetType()}::SetDefaultData");

        BGMValue = 1f;
        SFXValue = 1f;
        Language = 0;
    }
    public bool LoadData()
    {
        Logger.Log($"{GetType()}::LoadData");

        bool result = false;

        try
        {
            BGMValue = ES3.Load<float>("BGMValue");
            SFXValue = ES3.Load<float>("SFXValue");
            Language = ES3.Load<int>("Language");

            result = true;

            Logger.Log($"BGM:{BGMValue} / SFX:{SFXValue}");
            Logger.Log($"Language:{((LanguageType)Language).ToString()}");
        }
        catch (System.Exception e)
        {
            Logger.Log($"Load failed (" + e.Message + ")");
        }

        return result;
    }

    public bool SaveData()
    {
        Logger.Log($"{GetType()}::SaveData");

        bool result = false;

        try
        {
            ES3.Save("BGMValue", BGMValue);
            ES3.Save("SFXValue", SFXValue);
            ES3.Save("Language", Language);

            result = true;

            Logger.Log($"BGM:{BGMValue} / SFX:{SFXValue}");
            Logger.Log($"Language:{((LanguageType)Language).ToString()}");
        }
        catch (System.Exception e)
        {
            Logger.Log($"Save failed (" + e.Message + ")");
        }

        return result;
    }
}
