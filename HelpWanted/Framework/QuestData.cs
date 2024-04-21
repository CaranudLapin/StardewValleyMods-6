﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StardewValley;
using StardewValley.Quests;

namespace HelpWanted.Framework;

public class QuestData
{
    public QuestData(QuestType questType, NPC npc)
    {
        var config = ModEntry.Config;
        PadTexture = ModEntry.GetPadTexture(npc.Name, questType.ToString());;
        PadTextureSource = new Rectangle(0, 0, 64, 64);
        PadColor = ModEntry.GetRandomColor();
        PinTexture = ModEntry.GetPinTexture(npc.Name, questType.ToString());
        PinTextureSource = new Rectangle(0, 0, 64, 64);
        PinColor = ModEntry.GetRandomColor();
        Icon = npc.Portrait;
        IconSource = new Rectangle(0,0,64,64);
        IconColor = new Color(config.PortraitTintR, config.PortraitTintG, config.PortraitTintB, config.PortraitTintA);
        IconScale = config.PortraitScale;
        IconOffset = new Point(config.PortraitOffsetX, config.PortraitOffsetY);
        Quest = Game1.questOfTheDay;
    }

    public Texture2D PadTexture { get; set; }
    public Rectangle PadTextureSource { get; set; }
    public Color PadColor { get; set; }
    public Texture2D PinTexture { get; set; }
    public Rectangle PinTextureSource { get; set; }
    public Color PinColor { get; set; }
    public Texture2D Icon { get; set; }
    public Rectangle IconSource { get; set; }
    public Color IconColor { get; set; }
    public float IconScale { get; set; }
    public Point IconOffset { get; set; }
    public Quest Quest { get; set; }
    
    
}

public enum QuestType
{
    ItemDelivery,
    ResourceCollection,
    SlayMonster,
    Fishing
}