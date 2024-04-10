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
    private Sprite onSprite;
    [SerializeField]
    private Sprite offSprite;

    [SerializeField]
    private Image[] clearProgress;

    private Button button;

    public void MenuStageButtonChange(bool check)
    {
        button = GetComponent<Button>();
        if (!check) { 
            button.image.sprite = offSprite;
            button.interactable = false;
            foreach(Transform child  in this.transform)
            {
                child.gameObject.SetActive(false);
            }
        }
        else { 
            button.image.sprite = onSprite;
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

    public void SpriteChange(bool check)
    {
        if (!check)
        {
            gameObject.GetComponent<Image>().sprite = offSprite;
        }
        else
        {
            gameObject.GetComponent<Image>().sprite = onSprite;
        }
    }
}
