using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameUIController : MonoBehaviour
{
    public void OnClickPauseBtn()
    {
        
    }
    public void OnClickMoveLeftBtn()
    {
        Logger.Log("Move Left");

        InGameManager.Instance.Player.GetComponent<PlayerHorizontalMovement>().IsLeftMove = true;
    }
    public void OffClickMoveLeftBtn()
    {
        InGameManager.Instance.Player.GetComponent<PlayerHorizontalMovement>().IsLeftMove = false;
    }
    public void OnClickMoveRightBtn()
    {
        Logger.Log("Move Right");

        InGameManager.Instance.Player.GetComponent<PlayerHorizontalMovement>().IsRightMove = true;
    }
    public void OffClickMoveRightBtn()
    {
        InGameManager.Instance.Player.GetComponent<PlayerHorizontalMovement>().IsRightMove = false;
    }
    public void OnClickJumpBtn()
    {
        Logger.Log("Jump");

        //InGameManager.Instance.Player.GetComponent<PlayerJump>().DoPlayerJump();
    }
}
