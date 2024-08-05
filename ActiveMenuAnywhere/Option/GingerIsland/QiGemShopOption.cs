﻿using StardewValley;
using StardewValley.Locations;
using weizinai.StardewValleyMod.ActiveMenuAnywhere.Framework;

namespace weizinai.StardewValleyMod.ActiveMenuAnywhere.Option;

internal class QiGemShopOption : BaseOption
{
    public QiGemShopOption() : base(I18n.UI_Option_QiGemShop(), GetSourceRectangle(1)) { }

    public override bool IsEnable()
    {
        return IslandWest.IsQiWalnutRoomDoorUnlocked(out _);
    }

    public override void Apply()
    {
        Utility.TryOpenShopMenu("QiGemShop", null, true);
    }
}