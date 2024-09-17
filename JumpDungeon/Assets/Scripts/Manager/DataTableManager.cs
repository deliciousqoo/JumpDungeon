using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DataTableManager : SingletonBehaviour<DataTableManager>
{
    private const string DATA_PATH = "DataTable";
    
    protected override void Init()
    {
        base.Init();

        LoadCharacterSkinDataTable();
    }

    #region CHARACTER_SKIN_DATA
    private const string CHARACTER_SKIN_DATA_TABLE = "CharacterSkinDataTable";
    private List<CharacterSkinData> m_CharacterSkinDataTable = new List<CharacterSkinData>();

    public List<CharacterSkinData> GetCharacterSkinList()
    {
        return m_CharacterSkinDataTable;
    }

    private void LoadCharacterSkinDataTable()
    {
        var parsedDataTable = CSVReader.Read($"{DATA_PATH}/{CHARACTER_SKIN_DATA_TABLE}");

        foreach(var data in parsedDataTable)
        {
            var skinData = new CharacterSkinData
            {
                SkinId = Convert.ToInt32(data["skin_id"]),
                SkinName = data["skin_name"].ToString(),
            };

            m_CharacterSkinDataTable.Add(skinData);
        }
    }

    public CharacterSkinData GetCharacterSkinData(int skinId)
    {
        return m_CharacterSkinDataTable.Where(skin => skin.SkinId == skinId).FirstOrDefault();
    }

    public class CharacterSkinData
    {
        public int SkinId;
        public string SkinName;
    }
    #endregion
}
