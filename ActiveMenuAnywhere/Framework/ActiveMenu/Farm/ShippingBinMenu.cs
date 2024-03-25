﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StardewModdingAPI;
using StardewValley;
using StardewValley.Buildings;
using StardewValley.Objects;

namespace ActiveMenuAnywhere.Framework.ActiveMenu;

public class ShippingBinMenu : BaseActiveMenu
{
    public ShippingBinMenu(Rectangle bounds, Texture2D texture, Rectangle sourceRect) : base(bounds, texture, sourceRect)
    {
    }

    public override void ReceiveLeftClick()
    {
        Game1.drawObjectDialogue(I18n.Tip_Unfinished());
    }
}