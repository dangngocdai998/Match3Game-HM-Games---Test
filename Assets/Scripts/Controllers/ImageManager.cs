using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageManager : SingletonMonoBehaviour<ImageManager>
{
    [Header("Sprite Items Bonus")]
    [SerializeField] Sprite spriteBomb;
    [SerializeField] Sprite spriteHorizontal;
    [SerializeField] Sprite spriteVertical;

    [Header("Sprite Items Normal")]
    [SerializeField] SkinItemSObject skinItem1;
    [SerializeField] SkinItemSObject skinItem2;
    [SerializeField] SkinItemSObject skinItem3;
    [SerializeField] SkinItemSObject skinItem4;
    [SerializeField] SkinItemSObject skinItem5;
    [SerializeField] SkinItemSObject skinItem6;
    [SerializeField] SkinItemSObject skinItem7;

    public Sprite GetSpriteItemBonus(BonusItem.eBonusType type)
    {
        return type switch
        {
            BonusItem.eBonusType.NONE => null,
            BonusItem.eBonusType.HORIZONTAL => spriteHorizontal,
            BonusItem.eBonusType.VERTICAL => spriteVertical,
            BonusItem.eBonusType.ALL => spriteBomb,
            _ => null,
        };
    }
    public Sprite GetSpriteItemNormal(NormalItem.eNormalType type)
    {
        return type switch
        {
            NormalItem.eNormalType.TYPE_ONE => skinItem1.GetSkinItem(GameManager.Instance.TypeSkin),
            NormalItem.eNormalType.TYPE_TWO => skinItem2.GetSkinItem(GameManager.Instance.TypeSkin),
            NormalItem.eNormalType.TYPE_THREE => skinItem3.GetSkinItem(GameManager.Instance.TypeSkin),
            NormalItem.eNormalType.TYPE_FOUR => skinItem4.GetSkinItem(GameManager.Instance.TypeSkin),
            NormalItem.eNormalType.TYPE_FIVE => skinItem5.GetSkinItem(GameManager.Instance.TypeSkin),
            NormalItem.eNormalType.TYPE_SIX => skinItem6.GetSkinItem(GameManager.Instance.TypeSkin),
            NormalItem.eNormalType.TYPE_SEVEN => skinItem7.GetSkinItem(GameManager.Instance.TypeSkin),
            _ => null,
        };
    }

}
