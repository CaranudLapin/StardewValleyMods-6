﻿using Microsoft.Xna.Framework;
using StardewValley;

namespace ActiveMenuAnywhere.Framework.Options;

public class MysticFalls3Option : BaseOption
{
    public MysticFalls3Option(Rectangle sourceRect) : base(I18n.Option_MysticFall3(), sourceRect)
    {
    }

    public override void ReceiveLeftClick()
    {
        if (Game1.player.eventsSeen.Contains("75160187"))
            Utility.TryOpenShopMenu("RSVMysticFalls3", null, false);
        else
            Game1.drawObjectDialogue(I18n.Tip_Unavailable());
    }
}