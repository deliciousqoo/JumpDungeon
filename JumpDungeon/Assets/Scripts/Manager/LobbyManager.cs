using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyManager : SingletonBehaviour<LobbyManager>
{
    public LobbyUIController LobbyUIController { get; private set; }
    
    public Transform ChapterUITrs;

    private BaseUI _currChapterUI;
    private Dictionary<int, GameObject> _chapterUIPool = new Dictionary<int, GameObject>();

    protected override void Init()
    {
        _isDestroyOnLoad = true;

        base.Init();
    }

    private void Start()
    {
        LobbyUIController = FindObjectOfType<LobbyUIController>();
        if(LobbyUIController == null)
        {
            Logger.LogError("LobbyUIController does not exist.");
            return;
        }

        ChapterUITrs = GameObject.Find("ChapterTrs").GetComponent<Transform>();

        LobbyUIController.Init();
        AudioManager.Instance.PlayBGM(BGM.temp_bgm);
    }

    private BaseUI GetChapterUI(int chapterNum)
    {
        Logger.Log($"{GetType()}::GetChapterUI");

        BaseUI ui = null;

        if (_chapterUIPool.ContainsKey(chapterNum))
        {
            ui = _chapterUIPool[chapterNum].GetComponent<BaseUI>();
        }
        else
        {
            var uiObj = Instantiate(Resources.Load($"Prefabs/UI/ChapterUI", typeof(GameObject))) as GameObject;
            ui = uiObj.GetComponent<BaseUI>();
        }

        return ui;
    }
    public void OpenChapterUI(BaseUIData uiData)
    {
        var chapterUIData = uiData as ChapterUIData;
        var ui = GetChapterUI((int)chapterUIData.ChapterType);

        if (!ui)
        {
            Logger.LogError($"ChapterUI does not exist.");
            return;
        }

        ui.Init(ChapterUITrs);
        ui.gameObject.SetActive(true);
        ui.SetInfo(uiData);
        ui.ShowUI();

        _currChapterUI = ui;
        _chapterUIPool[(int)chapterUIData.ChapterType] = ui.gameObject;
    }

    public void CloseChapterUI(int chapterNum, int dir)
    {
        UIManager.Instance.CheckCanUIMove = false;

        Sequence sequence = DOTween.Sequence();
        Tween tr1 = _chapterUIPool[chapterNum].transform.DOLocalMove(new Vector3(720f * dir, 0f, 0f), 0.3f).SetEase(Ease.InCubic);
        sequence.Append(tr1).OnComplete(() =>
        {
            UIManager.Instance.CheckCanUIMove = true;
            _chapterUIPool[chapterNum].SetActive(false);
        });
    }

    public void CloseCurrChapterUI()
    {
        _currChapterUI.gameObject.SetActive(false);
    }

    public GameObject GetActiveChapterUI(int chapterNum)
    {
        return _chapterUIPool[chapterNum];
    }
}
