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

    private BaseUI m_FrontUI;
    private Dictionary<System.Type, GameObject> m_OpenUIPool = new Dictionary<System.Type, GameObject>();
    private Dictionary<System.Type, GameObject> m_ClosedUIPool = new Dictionary<System.Type, GameObject>();

    public bool CheckCanUIMove { get; set; } = true;

    private BaseUI GetUI<T>(out bool isAlreadyOpen)
    {
        var uiType = typeof(T);

        Logger.Log($"{GetType()}::GetUI({uiType})");

        BaseUI ui = null;
        isAlreadyOpen = false;

        if(m_OpenUIPool.ContainsKey(uiType))
        {
            ui = m_OpenUIPool[uiType].GetComponent<BaseUI>();
            isAlreadyOpen = true;
        }
        else if (m_ClosedUIPool.ContainsKey(uiType))
        {
            ui = m_ClosedUIPool[uiType].GetComponent<BaseUI>();
            m_ClosedUIPool.Remove(uiType);
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

        m_FrontUI = ui;
        m_OpenUIPool[uiType] = ui.gameObject;
    }
     
    public void CloseUI(BaseUI ui)
    {
        var uiType = ui.GetType();

        Logger.Log($"{GetType()}::CloseUI ({uiType})");

        ui.gameObject.SetActive(false);
        m_OpenUIPool.Remove(uiType);
        m_ClosedUIPool[uiType] = ui.gameObject;
        ui.transform.SetParent(ClosedTrs);

        m_FrontUI = null;
        var lastChild = UICanvasTrs.GetChild(UICanvasTrs.childCount - 3);
        if(lastChild)
        {
            m_FrontUI = lastChild.gameObject.GetComponent<BaseUI>();
            if (m_FrontUI != null) BackgroundButton.transform.SetSiblingIndex(UICanvasTrs.childCount - 3);
            else BackgroundButton.gameObject.SetActive(false);
        }
    }

    public void CloseCurrentFrontUI()
    {
        if (UIManager.Instance.CheckCanUIMove) m_FrontUI.CloseUI();
    }

    public BaseUI GetActiveUI<T>()
    {
        var uiType = typeof(T);
        return m_OpenUIPool.ContainsKey(uiType) ? m_OpenUIPool[uiType].GetComponent<BaseUI>() : null;
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
