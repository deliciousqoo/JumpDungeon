using Gpm.Ui;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSkinItemData : InfiniteScrollData
{
    public int SkinId;
    public string SkinName;
    public bool isGetted;
}

public class CharacterSkinItem : InfiniteScrollItem
{
    public Image CharacterIcon;
    public Sprite UnknownSprite;

    private CharacterSkinItemData m_CharacterItemData;
    private int m_SkinId;
    private bool m_IsGetted;

    public override void UpdateData(InfiniteScrollData scrollData)
    {
        base.UpdateData(scrollData);

        m_CharacterItemData = scrollData as CharacterSkinItemData;
        if(m_CharacterItemData == null)
        {
            Logger.Log("CharacterItemData does not exist.");
            return;
        }

        m_SkinId = m_CharacterItemData.SkinId;
        m_IsGetted = m_CharacterItemData.isGetted;

        if(m_IsGetted)
        {
            var skin_num = m_SkinId % 1000;
            var skinIconSprite = Resources.LoadAll<Sprite>("Art/Player/thumbnail")[skin_num - 0];
            if (skinIconSprite != null)
            {
                CharacterIcon.sprite = skinIconSprite;
            }
        }
        else
        {
            CharacterIcon.sprite = UnknownSprite;
        }
    }
}
