﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StardewValley;

namespace ActiveMenuAnywhere.Framework.Options;

public class MarlonOption : BaseOption
{
    public MarlonOption(Rectangle bounds, Texture2D texture, Rectangle sourceRect) : base(bounds, texture, sourceRect, I18n.Option_Marlon())
    {
    }

    public override void ReceiveLeftClick()
    {
        if (Game1.player.mailReceived.Contains("guildMember"))
            Marlon();
        else
            Game1.drawObjectDialogue(I18n.Tip_Unavailable());
    }

    private void Marlon()
    {
        if (Game1.player.itemsLostLastDeath.Count > 0)
        {
            var options = new List<Response>
            {
                new("Shop", Game1.content.LoadString("Strings\\Locations:ScienceHouse_CarpenterMenu_Shop")),
                new("Recovery", Game1.content.LoadString("Strings\\Locations:AdventureGuild_ItemRecovery")),
                new("Leave", Game1.content.LoadString("Strings\\Locations:ScienceHouse_CarpenterMenu_Leave"))
            };
            Game1.currentLocation.createQuestionDialogue(Game1.content.LoadString("Strings\\Locations:AdventureGuild_Greeting"),
                options.ToArray(), "adventureGuild");
        }
        else
        {
            Utility.TryOpenShopMenu("AdventureShop", "Marlon");
        }
    }
}