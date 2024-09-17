using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UIManager : SingletonBehaviour<UIManager>
{
    #region BASE_UI
    public Transform UICanvasTrs;
    public Transform ClosedTrs;

    public Image FadeImg;
    public Button BackgroundButton;

    private BaseUI _frontUI;
    private Dictionary<System.Type, GameObject> _openUIPool = new Dictionary<System.Type, GameObject>();
    private Dictionary<System.Type, GameObject> _closedUIPool = new Dictionary<System.Type, GameObject>();

    public bool CheckCanUIMove { get; set; } = true;

    private BaseUI GetUI<T>(out bool isAlreadyOpen)
    {
        var uiType = typeof(T);

        Logger.Log($"{GetType()}::GetUI({uiType})");

        BaseUI ui = null;
        isAlreadyOpen = false;

        if(_openUIPool.ContainsKey(uiType))
        {
            ui = _openUIPool[uiType].GetComponent<BaseUI>();
            isAlreadyOpen = true;
        }
        else if (_closedUIPool.ContainsKey(uiType))
        {
            ui = _closedUIPool[uiType].GetComponent<BaseUI>();
            _closedUIPool.Remove(uiType);
        }
        else
        {
            var uiObj = Instantiate(Resources.Load($"Prefabs/UI/{uiType}", typeof(GameObject))) as GameObject;
            ui = uiObj.GetComponent<BaseUI>();
        }

        return ui;
    }

    public void OpenUI<T>(BaseUIData uiData)
    {
        var uiType = typeof(T);

        Logger.Log($"{GetType()}::OpenUI({uiType})");

        bool isAlreadyOpen;
        var ui = GetUI<T>(out isAlreadyOpen);

        if(!ui)
        {
            Logger.LogError($"{uiType} does not exist.");
            return;
        }

        if(isAlreadyOpen)
        {
            Logger.LogError($"{uiType} is already open.");
            return;
        }

        var siblingIndex = UICanvasTrs.childCount - 1;
        ui.Init(UICanvasTrs);
        ui.transform.SetSiblingIndex(siblingIndex);
        ui.gameObject.SetActive(true);
        ui.SetInfo(uiData);
        ui.ShowUI();

        BackgroundButton.gameObject.SetActive(true);
        BackgroundButton.transform.SetSiblingIndex(siblingIndex - 1);

        _frontUI = ui;
        _openUIPool[uiType] = ui.gameObject;
    }
     
    public void CloseUI(BaseUI ui)
    {
        var uiType = ui.GetType();

        Logger.Log($"{GetType()}::CloseUI ({uiType})");

        ui.gameObject.SetActive(false);
        _openUIPool.Remove(uiType);
        _closedUIPool[uiType] = ui.gameObject;
        ui.transform.SetParent(ClosedTrs);

        _frontUI = null;
        var lastChild = UICanvasTrs.GetChild(UICanvasTrs.childCount - 3);
        if(lastChild)
        {
            _frontUI = lastChild.gameObject.GetComponent<BaseUI>();
            if (_frontUI != null) BackgroundButton.transform.SetSiblingIndex(UICanvasTrs.childCount - 3);
            else BackgroundButton.gameObject.SetActive(false);
        }
    }

    public void CloseCurrentFrontUI()
    {
        if (UIManager.Instance.CheckCanUIMove) _frontUI.CloseUI();
    }

    public BaseUI GetActiveUI<T>()
    {
        var uiType = typeof(T);
        return _openUIPool.ContainsKey(uiType) ? _openUIPool[uiType].GetComponent<BaseUI>() : null;
    }
    #endregion

    #region FADE
    public void Fade(bool mode, bool isDeactive, Action onFinish = null) // mode true: Fade-In, false: Fade-Out
    {
        Logger.Log($"{GetType()}::Fade / Mode : {mode}");

        if (mode) StartCoroutine(FadeInCo(isDeactive, onFinish));
        else StartCoroutine(FadeOutCo(isDeactive, onFinish));
    }

    private IEnumerator FadeInCo(bool isDeactive, Action onFinish)
    {
        Logger.Log($"{GetType()}::FadeInCo");

        yield return new WaitForSeconds(0.5f);

        FadeImg.gameObject.SetActive(true);

        Color tempColor = FadeImg.color;
        tempColor.a = 1f;
        FadeImg.color = tempColor;

        while(FadeImg.color.a > 0f)
        {
            tempColor.a -= 0.02f;
            FadeImg.color = tempColor;

            yield return new WaitForSeconds(0.01f);
        }

        tempColor.a = 0f;
        FadeImg.color = tempColor;

        yield return new WaitForSeconds(0.5f);

        if (isDeactive) FadeImg.gameObject.SetActive(false);
        onFinish?.Invoke();
    }

    private IEnumerator FadeOutCo(bool isDeactive, Action onFinish)
    {
        Logger.Log($"{GetType()}::FadeInCo");

        yield return new WaitForSeconds(0.5f);

        FadeImg.gameObject.SetActive(true);

        Color tempColor = FadeImg.color;
        tempColor.a = 0f;
        FadeImg.color = tempColor;

        while (FadeImg.color.a < 1f)
        {
            tempColor.a += 0.02f;
            FadeImg.color = tempColor;

            yield return new WaitForSeconds(0.01f);
        }

        tempColor.a = 1f;
        FadeImg.color = tempColor;

        yield return new WaitForSeconds(0.5f);

        if (isDeactive) FadeImg.gameObject.SetActive(false);
        onFinish?.Invoke();
    }
    #endregion
}
