﻿namespace HelpWanted.Framework;

public class ModConfig
{
    // 一般设置
    public bool QuestFirstDay { get; set; }
    public bool QuestFestival { get; set; }
    public float DailyQuestChance { get; set; } = 0.9f;
    public bool OneQuestPerVillager { get; set; } = true;
    public bool AvoidMaxHearts { get; set; } = true;
    public int QuestDays { get; set; } = 2;
    public int MaxQuests { get; set; } = 10;

    // 交易任务
    public float ItemDeliveryWeight { get; set; } = 0.4f;
    public bool MustLikeItem { get; set; } = true;
    public bool MustLoveItem { get; set; }
    public bool AllowArtisanGoods { get; set; } = true;
    public int MaxPrice { get; set; } = -1;
    public bool IgnoreVanillaItemRestriction { get; set; } = true;

    // 采集任务
    public float ResourceCollectionWeight { get; set; } = 0.08f;
    
    // 钓鱼任务
    public float FishingWeight { get; set; } = 0.07f;

    // 杀怪任务
    public float SlayMonstersWeight { get; set; } = 0.1f;

    #region 外观

    // 便签缩放
    public float NoteScale { get; set; } = 2f;

    // 便签重叠率
    public float XOverlapBoundary { get; set; } = 0.5f;

    public float YOverlapBoundary { get; set; } = 0.25f;

    // 随机颜色通道
    public int RandomColorMin { get; set; } = 150;

    public int RandomColorMax { get; set; } = 255;

    // 肖像缩放
    public float PortraitScale { get; set; } = 1f;

    // 肖像偏移
    public int PortraitOffsetX { get; set; } = 32;

    public int PortraitOffsetY { get; set; } = 64;

    // 肖像色调
    public int PortraitTintR { get; set; } = 150;
    public int PortraitTintG { get; set; } = 150;
    public int PortraitTintB { get; set; } = 150;
    public int PortraitTintA { get; set; } = 150;

    #endregion
}