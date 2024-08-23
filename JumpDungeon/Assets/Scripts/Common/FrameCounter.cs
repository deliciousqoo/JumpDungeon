using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrameCounter : SingletonBehaviour<FrameCounter>
{
    private float deltaTime = 0f;

    [SerializeField, Range(1, 100)]
    private int size = 25;
    [SerializeField]
    private Color color = Color.green;

    public bool isShow;

    protected override void Init()
    {
        base.Init();

        isShow = true;
    }

    private void Update()
    {
        deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;

        if(Input.GetKeyDown(KeyCode.F1))
        {
            isShow = !isShow;
        }

        Application.targetFrameRate = 60;
    }

    private void OnGUI()
    {
        if(isShow)
        {
            GUIStyle style = new GUIStyle();
            Rect rect = new Rect(30, 30, Screen.width, Screen.height);
            style.alignment = TextAnchor.UpperLeft;
            style.fontSize = size;
            style.normal.textColor = color;

            float ms = deltaTime * 1000f;
            float fps = 1.0f / deltaTime;
            string text = string.Format("{0:0.} FPS ({1:0.0}) ms", fps, ms);

            GUI.Label(rect, text, style);
        }
    }
}
