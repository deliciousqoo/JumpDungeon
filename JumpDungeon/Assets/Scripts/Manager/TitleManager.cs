using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TitleManager : SingletonBehaviour<TitleManager>
{
    public TitleUIController TitleUIController { get; private set; }

    private AsyncOperation m_AsyncOperation;

    protected override void Init()
    {
        m_IsDestroyOnLoad = true;

        base.Init();
    }

    private void Start()
    {
        TitleUIController = FindObjectOfType<TitleUIController>();
        if(TitleUIController == null)
        {
            Logger.LogError("TitleUIController does not exist.");
            return;
        }

        TitleUIController.Init();
    }

    public void StartGame()
    {
        StartCoroutine(StartGameCo());
    }
    public void LoadGame()
    {
        StartCoroutine(LoadGameCo());
    }
    private IEnumerator StartGameCo()
    {
        Logger.Log($"{GetType()}::StartGameCo");

        yield return 0;
    }
    private IEnumerator LoadGameCo()
    {
        Logger.Log($"{GetType()}::LoadGameCo");

        var loadingSlider = TitleUIController.LoadingSlider;
        var loadingProgressTxt = TitleUIController.LoadingProgressTxt;
        
        loadingSlider.gameObject.SetActive(true);
        loadingProgressTxt.gameObject.SetActive(true);
        TitleUIController.GuestLoginBtn.gameObject.SetActive(false);
        TitleUIController.ClickToStartTxt.gameObject.SetActive(false);
        UIManager.Instance.CheckCanUIMove = true;

        m_AsyncOperation = SceneLoader.Instance.LoadSceneAsync(SceneType.Lobby);
        if (m_AsyncOperation == null)
        {
            Logger.Log("Lobby async loading error.");
            yield break;
        }

        m_AsyncOperation.allowSceneActivation = false;

        loadingSlider.value = 0.5f;
        loadingProgressTxt.text = $"{(int)(loadingSlider.value * 100)}%";
        yield return new WaitForSeconds(0.5f);

        while(!m_AsyncOperation.isDone)
        {
            loadingSlider.value = m_AsyncOperation.progress < 0.5f ? 0.5f : m_AsyncOperation.progress;
            loadingProgressTxt.text = $"{(int)(loadingSlider.value * 100)}%";
            
            if(m_AsyncOperation.progress >= 0.9f)
            {
                m_AsyncOperation.allowSceneActivation = true;
                yield break;
            }

            yield return null;
        }

        yield return 0;
    }
}
