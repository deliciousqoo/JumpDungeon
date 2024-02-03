using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ButtonImage : MonoBehaviour
{
    [SerializeField]
    private Sprite startImage;
    [SerializeField]
    private Sprite onButton;
    [SerializeField]
    private Sprite offButton;

    private Button button;

    private void Awake()
    {
        button = GetComponent<Button>();
    }

    public void ButtonImageChange(bool check)
    {
        if (!check) { button.image.sprite = offButton; }
        else { button.image.sprite = onButton; }
    }
}
