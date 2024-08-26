using Gpm.Ui;
using SuperMaxim.Messaging;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum CharacterSkinGrade
{
    RARE = 1,
    UNIQUE,
}
public class CharacterSkinUIData : BaseUIData { }
public class SelectUpdateMsg 
{
    public int SkinId;
    public bool IsGetted;
}
public class SkinUpdateMsg
{
    public int SkinId;
}

public class CharacterSkinUI : BaseUI
{
    public List<GameObject> CharacterSkinList = new List<GameObject>();
    public NumPrint HaveCount;
    public NumPrint TotalCount;
    public TextMeshProUGUI SkinName;
    public Button SkinEquipBtn;

    public ScrollRect temp;
    public GameObject CharacterSkinPrefab;
    public Transform ContentsTrs;
    public Image SelectPoint;
    public Sprite[] SelectPointSprites = new Sprite[2];

    private int m_SelectedSkinId;
    private int m_CurrSelectSkinId;
    private int m_HaveCount;

    private void OnEnable()
    {
        Messenger.Default.Subscribe<SelectUpdateMsg>(OnUpdateSelect);
    }
    private void OnDisable()
    {
        Messenger.Default.Unsubscribe<SelectUpdateMsg>(OnUpdateSelect);
    }

    public override void SetInfo(BaseUIData uiData)
    {
        base.SetInfo(uiData);

        ContentsTrs.localPosition = new Vector3(0f, 0f, 0f);

        if(ContentsTrs.childCount == 0) InitCharacterSkinList();
        else SetCharacterSkinList();

        HaveCount.printNum(m_HaveCount);
        TotalCount.printNum(CharacterSkinList.Count);
        SkinName.text = "";

        var userCharacterSkinData = UserDataManager.Instance.GetUserData<UserCharacterSkinData>();
        if(userCharacterSkinData == null)
        {
            Logger.LogError("UserCharacterSkinData does not exist.");
            return;
        }
        m_SelectedSkinId = userCharacterSkinData.EquippedCharacterSkin;
        m_CurrSelectSkinId = m_SelectedSkinId;

        if (userCharacterSkinData.GetUserCharacterSkinProgressData(m_SelectedSkinId) != null)
        {
            SkinName.text = userCharacterSkinData.GetUserCharacterSkinProgressData(m_SelectedSkinId).SkinName;
        }

        SelectPoint.gameObject.SetActive(true);
        SelectPoint.sprite = SelectPointSprites[1];
        SelectPoint.transform.SetParent(CharacterSkinList[m_SelectedSkinId % 1000 - 1].transform);
        SelectPoint.transform.SetSiblingIndex(0);
        SelectPoint.transform.localPosition = new Vector3(0f, 0f, 0f);

        SkinEquipBtn.interactable = true;
        SkinEquipBtn.image.color = new Color(1f, 1f, 1f, 1f);
        SkinEquipBtn.interactable = false;
        SkinEquipBtn.image.color = new Color(1f, 1f, 1f, 0.2f);
    }

    private void InitCharacterSkinList()
    {
        CharacterSkinList.Clear();
        m_HaveCount = 0;

        var characterSkinDataList = DataTableManager.Instance.GetCharacterSkinList();
        if(characterSkinDataList == null)
        {
            Logger.LogError("CharacterSkinDataList does not exist.");
            return;
        }

        var userCharacterSkinData = UserDataManager.Instance.GetUserData<UserCharacterSkinData>();
        if(userCharacterSkinData == null)
        {
            Logger.LogError("UserCharacterSkinData does not exist.");
            return;
        }

        int count = 1;
        foreach (var item in characterSkinDataList)
        {
            GameObject newGO = Instantiate(CharacterSkinPrefab, ContentsTrs);
            newGO.name = $"{count++}"; 
            var characterSkinItemData = new CharacterSkinItemData();
            characterSkinItemData.SkinId = item.SkinId;
            characterSkinItemData.IsGetted = userCharacterSkinData.GetUserCharacterSkinProgressData(characterSkinItemData.SkinId) == null ? false : true;
            if (characterSkinItemData.IsGetted) m_HaveCount++;

            newGO.GetComponent<CharacterSkinItem>().SetSkinItem(characterSkinItemData);

            CharacterSkinList.Add(newGO);
        }
    }

