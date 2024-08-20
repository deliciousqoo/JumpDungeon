using DG.Tweening;
using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseUIData
{
    public Action OnShow;
    public Action OnClose;
    public Vector3 StartPos;
    public Vector3 EndPos;
    public bool ShowMotionCheck;
    public bool CloseMotionCheck;
}
public class BaseUI : MonoBehaviour
{
    private Action m_OnShow;
    private Action m_OnClose;
    private Vector3 m_StartPos;
    private Vector3 m_EndPos;
    private bool m_ShowMotionCheck;
    private bool m_CloseMotionCheck;

    public virtual void Init(Transform parent)
    {
        Logger.Log($"{GetType()}::Init");

        m_OnShow = null;
        m_OnClose = null;

        transform.SetParent(parent);

        var rectTransform = GetComponent<RectTransform>();
        rectTransform.localPosition = Vector3.zero;
        rectTransform.localScale = new Vector3(1f, 1f, 1f);
        rectTransform.offsetMin = Vector2.zero;
        rectTransform.offsetMax = Vector2.zero;
    }

    public virtual void SetInfo(BaseUIData uiData)
    {
        Logger.Log($"{GetType()}::SetInfo");

        m_OnShow = uiData.OnShow;
        m_OnClose = uiData.OnClose;
        m_StartPos = uiData.StartPos;
        m_EndPos = uiData.EndPos;
        m_ShowMotionCheck = uiData.ShowMotionCheck;
        m_CloseMotionCheck = uiData.CloseMotionCheck;
    }

    public virtual void ShowUI()
    {
        m_OnShow?.Invoke();

        if(m_ShowMotionCheck)
        {
            UIManager.Instance.CheckCanUIMove = false;
            gameObject.transform.localPosition = m_StartPos;

            Sequence sequence = DOTween.Sequence();
            Tween tr1 = gameObject.transform.DOLocalMove(new Vector3(0f, 0f, 0f), 0.3f).SetEase(Ease.InCubic);
            Tween tr2 = gameObject.transform.DOPunchPosition(new Vector3(-20f, 0, 0), 0.5f, 10, 1, false);
            sequence.Append(tr1).Append(tr2).OnComplete(() =>
            {
                UIManager.Instance.CheckCanUIMove = true;
            });
        }

        m_OnShow = null;
    }

    public virtual void CloseUI()
    {
        m_OnClose?.Invoke();
        m_OnClose = null;

        if (m_CloseMotionCheck)
        {
            UIManager.Instance.CheckCanUIMove = false;

            Sequence sequence = DOTween.Sequence();
            Tween tr1 = gameObject.transform.DOLocalMove(m_EndPos, 0.3f).SetEase(Ease.InCubic);
            sequence.Append(tr1).OnComplete(() =>
            {
                UIManager.Instance.CheckCanUIMove = true;
                UIManager.Instance.CloseUI(this);
            });
        }
        else
        {
            UIManager.Instance.CloseUI(this);
        }
    }

    public virtual void OnClickCloseButton()
    {
        this.CloseUI();
    }
}
