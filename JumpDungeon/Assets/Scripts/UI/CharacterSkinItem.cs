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

    private int _skinId;
    private bool _isGetted;

    public void SetSkinItem(CharacterSkinItemData skinData)
    {
        _skinId = skinData.SkinId;
        _isGetted = skinData.IsGetted;

        if (_isGetted)
        {
            var skin_num = _skinId % 1000;
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

        Logger.Log($"{_skinId}");

        var characterSkinUI = UIManager.Instance.GetActiveUI<CharacterSkinUI>();
        if (characterSkinUI == null)
        {
            Logger.LogError("CharacterSkinUI does not exist.");
            return;
        }

        var selectUpdateMsg = new SelectUpdateMsg();
        selectUpdateMsg.SkinId = _skinId;
        selectUpdateMsg.IsGetted = _isGetted;

        Messenger.Default.Publish(selectUpdateMsg);
    }
}
