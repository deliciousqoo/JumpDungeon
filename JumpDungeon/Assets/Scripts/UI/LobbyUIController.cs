using SuperMaxim.Messaging;
using UnityEngine;
using UnityEngine.EventSystems;

public class GoldUpdateMsg { }
public class HeartUpdateMsg { }
public class ScoreUpdateMsg { }

public class LobbyUIController : MonoBehaviour
{
    public GameObject MidUI;
    public GameObject ModeUI;

    public GameObject GoldUI;
    public GameObject HeartUI;
    public GameObject ScoreUI;

    public int m_SelectedChapter { get; set; }

    private void OnEnable()
    {
        Messenger.Default.Subscribe<GoldUpdateMsg>(OnUpdateGold);
        Messenger.Default.Subscribe<HeartUpdateMsg>(OnUpdateHeart);
        Messenger.Default.Subscribe<ScoreUpdateMsg>(OnUpdateScore);
    }
    private void OnDisable()
    {
        Messenger.Default.Unsubscribe<GoldUpdateMsg>(OnUpdateGold);
        Messenger.Default.Unsubscribe<HeartUpdateMsg>(OnUpdateHeart);
        Messenger.Default.Unsubscribe<ScoreUpdateMsg>(OnUpdateScore);
    }

    public void Init()
    {
        SetCurrChapter();

        var userGoodsData = UserDataManager.Instance.GetUserData<UserGoodsData>();
        if(userGoodsData == null)
        {
            Logger.LogError("UserGoodsData does not exist.");
            return;
        }

        GoldUI.GetComponent<NumPrint>().printNum(userGoodsData.Gold);
        HeartUI.GetComponent<NumPrint>().printNum(userGoodsData.Heart);
        ScoreUI.GetComponent<NumPrint>().printNum(userGoodsData.Score);
    }

    public void SetCurrChapter()
    {
        var userPlayData = UserDataManager.Instance.GetUserData<UserPlayData>();
        if(userPlayData == null)
        {
            Logger.LogError("UserPlayData does not exist.");
            return;
        }

        var uiData = new ChapterUIData();
        uiData.ChapterType = (ChapterType)userPlayData.SelectedChapter;
        uiData.ChapterNameTxt = ((ChapterType)userPlayData.SelectedChapter).ToString();
        uiData.ShowMotionCheck = false;
        uiData.CloseMotionCheck = false;
        LobbyManager.Instance.OpenChapterUI(uiData);

        m_SelectedChapter = (int)userPlayData.SelectedChapter;
    }

    public void OnClickChangeChapter()
    {
        if (EventSystem.current.currentSelectedGameObject != null && UIManager.Instance.CheckCanUIMove)
        {
            GameObject clickObject = EventSystem.current.currentSelectedGameObject;

            int dir = 0;
            int m_BeforeChapter = m_SelectedChapter;

            switch(clickObject.name)
            {
                case "LeftChapterBtn":
                    m_SelectedChapter--;
                    dir = 1;
                    break;
                case "RightChapterBtn":
                    m_SelectedChapter++;
                    dir = -1;
                    break;
            }

            LobbyManager.Instance.CloseChapterUI(m_BeforeChapter, dir);

            if (m_SelectedChapter == (int)ChapterType.COUNT) m_SelectedChapter = 0;
            else if (m_SelectedChapter < 0) m_SelectedChapter = (int)ChapterType.COUNT - 1;

            var uiData = new ChapterUIData();
            uiData.ChapterType = (ChapterType)m_SelectedChapter;
            uiData.ChapterNameTxt = ((ChapterType)m_SelectedChapter).ToString();
            uiData.ShowMotionCheck = true;
            uiData.StartPos = new Vector3(-720f * dir, 0f, 0f);
            uiData.IsHorizontal = true;

            LobbyManager.Instance.OpenChapterUI(uiData);
        }
    }

    public void OnClickSettingBtn()
    {
        if (!UIManager.Instance.CheckCanUIMove) return;

        Logger.Log($"{GetType()}::OnClickSettingBtn");

        var uiData = new SettingUIData();
        uiData.SettingType = SettingType.OUT_GAME;
        uiData.StartPos = new Vector3(720f, 0f, 0f);
        uiData.EndPos = new Vector3(720f, 0f, 0f);
        uiData.ShowMotionCheck = true;
        uiData.CloseMotionCheck = true;
        uiData.IsHorizontal = true;

        UIManager.Instance.OpenUI<SettingUI>(uiData);
    }

