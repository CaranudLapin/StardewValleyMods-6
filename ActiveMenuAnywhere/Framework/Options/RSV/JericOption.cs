﻿using Microsoft.Xna.Framework;
using StardewValley;

namespace ActiveMenuAnywhere.Framework.Options;

public class JericOption : BaseOption
{
    public JericOption(Rectangle sourceRect) :
        base(I18n.Option_Jeric(), sourceRect)
    {
    }

    public override void ReceiveLeftClick()
    {
        Utility.TryOpenShopMenu("RSVJericShop", "Jeric");
    }
}