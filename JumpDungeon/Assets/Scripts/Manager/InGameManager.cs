using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameManager : SingletonBehaviour<InGameManager>
{
    public InGameUIController InGameUIController { get; private set; }

    public GameObject Player;

    public bool CheckControl { get; set; } = true;

    protected override void Init()
    {
        m_IsDestroyOnLoad = true;

        base.Init();
    }


}
