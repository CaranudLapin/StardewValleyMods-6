﻿using Microsoft.Xna.Framework;
using StardewValley;
using weizinai.StardewValleyMod.ActiveMenuAnywhere.Framework;

namespace weizinai.StardewValleyMod.ActiveMenuAnywhere.Option;

internal class VolcanoShopOption : BaseOption
{
    public VolcanoShopOption(Rectangle sourceRect) :
        base(I18n.UI_Option_VolcanoShop(), sourceRect) { }

    public override bool IsEnable()
    {
        return Game1.player.mailReceived.Contains("willyHours") && Game1.player.canUnderstandDwarves;
    }

    public override void Apply()
    {
        Utility.TryOpenShopMenu("VolcanoShop", null, true);
    }
}