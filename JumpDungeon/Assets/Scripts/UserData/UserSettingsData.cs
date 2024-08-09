using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UserSettingsData : IUserData
{
    public float BGMValue { get; set; }
    public float SFXValue { get; set; }

    public void SetDefaultData()
    {
        Logger.Log($"{GetType()}::SetDefaultData");

        BGMValue = 1f;
        SFXValue = 1f;
    }
    public bool LoadData()
    {
        Logger.Log($"{GetType()}::LoadData");

        bool result = false;

        try
        {
            BGMValue = PlayerPrefs.GetFloat("MusicValue");
            SFXValue = PlayerPrefs.GetFloat("SoundValue");

            result = true;

            Logger.Log($"Music:{BGMValue} / Sound:{SFXValue}");
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
            PlayerPrefs.SetFloat("MusicValue", BGMValue);
            PlayerPrefs.SetFloat("SoundValue", SFXValue);
            PlayerPrefs.Save();

            result = true;

            Logger.Log($"Music:{BGMValue} / Sound:{SFXValue}");
        }
        catch (System.Exception e)
        {
            Logger.Log($"Save failed (" + e.Message + ")");
        }

        return result;
    }
}
