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

    private LanguageUIData _languageUIData;

    public override void SetInfo(BaseUIData uiData)
    {
        base.SetInfo(uiData);

        _languageUIData = uiData as LanguageUIData;

        LanguageBtns[(int)_languageUIData.LanguageType].Select();
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
                return;
            }

            userSettingsData.Language = int.Parse(clickObject.name) - 1;
            userSettingsData.SaveData();
        }
    }
}
