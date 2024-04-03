﻿using Microsoft.Xna.Framework;
using StardewValley;

namespace ActiveMenuAnywhere.Framework.Options;

public class DwarfOption : BaseOption
{
    public DwarfOption(Rectangle sourceRect) :
        base(I18n.Option_Dwarf(), sourceRect)
    {
    }

    public override void ReceiveLeftClick()
    {
        if (Game1.player.canUnderstandDwarves)
            Utility.TryOpenShopMenu("Dwarf", "Dwarf");
        else
            Game1.drawObjectDialogue(I18n.Tip_Unavailable());
    }
}