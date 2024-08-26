using Gpm.Ui;
using SuperMaxim.Messaging;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSkinItemData
{
    public bool IsGetted;
    public int SkinId;
}

public class CharacterSkinItem : MonoBehaviour
{
    public Image CharacterIcon;
    public Sprite UnknownSprite;
    public Sprite[] SelectPointSprites = new Sprite[2];

    private int m_SkinId;
    private bool m_IsGetted;

    public void SetSkinItem(CharacterSkinItemData skinData)
    {
        m_SkinId = skinData.SkinId;
        m_IsGetted = skinData.IsGetted;

        if (m_IsGetted)
        {
            var skin_num = m_SkinId % 1000;
            var skinIconSprite = Resources.LoadAll<Sprite>("Art/Player/thumbnail")[skin_num - 1];
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

    public void OnClickCharacterSkinBtn()
    {
        if (!UIManager.Instance.CheckCanUIMove) return;

        Logger.Log($"{m_SkinId}");

        var characterSkinUI = UIManager.Instance.GetActiveUI<CharacterSkinUI>();
        if (characterSkinUI == null)
        {
            Logger.LogError("CharacterSkinUI does not exist.");
            return;
        }

        var selectUpdateMsg = new SelectUpdateMsg();
        selectUpdateMsg.SkinId = m_SkinId;
        selectUpdateMsg.IsGetted = m_IsGetted;

        Messenger.Default.Publish(selectUpdateMsg);
    }
}
