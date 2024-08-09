using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class UserDataManager : SingletonBehaviour<UserDataManager>
{
    public bool ExistsSavedData { get; private set; }
    public List<IUserData> UserDataList { get; private set; } = new List<IUserData>();

    protected override void Init()
    {
        base.Init();

        UserDataList.Add(new UserGoodsData());
        UserDataList.Add(new UserPlayData());
        UserDataList.Add(new UserSettingsData());
        UserDataList.Add(new UserStagesData());
    }

    public void SetDefaultUserData()
    {
        for (int i = 0; i < UserDataList.Count; i++)
        {
            UserDataList[i].SetDefaultData();
        }
    }

    public void LoadUserData()
    {
        ExistsSavedData = PlayerPrefs.GetInt("ExistSavedData") == 1 ? true : false;
    
        if(ExistsSavedData)
        {
            for (int i = 0; i < UserDataList.Count; i++)
            {
                UserDataList[i].LoadData();
            }
        }
    }

    public void SaveUserData()
    {
        bool errorCheck = false;

        for (int i = 0; i < UserDataList.Count; i++)
        {
            bool isSaveSuccess = UserDataList[i].SaveData();
            if (!isSaveSuccess)
            {
                errorCheck = true;
            }
        }

        if(!errorCheck)
        {
            ExistsSavedData = true;
            PlayerPrefs.SetInt("ExistSavedData", 1);
            PlayerPrefs.Save();
        }
    }

    public T GetUserData<T>() where T : class, IUserData
    {
        return UserDataList.OfType<T>().FirstOrDefault();
    }
}
