using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class UserDataManager : SingletonBehaviour<UserDataManager>
{
    public bool ExistsSavedData { get; private set; }
    public List<IUserData> UserDataList { get; private set; } = new List<IUserData>();

    public string m_fileName { get; private set; } = "SaveData.txt";

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
        ES3.Save("UserID", LoginManager.Instance.UserId);

        for (int i = 0; i < UserDataList.Count; i++)
        {
            UserDataList[i].SetDefaultData();
        }
    }

    public void LoadUserData()
    {
        if (ES3.FileExists(m_fileName))
        {
            string userId = ES3.Load<string>("UserID");
            LoginManager.Instance.SetUserId(userId);

            for (int i = 0; i < UserDataList.Count; i++)
            {
                UserDataList[i].LoadData();
            }
        }
    }

    public void SaveUserData()
    {
        for (int i = 0; i < UserDataList.Count; i++)
        {
            bool isSaveSuccess = UserDataList[i].SaveData();
        }
    }

    public T GetUserData<T>() where T : class, IUserData
    {
        return UserDataList.OfType<T>().FirstOrDefault();
    }
}
