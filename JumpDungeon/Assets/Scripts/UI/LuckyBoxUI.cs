using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using SuperMaxim.Messaging;

public enum LuckyBoxItemGrade
{
    BASIC,
    RARE,
    UNIQUE,
}
public enum LuckyBoxItemType
{
    GOLD,
    HEART,
    SKIN,
}

public class LuckyBoxItemData
{
    public LuckyBoxItemGrade LuckyBoxItemGrade;
    public LuckyBoxItemType LuckyBoxItemType;
    public int GoldAmount;
    public int HeartAmount;
    public int skinId;

    public LuckyBoxItemData()
    {
        LuckyBoxItemGrade = LuckyBoxItemGrade.BASIC;
        LuckyBoxItemType = LuckyBoxItemType.GOLD;
        GoldAmount = 0;
        HeartAmount = 0;
        skinId = 0;
    }
}

public class LuckyBoxUIData : BaseUIData
{

}

public class LuckyBoxUI : BaseUI
{
    public Image LuckyBox;
    public Image LuckyBoxItem;
    public Sprite GoldImg;
    public Sprite HeartImg;

    private LuckyBoxUIData m_LuckyBoxUIData;

    private const string BOX_OPEN_PATH = "Art/UI/Box/box_open";
    private const string BOX_SHAKE_PATH = "Art/UI/Box/box_shake";
    private Sprite[] m_BoxOpenSprites;
    private Sprite[] m_BoxShakeSprites;

    private bool m_IsCanChoice = true;

    private List<int> m_RareSkinList = new List<int>();
    private List<int> m_UniqueSkinList = new List<int>();

    public override void SetInfo(BaseUIData uiData)
    {
        base.SetInfo(uiData);

        SetLuckyBoxList();
        LuckyBoxItem.gameObject.SetActive(false);

        m_BoxOpenSprites = Resources.LoadAll<Sprite>(BOX_OPEN_PATH);
        m_BoxShakeSprites = Resources.LoadAll<Sprite>(BOX_SHAKE_PATH);
        LuckyBox.sprite = m_BoxOpenSprites[0];
    }

