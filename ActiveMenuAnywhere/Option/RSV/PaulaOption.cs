﻿using weizinai.StardewValleyMod.ActiveMenuAnywhere.Framework;

namespace weizinai.StardewValleyMod.ActiveMenuAnywhere.Option;

internal class PaulaOption : BaseOption
{
    public PaulaOption() : base(I18n.UI_Option_Paula(), GetSourceRectangle(3)) { }

    public override void Apply()
    {
        RSVReflection.GetRSVPrivateStaticMethod("RidgesideVillage.PaulaClinic", "ClinicChoices").Invoke(null, null);
    }
}