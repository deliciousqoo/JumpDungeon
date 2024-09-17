using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum ConfirmType
{
    OK,
    OK_CANCEL,
}
public class ConfirmUIData : BaseUIData
{
    public ConfirmType ConfirmType;
    public string ConfirmTxt;
    public string OKBtnTxt;
    public Action OnClickOKBtn;
    public string CancelBtnTxt;
    public Action OnClickCancelBtn;
}

public class ConfirmUI : BaseUI
{
    public TextMeshProUGUI ConfirmTxt;
    public Button OKBtn;
    public Button CancelBtn;
    public TextMeshProUGUI OKBtnTxt;
    public TextMeshProUGUI CancelBtnTxt;

    private ConfirmUIData _confirmUIData;
    private Action _onClickOKBtn;
    private Action _onClickCancelBtn;

    public override void SetInfo(BaseUIData uiData)
    {
        base.SetInfo(uiData);

        _confirmUIData = uiData as ConfirmUIData;
        if (_confirmUIData == null)
        {
            Logger.LogError("ConfirmUIData is invalid");
            return;
        }

        ConfirmTxt.text = _confirmUIData.ConfirmTxt;
        OKBtnTxt.text = _confirmUIData.OKBtnTxt;
        CancelBtnTxt.text = _confirmUIData.CancelBtnTxt;
        _onClickOKBtn = _confirmUIData.OnClickOKBtn;
        _onClickCancelBtn = _confirmUIData.OnClickCancelBtn;

        OKBtn.gameObject.SetActive(true);
        CancelBtn.gameObject.SetActive(_confirmUIData.ConfirmType == ConfirmType.OK_CANCEL);
    }

    public void OnClickOKBtn()
    {
        _onClickOKBtn?.Invoke();
        _onClickOKBtn = null;
        CloseUI();
    }

    public void OnClickCancelBtn()
    {
        _onClickCancelBtn?.Invoke();
        _onClickCancelBtn = null;
        CloseUI();
    }
}