    public void SetCharacterSkinList()
    {
        var characterSkinDataList = DataTableManager.Instance.GetCharacterSkinList();
        if (characterSkinDataList == null)
        {
            Logger.LogError("CharacterSkinDataList does not exist.");
            return;
        }

        var userCharacterSkinData = UserDataManager.Instance.GetUserData<UserCharacterSkinData>();
        if (userCharacterSkinData == null)
        {
            Logger.LogError("UserCharacterSkinData does not exist.");
            return;
        }

        foreach (var item in characterSkinDataList)
        {
            var characterSkinItemData = new CharacterSkinItemData();
            characterSkinItemData.SkinId = item.SkinId;
            characterSkinItemData.IsGetted = userCharacterSkinData.GetUserCharacterSkinProgressData(item.SkinId) != null ? true : false;

            CharacterSkinList[item.SkinId % 1000 - 1].GetComponent<CharacterSkinItem>().SetSkinItem(characterSkinItemData);
        }
    }

    public void OnClickEquipSkinBtn()
    {
        m_SelectedSkinId = m_CurrSelectSkinId;
        SelectPoint.sprite = SelectPointSprites[1];

        var userCharacterSkinData = UserDataManager.Instance.GetUserData<UserCharacterSkinData>();
        if (userCharacterSkinData == null)
        {
            Logger.LogError("UserCharacterSkinData does not exist.");
            return;
        }

        userCharacterSkinData.EquippedCharacterSkin = m_SelectedSkinId;
    }

    private void OnUpdateSelect(SelectUpdateMsg selectUpdateMsg)
    {
        var userCharacterSkinData = UserDataManager.Instance.GetUserData<UserCharacterSkinData>();
        if (userCharacterSkinData == null)
        {
            Logger.LogError("UserCharacterSkinData does not exist.");
            return;
        }

        var tempSkinData = userCharacterSkinData.GetUserCharacterSkinProgressData(selectUpdateMsg.SkinId);
        SkinName.text = tempSkinData != null ? tempSkinData.SkinName : "???";

        if(selectUpdateMsg.SkinId == m_CurrSelectSkinId)
        {
            SelectPoint.gameObject.SetActive(false);
            SkinName.gameObject.SetActive(false); 
            m_CurrSelectSkinId = -1;
        }
        else
        {
            SelectPoint.gameObject.SetActive(true);
            SkinName.gameObject.SetActive(true);
            m_CurrSelectSkinId = selectUpdateMsg.SkinId;
        }

        SelectPoint.sprite = selectUpdateMsg.SkinId == m_SelectedSkinId ? SelectPointSprites[1] : SelectPointSprites[0];
        SelectPoint.transform.SetParent(CharacterSkinList[selectUpdateMsg.SkinId % 1000 - 1].transform);
        SelectPoint.transform.SetSiblingIndex(0);
        SelectPoint.transform.localPosition = new Vector3(0f, 0f, 0f);

        SkinEquipBtn.interactable = selectUpdateMsg.IsGetted;
        SkinEquipBtn.image.color = !selectUpdateMsg.IsGetted ? new Color(1f, 1f, 1f, 0.2f) : new Color(1f, 1f, 1f, 1f);
        if(selectUpdateMsg.SkinId == m_SelectedSkinId)
        {
            SkinEquipBtn.interactable = false;
            SkinEquipBtn.image.color = new Color(1f, 1f, 1f, 0.2f);
        }
    }
    private void OnUpdateSkin(SkinUpdateMsg skinUpdateMsg)
    {
        Logger.Log($"{GetType()}::OnUpdateSkin");

        var updateSkin = CharacterSkinList[skinUpdateMsg.SkinId % 1000 - 1].GetComponent<CharacterSkinItem>();

        var skinData = new CharacterSkinItemData();
        skinData.SkinId = skinUpdateMsg.SkinId;
        skinData.IsGetted = true;

        updateSkin.SetSkinItem(skinData);
    }
}
