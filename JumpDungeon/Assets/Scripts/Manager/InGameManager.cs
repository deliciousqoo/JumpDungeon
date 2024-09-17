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

    protected override void Init()
    {
        _isDestroyOnLoad = true;

        base.Init();

        InitializeInGame();
    }

    private void InitializeInGame()
    {
        GameObject clone = Instantiate(CharacterPrefab);
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
