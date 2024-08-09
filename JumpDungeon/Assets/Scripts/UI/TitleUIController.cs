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

    private bool m_IsAlreadyLogin;

    public void Init()
    {
        UserDataManager.Instance.LoadUserData();
        m_IsAlreadyLogin = UserDataManager.Instance.ExistsSavedData;
        
        ClickToStartTxt.gameObject.SetActive(m_IsAlreadyLogin);
        GuestLoginBtn.gameObject.SetActive(!m_IsAlreadyLogin);
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
        uiData.OnClickOKBtn = () =>
        {
            LoginManager.Instance.OnClickGuestLogin();

            UserDataManager.Instance.SetDefaultUserData();
            UserDataManager.Instance.SaveUserData();

            //TitleManager.Instance.LoadGame();
            UIManager.Instance.Fade(false, true, () => {
                TitleManager.Instance.LoadGame();
            });
        };
        UIManager.Instance.OpenUI<ConfirmUI>(uiData);
    }
}
