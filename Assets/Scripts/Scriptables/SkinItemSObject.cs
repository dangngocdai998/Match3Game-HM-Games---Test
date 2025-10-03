using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SkinItem_", menuName = "ScriptableObjects/Create new Item", order = 0)]
public class SkinItemSObject : ScriptableObject
{
    [SerializeField] Sprite sprite_SkinNormal;
    [SerializeField] Sprite sprite_SkinFish;

    public Sprite GetSkinItem(eTypeSkinItem type)
    {
        switch (type)
        {
            case eTypeSkinItem.NORMAL:
                return sprite_SkinNormal;
            case eTypeSkinItem.FISH:
                return sprite_SkinFish;
            default:
                return sprite_SkinNormal;
        }
    }
}
