using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Experimental;
using UnityEngine.U2D.Animation;
public struct PlayerAffectedValue
{
    public float maxSpeed;
    public float jumpPower;
    public float speed;
    public float drag;
    public float gravityScale;
    public int jumpCount;
}
public class PlayerInfo : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    SpriteLibrary spriteLibrary;

    [SerializeField]
    private SpriteLibraryAsset[] skinList;

    private void Start()
    {
        spriteLibrary.spriteLibraryAsset = skinList[GameManager.instance.skinNum];
    }

    public void PlayerHide()
    {
        Color tempColor = spriteRenderer.color;
        tempColor.a = 0f;
        spriteRenderer.color = tempColor;
    }
}
