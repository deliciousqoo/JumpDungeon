using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public enum LanguageType
{
    ENGLISH,
    KOREAN,
    JAPANESE,
    FRENCH,
    SPANISH,
    HINDI,
    ARABIC,
    CHINESE,
}

public class LanguageUIData : BaseUIData
{
    public LanguageType LanguageType;
}

public class LanguageUI : BaseUI
{
    public Button[] LanguageBtns = new Button[8];

    private LanguageUIData m_LanguageUIData;

    public override void SetInfo(BaseUIData uiData)
    {
        base.SetInfo(uiData);

        m_LanguageUIData = uiData as LanguageUIData;

        LanguageBtns[(int)m_LanguageUIData.LanguageType].Select();
    }

    public void OnClickLanguageBtn()
    {
        if (EventSystem.current.currentSelectedGameObject != null)
        {
            GameObject clickObject = EventSystem.current.currentSelectedGameObject;

            var userSettingsData = UserDataManager.Instance.GetUserData<UserSettingsData>();
            if (userSettingsData == null)
            {
                Logger.LogError("UserSettingsData does not exist.");
            }

            userSettingsData.Language = int.Parse(clickObject.name) - 1;
            userSettingsData.SaveData();
        }
    }
}
