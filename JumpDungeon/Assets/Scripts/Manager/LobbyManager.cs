using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyManager : SingletonBehaviour<LobbyManager>
{
    public LobbyUIController LobbyUIController { get; private set; }

    private bool m_IsLoadingInGame;

    protected override void Init()
    {
        m_IsDestroyOnLoad = true;
        m_IsLoadingInGame = false;

        base.Init();
    }

    private void Start()
    {
        LobbyUIController = FindObjectOfType<LobbyUIController>();
        if(LobbyUIController == null)
        {
            Logger.LogError("LobbyUIController does not exist.");
            return;
        }
    }

    public void StartInGame()
    {
        if(m_IsLoadingInGame)
        {
            return;
        }

        m_IsLoadingInGame = true;
    }
}
