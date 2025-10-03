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
    [SerializeField] Sprite spriteItem1;
    [SerializeField] Sprite spriteItem2;
    [SerializeField] Sprite spriteItem3;
    [SerializeField] Sprite spriteItem4;
    [SerializeField] Sprite spriteItem5;
    [SerializeField] Sprite spriteItem6;
    [SerializeField] Sprite spriteItem7;

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
            NormalItem.eNormalType.TYPE_ONE => spriteItem1,
            NormalItem.eNormalType.TYPE_TWO => spriteItem2,
            NormalItem.eNormalType.TYPE_THREE => spriteItem3,
            NormalItem.eNormalType.TYPE_FOUR => spriteItem4,
            NormalItem.eNormalType.TYPE_FIVE => spriteItem5,
            NormalItem.eNormalType.TYPE_SIX => spriteItem6,
            NormalItem.eNormalType.TYPE_SEVEN => spriteItem7,
            _ => null,
        };
    }

}
