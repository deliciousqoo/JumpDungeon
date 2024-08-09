using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageUIData : BaseUIData
{
    public int StartAmount;
}

public class StageUI : BaseUI
{
    public Image[] Stars = new Image[3];

    private StageUIData m_StageUIData;
    private int m_StartAmount;

    public override void SetInfo(BaseUIData uiData)
    {
        base.SetInfo(uiData);

        m_StageUIData = uiData as StageUIData;
        if(m_StageUIData == null)
        {
            Logger.LogError("StageUIData is invalid");
            return;
        }

        m_StartAmount = m_StageUIData.StartAmount;
        for (int i = 0; i < Stars.Length; i++)
        {
            Stars[i].gameObject.SetActive(i < m_StartAmount);
        }
    }

    public void OnClickAdBtn()
    {
        CloseUI();
    }

    public void OnClickStartBtn()
    {
        CloseUI();
    }
}
