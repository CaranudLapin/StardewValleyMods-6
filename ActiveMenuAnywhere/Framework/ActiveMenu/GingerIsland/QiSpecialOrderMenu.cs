﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StardewValley;
using StardewValley.Locations;
using StardewValley.Menus;

namespace ActiveMenuAnywhere.Framework.ActiveMenu;

public class QiSpecialOrderMenu : BaseActiveMenu
{
    public QiSpecialOrderMenu(Rectangle bounds, Texture2D texture, Rectangle sourceRect) : base(bounds, texture, sourceRect)
    {
    }

    public override void ReceiveLeftClick()
    {
        var isQiWalnutRoomDoorUnlocked = IslandWest.IsQiWalnutRoomDoorUnlocked(out _);
        if (isQiWalnutRoomDoorUnlocked)
            Game1.activeClickableMenu = new SpecialOrdersBoard("Qi");
        else
            Game1.drawObjectDialogue(I18n.Tip_Unavailable());
    }
}