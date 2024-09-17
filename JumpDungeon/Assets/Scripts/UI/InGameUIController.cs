using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameUIController : MonoBehaviour
{
    public void OnClickPauseBtn()
    {
        var uiData = new SettingUIData();
        uiData.ShowMotionCheck = false;
        uiData.CloseMotionCheck = false;
        uiData.OnShow = () => { Time.timeScale = 0f; };
        uiData.OnClose = () => { Time.timeScale = 1f; };
        uiData.SettingType = SettingType.IN_GAME;

        UIManager.Instance.OpenUI<SettingUI>(uiData);
    }
    public void OnClickMoveLeftBtn()
    {
        Logger.Log("Move Left");

        //InGameManager.Instance.Player.GetComponent<CharacterHorizontalMovement>().IsLeftMove = true;
    }
    public void OffClickMoveLeftBtn()
    {
        //InGameManager.Instance.Player.GetComponent<CharacterHorizontalMovement>().IsLeftMove = false;
    }
    public void OnClickMoveRightBtn()
    {
        Logger.Log("Move Right");

        //InGameManager.Instance.Player.GetComponent<CharacterHorizontalMovement>().IsRightMove = true;
    }
    public void OffClickMoveRightBtn()
    {
        //InGameManager.Instance.Player.GetComponent<CharacterHorizontalMovement>().IsRightMove = false;
    }
    public void OnClickJumpBtn()
    {
        Logger.Log("Jump");

        //InGameManager.Instance.Player.GetComponent<PlayerJump>().DoPlayerJump();
    }
}