    private void SetLuckyBoxList()
    {
        m_RareSkinList.Clear();
        m_UniqueSkinList.Clear();

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
            if (userCharacterSkinData.GetUserCharacterSkinProgressData(item.SkinId) != null) continue;

            var characterSkinGrade = (CharacterSkinGrade)(item.SkinId / 10000);
            bool isLuckyBoxItem = ((item.SkinId / 1000) % 10) == 1 ? true : false;

            if(characterSkinGrade == CharacterSkinGrade.RARE && isLuckyBoxItem)
            {
                m_RareSkinList.Add(item.SkinId);
            }
            else if(characterSkinGrade == CharacterSkinGrade.UNIQUE && isLuckyBoxItem)
            {
                m_UniqueSkinList.Add(item.SkinId);
            }
        }
    }

    public void OnClickAdBtn()
    {

    }

    public void OnClickChoiceBtn()
    {
        if (!m_IsCanChoice) return;
        m_IsCanChoice = false;
        UIManager.Instance.CheckCanUIMove = false;

        var userGoodData = UserDataManager.Instance.GetUserData<UserGoodsData>();
        if(userGoodData == null)
        {
            Logger.LogError("UserGoodsData does not exist.");
            return;
        }

        //userGoodData.Gold -= 100;
        //userGoodData.SaveData();

        StartCoroutine("DoChoiceLuckyBox");
    }

    private IEnumerator DoChoiceLuckyBox()
    {
        LuckyBoxItem.gameObject.SetActive(false);
        for(int i=0;i<2;i++)
        {
            for(int j=0;j<m_BoxShakeSprites.Length;j++)
            {
                LuckyBox.sprite = m_BoxShakeSprites[j];
                yield return new WaitForSeconds(0.08f);
            }
        }

        for(int i=0;i<m_BoxOpenSprites.Length;i++)
        {
            LuckyBox.sprite = m_BoxOpenSprites[i];
            yield return new WaitForSeconds(0.08f);
        }


        var skinIconSprites = Resources.LoadAll<Sprite>("Art/Player/thumbnail");
        if(skinIconSprites == null)
        {
            Logger.LogError("SkinIconSprites does not exist.");
            yield return null;
        } 

        float gapTime = 0f;
        int count = Random.Range(0, 3);
        var luckyBoxItemResult = new LuckyBoxItemData();
        LuckyBoxItem.gameObject.SetActive(true);
        LuckyBox.transform.DOShakePosition(4f, new Vector3(0f, 30f, 0f), 10, 1, false, true);

        float start_time = Time.time;
        while (gapTime < 0.13f)
        {
            yield return new WaitForSeconds(gapTime);

            if (count == (int)LuckyBoxItemType.GOLD)
            {
                LuckyBoxItem.sprite = GoldImg;
            }
            else if(count == (int)LuckyBoxItemType.HEART)
            {
                LuckyBoxItem.sprite = HeartImg;
            }
            else if(count == (int)LuckyBoxItemType.SKIN)
            {
                LuckyBoxItem.sprite = skinIconSprites[Random.Range(0, skinIconSprites.Length)];
                count = -1;
            }

            count++;
            gapTime = Mathf.Lerp(gapTime, gapTime + 0.1f, Time.deltaTime * 1.5f);
            //Debug.Log(gapTime);
        }
        float end_time = Time.time;

        //Logger.Log($"Time: {end_time - start_time}");

        luckyBoxItemResult = RandomChoiceLuckyBoxItem();
        Logger.Log($"Grade:{luckyBoxItemResult.LuckyBoxItemGrade}/Type:{luckyBoxItemResult.LuckyBoxItemType}");

        var uiData = new LuckyBoxResultUIData();
        uiData.ShowMotionCheck = false;
        uiData.CloseMotionCheck = false;
        uiData.OnShow = () =>
        {
            SetUserLuckyBoxResult(luckyBoxItemResult);
        };
        uiData.OnClose = () =>
        {
            LuckyBox.sprite = m_BoxOpenSprites[0];
            LuckyBoxItem.gameObject.SetActive(false);
        };

        switch(luckyBoxItemResult.LuckyBoxItemType)
        {
            case LuckyBoxItemType.SKIN:
                uiData.LuckyBoxSprite = skinIconSprites[(luckyBoxItemResult.skinId % 1000) - 1];
                LuckyBoxItem.sprite = skinIconSprites[(luckyBoxItemResult.skinId % 1000) - 1];
                break;
            case LuckyBoxItemType.GOLD:
                uiData.LuckyBoxSprite = GoldImg;
                LuckyBoxItem.sprite = GoldImg;
                break;
            case LuckyBoxItemType.HEART:
                uiData.LuckyBoxSprite = HeartImg;
                LuckyBoxItem.sprite = HeartImg;
                break;
        }

        yield return new WaitForSeconds(0.3f);

        UIManager.Instance.OpenUI<LuckyBoxResultUI>(uiData);

        m_IsCanChoice = true;
    }

    private LuckyBoxItemData RandomChoiceLuckyBoxItem()
    {
        int category = Random.Range(1, 101);
        var luckyBoxData = new LuckyBoxItemData();
        int tempIndex = 0;

        if (category <= 66) // Basic
        {
            tempIndex = Random.Range(0, 2);
            luckyBoxData.LuckyBoxItemGrade = LuckyBoxItemGrade.BASIC;
            if (tempIndex == 0)
            {
                luckyBoxData.LuckyBoxItemType = LuckyBoxItemType.GOLD;
                luckyBoxData.GoldAmount = 10;
            }
            else
            {
                luckyBoxData.LuckyBoxItemType = LuckyBoxItemType.HEART;
                luckyBoxData.HeartAmount = 1;
            }
        }
        else if(category <= 99) // Rare
        {
            tempIndex = Random.Range(0, 3);
            luckyBoxData.LuckyBoxItemGrade = LuckyBoxItemGrade.RARE;
            if (tempIndex == 0 && m_RareSkinList.Count != 0)
            {
                luckyBoxData.LuckyBoxItemType = LuckyBoxItemType.SKIN;
                luckyBoxData.skinId = m_RareSkinList[Random.Range(0, m_RareSkinList.Count)];
            }
            else if(tempIndex == 1)
            {
                luckyBoxData.LuckyBoxItemType = LuckyBoxItemType.HEART;
                luckyBoxData.HeartAmount = 5;
            }
            else
            {
                luckyBoxData.LuckyBoxItemType = LuckyBoxItemType.GOLD;
                luckyBoxData.GoldAmount = 100;
            }
        }
        else // Unique
        {
            luckyBoxData.LuckyBoxItemGrade = LuckyBoxItemGrade.UNIQUE;
            if (m_UniqueSkinList.Count != 0)
            {
                luckyBoxData.LuckyBoxItemType = LuckyBoxItemType.SKIN;
                luckyBoxData.skinId = m_UniqueSkinList[Random.Range(0, m_UniqueSkinList.Count)];
            }
            else
            {
                luckyBoxData.LuckyBoxItemType = LuckyBoxItemType.GOLD;
                luckyBoxData.GoldAmount = 1000;
            }
        }

        return luckyBoxData;
    }

    private void SetUserLuckyBoxResult(LuckyBoxItemData result)
    {
        if (result.LuckyBoxItemType == LuckyBoxItemType.GOLD)
        {
            var userGoodsData = UserDataManager.Instance.GetUserData<UserGoodsData>();
            if(userGoodsData == null)
            {
                Logger.LogError("UserGoodsData does not exist");
                return;
            }
            
            userGoodsData.Gold += result.GoldAmount;
            userGoodsData.SaveData();

            var goldUpdateMsg = new GoldUpdateMsg();
            Messenger.Default.Publish(goldUpdateMsg);
        }
        else if (result.LuckyBoxItemType == LuckyBoxItemType.HEART)
        {
            var userGoodsData = UserDataManager.Instance.GetUserData<UserGoodsData>();
            if (userGoodsData == null)
            {
                Logger.LogError("UserGoodsData does not exist");
                return;
            }

            userGoodsData.Heart += result.HeartAmount;
            userGoodsData.SaveData();

            var heartUpdateMsg = new HeartUpdateMsg();
            Messenger.Default.Publish(heartUpdateMsg);
        }
        else if (result.LuckyBoxItemType == LuckyBoxItemType.SKIN)
        {
            var userCharacterSkinData = UserDataManager.Instance.GetUserData<UserCharacterSkinData>();
            if (userCharacterSkinData == null)
            {
                Logger.LogError("UserCharacterSkinData does not exist");
                return;
            }

            userCharacterSkinData.AddUserCharacterSkinProgressData(result.skinId);
            userCharacterSkinData.SaveData();
        }
    }
}
