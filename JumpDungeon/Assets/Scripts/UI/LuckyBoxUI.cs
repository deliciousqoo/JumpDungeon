using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LuckyBoxUIData : BaseUIData
{

}

public class LuckyBoxUI : BaseUI
{
    private LuckyBoxUIData m_LuckyBoxUIData;

    public override void SetInfo(BaseUIData uiData)
    {
        base.SetInfo(uiData);

        SetLuckyBoxList();
    }

    private void SetLuckyBoxList()
    {

    }

    public void OnClickAdBtn()
    {

    }

    public void OnClickChoiceBtn()
    {

    }
}
