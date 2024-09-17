using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class LuckyBoxResultUIData : BaseUIData
{
    public Sprite LuckyBoxSprite;
}

public class LuckyBoxResultUI : BaseUI
{
    public Image LuckyBoxItem;

    private LuckyBoxResultUIData _luckyBoxItemData;

    public override void SetInfo(BaseUIData uiData)
    {
        base.SetInfo(uiData);

        _luckyBoxItemData = uiData as LuckyBoxResultUIData;

        LuckyBoxItem.sprite = _luckyBoxItemData.LuckyBoxSprite;
        gameObject.transform.localScale = new Vector3(0f, 0f, 0f);
        gameObject.transform.DOScale(new Vector3(1f, 1f, 1f), 0.4f).OnComplete(() =>
        {
            UIManager.Instance.CheckCanUIMove = true;
        });
    }
}
