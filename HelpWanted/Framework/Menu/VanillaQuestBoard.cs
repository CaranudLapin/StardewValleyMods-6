﻿using Common;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StardewValley;
using StardewValley.BellsAndWhistles;

namespace HelpWanted.Framework.Menu;

internal sealed class VanillaQuestBoard : BaseQuestBoard
{
    private readonly Texture2D billboardTexture;
    public static readonly List<QuestNote> QuestNotes = new();

    public VanillaQuestBoard(ModConfig config) : base(config)
    {
        // 背景逻辑
        billboardTexture = Game1.temporaryContent.Load<Texture2D>("LooseSprites/Billboard");

        // 初始化
        ShowingQuest = null;
        InitQuestNotes();
    }

    public override void performHoverAction(int x, int y)
    {
        // 关闭按钮逻辑
        upperRightCloseButton?.tryHover(x, y, 0.5f);

        if (ShowingQuest is null)
        {
            // 任务便签逻辑
            HoverTitle = "";
            HoverText = "";
            foreach (var option in QuestNotes.Where(option => option.containsPoint(x, y)))
            {
                HoverTitle = option.QuestData.Quest.questTitle;
                HoverText = option.QuestData.Quest.currentObjective;
                break;
            }
        }
        else
        {
            // 接受任务按钮逻辑
            var oldScale = AcceptQuestButton.scale;
            AcceptQuestButton.scale = AcceptQuestButton.bounds.Contains(x, y) ? 1.5f : 1f;
            if (AcceptQuestButton.scale > oldScale) Game1.playSound("Cowboy_gunshot");
        }
    }

    public override void receiveLeftClick(int x, int y, bool playSound = true)
    {
        // 如果当前没有展示任务面板,则处理OrderBillboard的鼠标左键点击事件
        if (ShowingQuest is null)
        {
            // 关闭按钮逻辑
            if (upperRightCloseButton != null && upperRightCloseButton.containsPoint(x, y))
            {
                if (playSound) Game1.playSound(closeSound);
                exitThisMenu();
            }

            // 任务便签逻辑
            foreach (var option in QuestNotes.Where(option => option.containsPoint(x, y)))
            {
                HoverTitle = "";
                HoverText = "";
                ShowingQuestID = option.myID;
                ShowingQuest = option.QuestData.Quest;
                AcceptQuestButton.visible = true;
                return;
            }
        }
        else
        {
            // 关闭按钮逻辑
            if (upperRightCloseButton != null && upperRightCloseButton.containsPoint(x, y))
            {
                if (playSound) Game1.playSound(closeSound);
                ShowingQuest = null;
                AcceptQuestButton.visible = false;
                return;
            }

            // 接受任务按钮逻辑
            if (AcceptQuestButton.containsPoint(x, y))
            {
                Game1.playSound("newArtifact");
                ShowingQuest.dayQuestAccepted.Value = Game1.Date.TotalDays;
                Game1.player.questLog.Add(ShowingQuest);
                QuestNotes.RemoveAll(option => option.myID == ShowingQuestID);
                ShowingQuest = null;
                AcceptQuestButton.visible = false;
            }
        }
    }

    public override void draw(SpriteBatch spriteBatch)
    {
        // 绘制阴影
        if (!Game1.options.showClearBackgrounds) spriteBatch.Draw(Game1.fadeToBlackRect, Game1.graphics.GraphicsDevice.Viewport.Bounds, Color.Black * 0.75f);

        // 绘制面板纹理
        spriteBatch.Draw(billboardTexture, new Vector2(xPositionOnScreen, yPositionOnScreen), new Rectangle(0, 0, 338, 198), Color.White,
            0f, Vector2.Zero, 4f, SpriteEffects.None, 1f);

        if (!QuestNotes.Any())
        {
            spriteBatch.DrawString(Game1.dialogueFont, Game1.content.LoadString("Strings\\UI:Billboard_NothingPosted"),
                new Vector2(xPositionOnScreen + 384, yPositionOnScreen + 320), Game1.textColor);
        }
        else
        {
            if (ShowingQuest is null)
                DrawQuestNotes(spriteBatch);
            else
                DrawShowingQuest(spriteBatch);
        }

        // 绘制星星
        for (var i = 0; i < Game1.stats.Get("BillboardQuestsDone") % 3; i++)
            spriteBatch.Draw(billboardTexture, Position + new Vector2(18 + 12 * i, 36f) * 4f, new Rectangle(140, 397, 10, 11), Color.White,
                0f, Vector2.Zero, 4f, SpriteEffects.None, 0.6f);

        // 绘制祝尼魔
        if (Game1.player.hasCompletedCommunityCenter())
            spriteBatch.Draw(billboardTexture, Position + new Vector2(290f, 59f) * 4f, new Rectangle(0, 427, 39, 54), Color.White, 0f,
                Vector2.Zero, 4f, SpriteEffects.None, 0.6f);

        // 绘制右上角的关闭按钮
        upperRightCloseButton.draw(spriteBatch);

        // 绘制鼠标
        Game1.mouseCursorTransparency = 1f;
        drawMouse(spriteBatch);

        // 绘制悬浮文本
        if (HoverText.Length > 0) drawHoverText(spriteBatch, HoverText, Game1.smallFont, 0, 0, -1, HoverTitle);
    }
    