    public void OnClickMissionBtn()
    {
        if (!UIManager.Instance.CheckCanUIMove) return;

        Logger.Log($"{GetType()}::OnClickMissionBtn");

        var uiData = new MissionUIData();
        uiData.StartPos = new Vector3(720f, 0f, 0f);
        uiData.EndPos = new Vector3(720f, 0f, 0f);
        uiData.ShowMotionCheck = true;
        uiData.CloseMotionCheck = true;
        uiData.IsHorizontal = true;

        UIManager.Instance.OpenUI<MissionUI>(uiData);
    }

    public void OnClickChargeBtn()
    {
        if (!UIManager.Instance.CheckCanUIMove) return;

        Logger.Log($"{GetType()}::OnClickChargeBtn");

        var uiData = new BaseUIData();
        //UIManager.Instance.OpenUI<ChargeUI>(uiData);
    }

    public void OnClickStoreBtn()
    {
        if (!UIManager.Instance.CheckCanUIMove) return;

        Logger.Log($"{GetType()}::OnClickStoreBtn");

        var uiData = new BaseUIData();
        //UIManager.Instance.OpenUI<ChargeUI>(uiData);
    }

    public void OnClickRankBtn()
    {
        if (!UIManager.Instance.CheckCanUIMove) return;

        Logger.Log($"{GetType()}::OnClickRankBtn");

        var uiData = new BaseUIData();
        //UIManager.Instance.OpenUI<ChargeUI>(uiData);
    }

    public void OnClickModeBtn()
    {
        if (!UIManager.Instance.CheckCanUIMove) return;

        Logger.Log($"{GetType()}::OnClickModeBtn");

        var uiData = new BaseUIData();
        //UIManager.Instance.OpenUI<ChargeUI>(uiData);
    }

    public void OnClickSkinBtn()
    {
        if (!UIManager.Instance.CheckCanUIMove) return;

        var userCharacterSkinData = UserDataManager.Instance.GetUserData<UserCharacterSkinData>();
        if(userCharacterSkinData == null)
        {
            Logger.LogError("UserCharacterSkinData does not exist.");
            return;
        }

        Logger.Log($"{GetType()}::OnClickSkinBtn");

        var uiData = new CharacterSkinUIData();
        uiData.StartPos = new Vector3(0f, -1280f, 0f);
        uiData.EndPos = new Vector3(0f, -1280f, 0f);
        uiData.ShowMotionCheck = true;
        uiData.CloseMotionCheck = true;
        uiData.IsHorizontal = false;

        UIManager.Instance.OpenUI<CharacterSkinUI>(uiData);
    }

    public void OnClickLuckyBoxBtn()
    {
        if (!UIManager.Instance.CheckCanUIMove) return;

        Logger.Log($"{GetType()}::OnClickBoxBtn");

        var uiData = new LuckyBoxUIData();
        uiData.StartPos = new Vector3(0f, -1280f, 0f);
        uiData.EndPos = new Vector3(0f, -1280f, 0f);
        uiData.ShowMotionCheck = true;
        uiData.CloseMotionCheck = true;
        uiData.IsHorizontal = false;

        UIManager.Instance.OpenUI<LuckyBoxUI>(uiData);
    }

    private void OnUpdateGold(GoldUpdateMsg goldUpdateMsg)
    {
        var userGoodsData = UserDataManager.Instance.GetUserData<UserGoodsData>();
        if(userGoodsData == null)
        {
            Logger.LogError("UserGoods does not exist.");
            return;
        }

        GoldUI.GetComponent<NumPrint>().printNum(userGoodsData.Gold);
    }

    private void OnUpdateHeart(HeartUpdateMsg heartUpdateMsg)
    {
        var userGoodsData = UserDataManager.Instance.GetUserData<UserGoodsData>();
        if (userGoodsData == null)
        {
            Logger.LogError("UserGoods does not exist.");
            return;
        }

        HeartUI.GetComponent<NumPrint>().printNum(userGoodsData.Heart);
    }

    private void OnUpdateScore(ScoreUpdateMsg scoreUpdateMsg)
    {
        var userGoodsData = UserDataManager.Instance.GetUserData<UserGoodsData>();
        if (userGoodsData == null)
        {
            Logger.LogError("UserGoods does not exist.");
            return;
        }

        ScoreUI.GetComponent<NumPrint>().printNum(userGoodsData.Score);
    }
}
