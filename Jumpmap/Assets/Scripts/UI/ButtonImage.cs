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

    [SerializeField]
    private Image[] clearProgress;

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
    
    public void MenuStageStarChange(int progress)
    {
        for(int i=2;i<5;i++)
        {
            if (i <= progress) clearProgress[i - 2].gameObject.SetActive(true);
            else clearProgress[i - 2].gameObject.SetActive(false);
        }
    }

    public void ButtonImageChange(bool check)
    {
        if (!check)
        {
            button.image.sprite = offButton;
        }
        else
        {
            button.image.sprite = onButton;
        }
    }
}
