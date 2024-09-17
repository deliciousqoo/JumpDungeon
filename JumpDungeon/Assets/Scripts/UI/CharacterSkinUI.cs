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

    private int _selectedSkinId;
    private int _currSelectSkinId;
    private int _haveCount;

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

        HaveCount.printNum(_haveCount);
        TotalCount.printNum(CharacterSkinList.Count);
        SkinName.text = "";

        var userCharacterSkinData = UserDataManager.Instance.GetUserData<UserCharacterSkinData>();
        if(userCharacterSkinData == null)
        {
            Logger.LogError("UserCharacterSkinData does not exist.");
            return;
        }
        _selectedSkinId = userCharacterSkinData.EquippedCharacterSkin;
        _currSelectSkinId = _selectedSkinId;

        if (userCharacterSkinData.GetUserCharacterSkinProgressData(_selectedSkinId) != null)
        {
            SkinName.text = userCharacterSkinData.GetUserCharacterSkinProgressData(_selectedSkinId).SkinName;
        }

        SelectPoint.gameObject.SetActive(true);
        SelectPoint.sprite = SelectPointSprites[1];
        SelectPoint.transform.SetParent(CharacterSkinList[_selectedSkinId % 1000 - 1].transform);
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
        _haveCount = 0;

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
            if (characterSkinItemData.IsGetted) _haveCount++;

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
        _selectedSkinId = _currSelectSkinId;
        SelectPoint.sprite = SelectPointSprites[1];

        var userCharacterSkinData = UserDataManager.Instance.GetUserData<UserCharacterSkinData>();
        if (userCharacterSkinData == null)
        {
            Logger.LogError("UserCharacterSkinData does not exist.");
            return;
        }

        userCharacterSkinData.EquippedCharacterSkin = _selectedSkinId;
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

        if(selectUpdateMsg.SkinId == _currSelectSkinId)
        {
            SelectPoint.gameObject.SetActive(false);
            SkinName.gameObject.SetActive(false); 
            _currSelectSkinId = -1;
        }
        else
        {
            SelectPoint.gameObject.SetActive(true);
            SkinName.gameObject.SetActive(true);
            _currSelectSkinId = selectUpdateMsg.SkinId;
        }

        SelectPoint.sprite = selectUpdateMsg.SkinId == _selectedSkinId ? SelectPointSprites[1] : SelectPointSprites[0];
        SelectPoint.transform.SetParent(CharacterSkinList[selectUpdateMsg.SkinId % 1000 - 1].transform);
        SelectPoint.transform.SetSiblingIndex(0);
        SelectPoint.transform.localPosition = new Vector3(0f, 0f, 0f);

        SkinEquipBtn.interactable = selectUpdateMsg.IsGetted;
        SkinEquipBtn.image.color = !selectUpdateMsg.IsGetted ? new Color(1f, 1f, 1f, 0.2f) : new Color(1f, 1f, 1f, 1f);
        if(selectUpdateMsg.SkinId == _selectedSkinId)
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
