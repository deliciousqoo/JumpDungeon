using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameManager : SingletonBehaviour<InGameManager>
{
    public InGameUIController InGameUIController { get; private set; }
    public bool CheckControl { get; set; } = true;
    public static bool IsGameStop { get; set; } = false;
    public GameObject CharacterPrefab;

    private BaseGameEntity _entity;
    private const string _entityName = "Player";

    private int _curChapterNum;
    private int _curStageNum;

    protected override void Init()
    {
        _isDestroyOnLoad = true;

        base.Init();

        InitializeInGame();
        InitializeCharacter();
    }

    private void InitializeInGame()
    {
        var userPlayData = UserDataManager.Instance.GetUserData<UserPlayData>();
        if (userPlayData == null)
        {
            Logger.LogError("UserPlayData does not exist.");
            return;
        }

        _curChapterNum = userPlayData.SelectedChapter;
        _curStageNum = userPlayData.SelectedStage;

        GameObject stage_clone = Instantiate(Resources.Load($"Prefabs/Stages/{_curChapterNum}_{_curStageNum}", typeof(GameObject))) as GameObject;
    }

    private void InitializeCharacter()
    {
        GameObject clone = Instantiate(CharacterPrefab);
        clone.transform.position = GameObject.Find("CharacterSpawnPoint").transform.position;

        Character entity = clone.GetComponent<Character>();
        _entity = entity;

        CameraManager.Instance.Player = clone;
        entity.Setup(_entityName + LoginManager.Instance.UserId);
    }

    private void Update()
    {
        if (IsGameStop == true) return;

        _entity.Updated();
    }

    public void StopGame()
    {
        IsGameStop = true;
    }
}
