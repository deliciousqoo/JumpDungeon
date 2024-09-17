using DG.Tweening;
using System;
using UnityEngine;

public class BaseUIData
{
    public Action OnShow;
    public Action OnClose;
    public Vector3 StartPos;
    public Vector3 EndPos;
    public bool ShowMotionCheck;
    public bool CloseMotionCheck;
    public bool IsHorizontal;
}
public class BaseUI : MonoBehaviour
{
    private Action _onShow;
    private Action _onClose;
    private Vector3 _startPos;
    private Vector3 _endPos;
    private bool _showMotionCheck;
    private bool _closeMotionCheck;
    private bool _isHorizontal;

    public virtual void Init(Transform parent)
    {
        Logger.Log($"{GetType()}::Init");

        _onShow = null;
        _onClose = null;

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

        _onShow = uiData.OnShow;
        _onClose = uiData.OnClose;
        _startPos = uiData.StartPos;
        _endPos = uiData.EndPos;
        _showMotionCheck = uiData.ShowMotionCheck;
        _closeMotionCheck = uiData.CloseMotionCheck;
        _isHorizontal = uiData.IsHorizontal;
    }

    public virtual void ShowUI()
    {
        _onShow?.Invoke();

        if(_showMotionCheck)
        {
            UIManager.Instance.CheckCanUIMove = false;
            gameObject.transform.localPosition = _startPos;

            Sequence sequence = DOTween.Sequence();
            Tween tr1 = gameObject.transform.DOLocalMove(new Vector3(0f, 0f, 0f), 0.3f).SetEase(Ease.InCubic);
            Tween tr2 = null;
            if (_isHorizontal) tr2 = gameObject.transform.DOPunchPosition(new Vector3(-20f, 0, 0), 0.5f, 10, 1, false);
            else tr2 = gameObject.transform.DOPunchPosition(new Vector3(0f, 20f, 0), 0.5f, 10, 1, false);
            sequence.Append(tr1).Append(tr2).OnComplete(() =>
            {
                UIManager.Instance.CheckCanUIMove = true;
            });
        }

        _onShow = null;
    }

    public virtual void CloseUI()
    {
        _onClose?.Invoke();
        _onClose = null;

        if (_closeMotionCheck)
        {
            UIManager.Instance.CheckCanUIMove = false;

            Sequence sequence = DOTween.Sequence();
            Tween tr1 = gameObject.transform.DOLocalMove(_endPos, 0.3f).SetEase(Ease.InCubic);
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
        if(UIManager.Instance.CheckCanUIMove) this.CloseUI();
    }
}
