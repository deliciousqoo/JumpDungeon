using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUserData
{
    void SetDefaultData();
    bool LoadData();
    bool SaveData();
}
