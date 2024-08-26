using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum SettingType
{
    OUT_GAME,
    IN_GAME,
}

public class SettingUIData : BaseUIData
{
    public SettingType SettingType;
}

public class SettingUI : BaseUI
{
    public Slider BGMValue;
    public Slider SFXValue;

    public Button LanguageBtn;
    public Button ExitBtn;
    public Button RestartBtn;
    public Button ContinueBtn;

    private SettingUIData m_SettingsData;

    public override void SetInfo(BaseUIData uiData)
    {
        var userSettingsData = UserDataManager.Instance.GetUserData<UserSettingsData>();
        if (userSettingsData != null)
        {
            SetAudioSetting(userSettingsData.BGMValue, userSettingsData.SFXValue);
        }

        uiData.OnClose = () =>
        {
            if(userSettingsData != null)
            {
                userSettingsData.BGMValue = BGMValue.value;
                userSettingsData.SFXValue = SFXValue.value;
                userSettingsData.SaveData();
            }
        };

        base.SetInfo(uiData);

        m_SettingsData = uiData as SettingUIData;

        LanguageBtn.gameObject.SetActive(m_SettingsData.SettingType == SettingType.OUT_GAME);
        ExitBtn.gameObject.SetActive(m_SettingsData.SettingType == SettingType.IN_GAME);
        RestartBtn.gameObject.SetActive(m_SettingsData.SettingType == SettingType.IN_GAME);
        ContinueBtn.gameObject.SetActive(m_SettingsData.SettingType == SettingType.IN_GAME);
    }

    public void SetAudioSetting(float bgm, float sfx)
    {
        BGMValue.value = bgm;
        SFXValue.value = sfx;
    }

    public void SetBGMValue(float value)
    {
        AudioManager.Instance.SetBGMValue(value);
    }

    public void SetSFXValue(float value)
    {
        AudioManager.Instance.SetSFXValue(value);
    }

    public void OnClickLanguage()
    {
        var userSettingData = UserDataManager.Instance.GetUserData<UserSettingsData>();
        if(userSettingData == null)
        {
            Logger.LogError("UserSettingsData does not exist");
            return;
        }

        var uiData = new LanguageUIData();
        uiData.LanguageType = (LanguageType)userSettingData.Language;

        UIManager.Instance.OpenUI<LanguageUI>(uiData);
    }

    public void OnClickExit()
    {
        UIManager.Instance.Fade(false, true, () =>
        {
            SceneLoader.Instance.LoadScene(SceneType.Lobby);
        });
    }

    public void OnClickRestart()
    {
        UIManager.Instance.Fade(false, true, () =>
        {
            SceneLoader.Instance.ReloadScene();
            UIManager.Instance.CloseCurrentFrontUI();
        });
    }

    public void OnClickContinue()
    {

    }
}

