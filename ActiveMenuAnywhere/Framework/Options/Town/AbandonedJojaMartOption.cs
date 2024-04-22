﻿using Microsoft.Xna.Framework;
using StardewValley;
using StardewValley.Locations;

namespace ActiveMenuAnywhere.Framework.Options;

public class AbandonedJojaMartOption : BaseOption
{
    public AbandonedJojaMartOption(Rectangle sourceRect) :
        base(I18n.Option_AbandonedJojaMart(), sourceRect)
    {
    }

    public override void ReceiveLeftClick()
    {
        if (Game1.MasterPlayer.mailReceived.Contains("abandonedJojaMartAccessible"))
        {
            var abandonedJojaMart = Game1.RequireLocation<AbandonedJojaMart>("AbandonedJojaMart");
            abandonedJojaMart.checkBundle();
        }
        else
        {
            Game1.drawObjectDialogue(I18n.Tip_Unavailable());
        }
    }
}