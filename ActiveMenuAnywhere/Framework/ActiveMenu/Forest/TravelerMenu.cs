﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StardewValley;

namespace ActiveMenuAnywhere.Framework.ActiveMenu;

public class TravelerMenu : BaseActiveMenu
{
    public TravelerMenu(Rectangle bounds, Texture2D texture, Rectangle sourceRect) : base(bounds, texture, sourceRect)
    {
    }

    public override void ReceiveLeftClick()
    {
        var isTravelingMerchantDay = Game1.RequireLocation<StardewValley.Locations.Forest>("Forest").travelingMerchantDay;
        if (isTravelingMerchantDay)
            Utility.TryOpenShopMenu("Traveler", null, true);
        else
            Game1.drawObjectDialogue(I18n.Tip_Unavailable());
    }
}