using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TitleUIController : MonoBehaviour
{
    public Slider LoadingSlider;
    public TextMeshProUGUI LoadingProgressTxt;
    public TextMeshProUGUI ClickToStartTxt;
    public Button GuestLoginBtn;

    private bool _isAlreadyLogin;

    public void Init()
    {
        _isAlreadyLogin = ES3.FileExists(UserDataManager.Instance.FileName);
        Logger.Log($"Is login : {_isAlreadyLogin}");

        ClickToStartTxt.gameObject.SetActive(_isAlreadyLogin);
        GuestLoginBtn.gameObject.SetActive(!_isAlreadyLogin);
        LoadingSlider.gameObject.SetActive(false);
        LoadingProgressTxt.gameObject.SetActive(false);
    }

    public void OnClickGuestLoginBtn()
    {
        Logger.Log($"{GetType()}::OnClickGuestLoginBtn");

        var uiData = new ConfirmUIData();
        uiData.ConfirmType = ConfirmType.OK_CANCEL;
        uiData.ConfirmTxt = "게스트 계정으로 로그인 시\n유저 데이터가 손실될 수 있습니다\n계정을 생성하시겠습니까?";
        uiData.OKBtnTxt = "예";
        uiData.CancelBtnTxt = "아니오";
        uiData.OnClickOKBtn = async () =>
        {
            await LoginManager.Instance.OnClickGuestLogin();

            UserDataManager.Instance.SetDefaultUserData();
            UserDataManager.Instance.SaveUserData();

            //TitleManager.Instance.LoadGame();
            UIManager.Instance.Fade(false, true, () => {
                TitleManager.Instance.LoadGame();
                AudioManager.Instance.OnLoadUserData();
            });
        };
        UIManager.Instance.OpenUI<ConfirmUI>(uiData);
    }

    public void OnClickStartBtn()
    {
        if (!UIManager.Instance.CheckCanUIMove) return;
        
        Logger.Log($"{GetType()}::OnClickStartBtn");

        UIManager.Instance.CheckCanUIMove = false;
        UserDataManager.Instance.LoadUserData();

        UIManager.Instance.Fade(false, true, () => {
            TitleManager.Instance.LoadGame();
            AudioManager.Instance.OnLoadUserData();
        });
    }
}
