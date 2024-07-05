﻿using Microsoft.Xna.Framework;
using StardewValley;
using StardewValley.Menus;
using weizinai.StardewValleyMod.ActiveMenuAnywhere.Framework;

namespace weizinai.StardewValleyMod.ActiveMenuAnywhere.Option;

internal class PrizeTicketOption : BaseOption
{
    public PrizeTicketOption(Rectangle sourceRect) :
        base(I18n.UI_Option_PrizeTicket(), sourceRect) { }

    public override void Apply()
    {
        Game1.activeClickableMenu = new PrizeTicketMenu();
    }
}