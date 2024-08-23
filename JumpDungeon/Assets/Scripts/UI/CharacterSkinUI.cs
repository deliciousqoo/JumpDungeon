using Gpm.Ui;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CharacterSkinGrade
{
    RARE = 1,
    UNIQUE,
}

public class CharacterSkinUIData : BaseUIData
{

}

public class CharacterSkinUI : BaseUI
{
    public InfiniteScroll CharacterScrollList;

    public NumPrint HaveCount;
    public NumPrint TotalCount;

    private int m_HaveCount;

    public override void SetInfo(BaseUIData uiData)
    {
        base.SetInfo(uiData);

        SetCharacterSkinList();

        HaveCount.printNum(m_HaveCount);
        TotalCount.printNum(CharacterScrollList.GetDataCount());
    }

    private void SetCharacterSkinList()
    {
        CharacterScrollList.Clear();
        m_HaveCount = 0;

        var characterSkinDataList = DataTableManager.Instance.GetCharacterSkinList();
        if(characterSkinDataList == null)
        {
            Logger.LogError("CharacterSkinDataList does not exist.");
            return;
        }

        var userCharacterSkinData = UserDataManager.Instance.GetUserData<UserCharacterSkinData>();
        if(userCharacterSkinData == null)
        {
            Logger.LogError("UserCharacterSkinData does not exist.");
            return;
        }

        foreach (var item in characterSkinDataList)
        {
            var characterSkinData = new CharacterSkinItemData();

            characterSkinData.SkinId = item.SkinId;
            characterSkinData.SkinName = item.SkinName;
            characterSkinData.isGetted = userCharacterSkinData.GetUserCharacterSkinProgressData(characterSkinData.SkinId) == null ? false : true;
            if (characterSkinData.isGetted) m_HaveCount++;

            CharacterScrollList.InsertData(characterSkinData);
        }
    }
}
