using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class UserCharacterSkinProgressData
{
    public int SkinId;
    public string SkinName;

    public UserCharacterSkinProgressData(int skinId, string skinName)
    {
        SkinId = skinId;
        SkinName = skinName;
    }
}

[Serializable]
public class UserCharacterSkinProgressDataListWrapper
{
    public List<UserCharacterSkinProgressData> CharacterSkinProgressDataList;
}

public class UserCharacterSkinData : IUserData
{
    public int EquippedCharacterSkin { get; set; }

    public List<UserCharacterSkinProgressData> CharacterSkinProgressDataList { get; set; } = new List<UserCharacterSkinProgressData>();

    public void SetDefaultData()
    {
        Logger.Log($"{GetType()}::SetDefaultData");

        EquippedCharacterSkin = 1001;
        CharacterSkinProgressDataList.Add(new UserCharacterSkinProgressData(1001, "±âº»"));
    }
    public bool LoadData()
    {
        Logger.Log($"{GetType()}::LoadData");

        bool result = false;

        try
        {
            EquippedCharacterSkin = ES3.Load<int>("EquippedCharacterSkin");
            string characterSkinProgressDataListJson = ES3.Load<string>("CharacterSkinProgressDataList");
            if(!string.IsNullOrEmpty(characterSkinProgressDataListJson))
            {
                UserCharacterSkinProgressDataListWrapper characterSkinProgressDataListWrapper = JsonUtility.FromJson<UserCharacterSkinProgressDataListWrapper>(characterSkinProgressDataListJson);
                CharacterSkinProgressDataList = characterSkinProgressDataListWrapper.CharacterSkinProgressDataList;

                Logger.Log("CharacterSkinProgressDataList");
                foreach (var item in CharacterSkinProgressDataList)
                {
                    Logger.Log($"CharacterSkinName:{item.SkinName}");
                }
            }
            
            result = true;

            Logger.Log($"EquippedSkin:{EquippedCharacterSkin}");
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
            ES3.Save("EquippedCharacterSkin", EquippedCharacterSkin);
            UserCharacterSkinProgressDataListWrapper characterSkinProgressDataListWrapper = new UserCharacterSkinProgressDataListWrapper();
            characterSkinProgressDataListWrapper.CharacterSkinProgressDataList = CharacterSkinProgressDataList;
            string characterSkinProgressDataListJson = JsonUtility.ToJson(characterSkinProgressDataListWrapper);
            ES3.Save("CharacterSkinProgressDataList", characterSkinProgressDataListJson);

            foreach (var item in CharacterSkinProgressDataList)
            {
                Logger.Log($"CharacterSkinName:{item.SkinName}");
            }

            result = true;

            Logger.Log($"EquippedSkin:{EquippedCharacterSkin}");
        }
        catch (System.Exception e)
        {
            Logger.Log($"Load failed (" + e.Message + ")");
        }

        return true;
    }

    public UserCharacterSkinProgressData GetUserCharacterSkinProgressData(int skinId)
    {
        return CharacterSkinProgressDataList.Where(item => item.SkinId == skinId).FirstOrDefault();
    }

}
