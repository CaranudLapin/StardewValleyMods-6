﻿using Microsoft.Xna.Framework;
using StardewValley;

namespace ActiveMenuAnywhere.Framework.Options;

public class JojaShopOption : BaseOption
{
    public JojaShopOption(Rectangle sourceRect) :
        base(I18n.Option_JojaShop(), sourceRect)
    {
    }

    public override void ReceiveLeftClick()
    {
        if (!Game1.MasterPlayer.hasCompletedCommunityCenter())
            Utility.TryOpenShopMenu("Joja", "Claire");
        else
            Game1.drawObjectDialogue(I18n.Tip_Unavailable());
    }
}