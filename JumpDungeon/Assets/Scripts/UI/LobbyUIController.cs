using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyUIController : MonoBehaviour
{
    public void Init()
    {
        SetCurrChapter();
    }

    public void SetCurrChapter()
    {
        var userPlayData = UserDataManager.Instance.GetUserData<UserPlayData>();
        if(userPlayData == null)
        {
            Logger.LogError("UserPlayData does not exist.");
            return;
        }
    }

    public void OnClickSettingBtn()
    {
        Logger.Log($"{GetType()}::OnClickSettingBtn");

        var uiData = new BaseUIData();
        //UIManager.Instance.OpenUI<SettingUI>(uiData);
    }

    public void OnClickMissionBtn()
    {
        Logger.Log($"{GetType()}::OnClickMissionBtn");

        var uiData = new BaseUIData();
        //UIManager.Instance.OpenUI<MissionUI>(uiData);
    }

    public void OnClickChargeBtn()
    {
        Logger.Log($"{GetType()}::OnClickChargeBtn");

        var uiData = new BaseUIData();
        //UIManager.Instance.OpenUI<ChargeUI>(uiData);
    }
}
