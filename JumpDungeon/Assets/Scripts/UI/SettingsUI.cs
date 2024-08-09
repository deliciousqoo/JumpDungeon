using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum SettingType
{
    OUT_GAME,
    IN_GAME,
}

public class SettingsUIData : BaseUIData
{
    public SettingType SettingType;
}

public class SettingsUI : BaseUI
{
    public Slider BGMValue;
    public Slider SFXValue;

    public Button LanguageBtn;
    public Button ExitBtn;
    public Button RestartBtn;
    public Button ContinueBtn;

    private SettingsUIData m_SettingsData;

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

        m_SettingsData = uiData as SettingsUIData;

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
        //AudioManager Music Value Setting
    }

    public void SetSFXValue(float value)
    {
        //AudioManager Music Value Setting
    }

    public void OnClickLanguage()
    {

    }

    public void OnClickExit()
    {

    }

    public void OnClickRestart()
    {

    }

    public void OnClickContinue()
    {

    }
}

