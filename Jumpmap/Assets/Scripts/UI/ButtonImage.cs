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

    public void MenuStageButtonChange(bool check)
    {
        //Debug.Log("check ice");
        if (!check) { 
            button.image.sprite = offButton;
            button.interactable = false;
            foreach(Transform child  in this.transform)
            {
                child.gameObject.SetActive(false);
            }
        }
        else { 
            button.image.sprite = onButton;
            button.interactable = true;
            foreach (Transform child in this.transform)
            {
                child.gameObject.SetActive(true);
            }
        }
    }
}
