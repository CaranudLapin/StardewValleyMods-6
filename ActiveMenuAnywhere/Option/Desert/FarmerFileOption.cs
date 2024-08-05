﻿using StardewValley;
using weizinai.StardewValleyMod.ActiveMenuAnywhere.Framework;

namespace weizinai.StardewValleyMod.ActiveMenuAnywhere.Option;

internal class FarmerFileOption : BaseOption
{
    public FarmerFileOption() : base(I18n.UI_Option_FarmerFile(), GetSourceRectangle(3)) { }

    public override bool IsEnable()
    {
        return Game1.player.mailReceived.Contains("ccVault") && Game1.player.hasClubCard;
    }

    public override void Apply()
    {
        Game1.currentLocation.farmerFile();
    }
}