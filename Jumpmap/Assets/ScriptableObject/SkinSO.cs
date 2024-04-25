using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D.Animation;

[CreateAssetMenu(fileName = "SkinSO", menuName = "createSO", order = 0)]
public class SkinSO : ScriptableObject
{
    //public Dictionary<int, SpriteLibraryAsset> SkinList = new Dictionary<int, SpriteLibraryAsset>();
    public List<SpriteLibraryAsset> SkinList = new List<SpriteLibraryAsset>();
}
