﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StardewValley;
using StardewValley.Locations;
using StardewValley.Menus;

namespace ActiveMenuAnywhere.Framework.ActiveMenu;

public class CommunityCenterMenu : BaseActiveMenu
{
    private readonly List<string> keys;
    private readonly List<string> texts;

    public CommunityCenterMenu(Rectangle bounds, Texture2D texture, Rectangle sourceRect) : base(bounds, texture, sourceRect)
    {
        keys = new List<string>() { "Pantry", "CraftsRoom", "FishTank", "BoilerRoom", "Vault", "Bulletin" };
        texts = new List<string>()
        {
            Game1.content.LoadString("Strings\\Locations:CommunityCenter_AreaName_Pantry"),
            Game1.content.LoadString("Strings\\Locations:CommunityCenter_AreaName_CraftsRoom"),
            Game1.content.LoadString("Strings\\Locations:CommunityCenter_AreaName_FishTank"),
            Game1.content.LoadString("Strings\\Locations:CommunityCenter_AreaName_BoilerRoom"),
            Game1.content.LoadString("Strings\\Locations:CommunityCenter_AreaName_Vault"),
            Game1.content.LoadString("Strings\\Locations:CommunityCenter_AreaName_BulletinBoard"),
        };
    }

    public override void ReceiveLeftClick()
    {
        if (!Game1.player.mailReceived.Contains("JojaMember") && Game1.player.mailReceived.Contains("canReadJunimoText"))
            CheckBundle();
        else
            Game1.drawObjectDialogue(I18n.Tip_Unavailable());
    }

    private void CheckBundle()
    {
        var communityCenter = Game1.RequireLocation<CommunityCenter>("CommunityCenter");
        var options = new List<Response>();
        for (var i = 0; i < 6; i++)
        {
            if (communityCenter.shouldNoteAppearInArea(i))
                options.Add(new Response(keys[i], texts[i]));
        }

        options.Add(new Response("Leave", Game1.content.LoadString("Strings\\Locations:AnimalShop_Marnie_Leave")));

        Game1.currentLocation.createQuestionDialogue("", options.ToArray(), AfterDialogueBehavior);
    }

    private void AfterDialogueBehavior(Farmer who, string whichAnswer)
    {
        if (whichAnswer == "Leave")
            Game1.exitActiveMenu();
        var communityCenter = Game1.RequireLocation<CommunityCenter>("CommunityCenter");
        communityCenter.checkBundle(keys.IndexOf(whichAnswer));
    }
}