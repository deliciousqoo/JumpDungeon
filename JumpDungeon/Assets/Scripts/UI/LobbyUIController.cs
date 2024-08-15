using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class LobbyUIController : MonoBehaviour
{
    public int m_SelectedChapter { get; set; }

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

        var uiData = new ChapterUIData();
        uiData.ChapterType = (ChapterType)userPlayData.SelectedChapter;
        uiData.ChapterNameTxt = ((ChapterType)userPlayData.SelectedChapter).ToString();
        LobbyManager.Instance.OpenChapterUI(uiData);

        m_SelectedChapter = (int)userPlayData.SelectedChapter;
    }

    public void OnClickChangeChapter()
    {
        if (EventSystem.current.currentSelectedGameObject != null)
        {
            GameObject clickObject = EventSystem.current.currentSelectedGameObject;

            LobbyManager.Instance.CloseChapterUI(m_SelectedChapter);

            switch(clickObject.name)
            {
                case "LeftChapterBtn":
                    m_SelectedChapter--;
                    break;
                case "RightChapterBtn":
                    m_SelectedChapter++;
                    break;
            }

            if (m_SelectedChapter == (int)ChapterType.COUNT) m_SelectedChapter = 0;
            else if (m_SelectedChapter < 0) m_SelectedChapter = (int)ChapterType.COUNT - 1;

            var uiData = new ChapterUIData();
            uiData.ChapterType = (ChapterType)m_SelectedChapter;
            uiData.ChapterNameTxt = ((ChapterType)m_SelectedChapter).ToString();
            LobbyManager.Instance.OpenChapterUI(uiData);
        }
    }

    public void OnClickSettingBtn()
    {
        Logger.Log($"{GetType()}::OnClickSettingBtn");

        var uiData = new SettingUIData();
        uiData.SettingType = SettingType.OUT_GAME;

        UIManager.Instance.OpenUI<SettingUI>(uiData);
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

    public void OnClickStoreBtn()
    {
        Logger.Log($"{GetType()}::OnClickStoreBtn");

        var uiData = new BaseUIData();
        //UIManager.Instance.OpenUI<ChargeUI>(uiData);
    }

    public void OnClickRankBtn()
    {
        Logger.Log($"{GetType()}::OnClickRankBtn");

        var uiData = new BaseUIData();
        //UIManager.Instance.OpenUI<ChargeUI>(uiData);
    }

    public void OnClickModeBtn()
    {
        Logger.Log($"{GetType()}::OnClickModeBtn");

        var uiData = new BaseUIData();
        //UIManager.Instance.OpenUI<ChargeUI>(uiData);
    }

    public void OnClickSkinBtn()
    {
        Logger.Log($"{GetType()}::OnClickSkinBtn");

        var uiData = new BaseUIData();
        //UIManager.Instance.OpenUI<ChargeUI>(uiData);
    }

    public void OnClickBoxBtn()
    {
        Logger.Log($"{GetType()}::OnClickBoxBtn");

        var uiData = new BaseUIData();
        //UIManager.Instance.OpenUI<ChargeUI>(uiData);
    }
}