    /// <summary>根据宽度和高度,获取一个没有被其他任务占用的矩形区域,该区域用于放置新的任务</summary>
    private Rectangle? GetFreeBounds(int width1, int height1)
    {
        // 如果宽度和高度大于面板的宽度和高度,则输出错误警告
        if (width1 >= BoardRect.Width || height1 >= BoardRect.Height)
        {
            Log.Warn($"note size {width1},{height1} is too big for the screen");
            return null;
        }

        // 设置最大尝试次数为10000
        var tries = 10000;
        while (tries > 0)
        {
            // 随机生成一个矩形区域
            var rectangle = new Rectangle(xPositionOnScreen + Game1.random.Next(BoardRect.X, BoardRect.Right - width1),
                yPositionOnScreen + Game1.random.Next(BoardRect.Y, BoardRect.Bottom - height1), width1, height1);
            // 遍历所有的可点击组件,计算是否有碰撞发生
            var collision = QuestNotes.Any(cc =>
                Math.Abs(cc.bounds.Center.X - rectangle.Center.X) < rectangle.Width * Config.XOverlapBoundary ||
                Math.Abs(cc.bounds.Center.Y - rectangle.Center.Y) < rectangle.Height * Config.YOverlapBoundary);
            // 如果碰撞发生,则尝试次数减1,否则返回矩形区域
            if (collision)
                tries--;
            else
                return rectangle;
        }

        // 如果尝试次数用完,还没有获得不冲突的矩形区域,则放回null
        return null;
    }

    private void DrawShowingQuest(SpriteBatch spriteBatch)
    {
        var font = LocalizedContentManager.CurrentLanguageCode == LocalizedContentManager.LanguageCode.ko ? Game1.smallFont : Game1.dialogueFont;
        var description = Game1.parseText(ShowingQuest!.questDescription, font, 640);
        // 任务描述逻辑
        Utility.drawTextWithShadow(spriteBatch, description, font, new Vector2(xPositionOnScreen + 320 + 32, yPositionOnScreen + 256), Game1.textColor, 1f, -1f,
            -1, -1, 0.5f);
        // 接受任务按钮逻辑
        drawTextureBox(spriteBatch, Game1.mouseCursors, new Rectangle(403, 373, 9, 9), AcceptQuestButton.bounds.X, AcceptQuestButton.bounds.Y,
            AcceptQuestButton.bounds.Width, AcceptQuestButton.bounds.Height, AcceptQuestButton.scale > 1f ? Color.LightPink : Color.White, 4f * AcceptQuestButton.scale);
        Utility.drawTextWithShadow(spriteBatch, Game1.content.LoadString("Strings\\UI:AcceptQuest"), Game1.dialogueFont,
            new Vector2(AcceptQuestButton.bounds.X + 12, AcceptQuestButton.bounds.Y + (LocalizedContentManager.CurrentLanguageLatin ? 16 : 12)), Game1.textColor);
        // 奖券逻辑
        if (Game1.stats.Get("BillboardQuestsDone") % 3 != 2) return;
        Utility.drawWithShadow(spriteBatch, Game1.content.Load<Texture2D>("TileSheets\\Objects_2"), Position + new Vector2(215f, 144f) * 4f,
            new Rectangle(80, 128, 16, 16), Color.White, 0f, Vector2.Zero, 4f);
        SpriteText.drawString(spriteBatch, "x1", (int)Position.X + 936, (int)Position.Y + 596);
    }

    private void DrawQuestNotes(SpriteBatch spriteBatch)
    {
        foreach (var questNote in QuestNotes) questNote.Draw(spriteBatch);
    }

    private void InitQuestNotes()
    {
        if (QuestManager.VanillaQuestList.Count <= 0) return;

        // 清空任务选项列表
        QuestNotes.Clear();
        // 遍历所有的任务数据,创建任务选项
        var questList = QuestManager.VanillaQuestList;
        for (var i = 0; i < questList.Count; i++)
        {
            var size = new Point(
                (int)(questList[i].PadSource.Width * Config.NoteScale),
                (int)(questList[i].PadSource.Height * Config.NoteScale));
            var bounds = GetFreeBounds(size.X, size.Y);
            if (bounds is null)
            {
                Log.Trace($"任务面板没有空间放置任务了({i + 1}/{questList.Count})");
                break;
            }

            QuestNotes.Add(new QuestNote(questList[i], bounds.Value)
            {
                // 设置该选项的ID
                myID = OptionIndex - i,
            });
            Log.Trace("成功添加一个任务到原版任务面板");
        }

        QuestManager.VanillaQuestList.Clear();
    }
}