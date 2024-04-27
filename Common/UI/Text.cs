using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StardewValley;

namespace Common.UI;

public sealed class Text : Element
{
    private readonly Color color;

    private readonly SpriteFont font;
    private readonly float scale;
    private readonly string text;

    public Text(string text, Vector2 localPosition, SpriteFont? font = null, Color? color = null, float scale = 1f)
    {
        // this.font = font ?? Game1.temporaryContent.Load<SpriteFont>("Fonts/SpriteFont1");
        this.font = font ?? Game1.dialogueFont;
        this.text = text;
        LocalPosition = localPosition;
        this.color = color ?? Game1.textColor;
        this.scale = scale;
    }

    public override int Width => (int)GetTextSize().X;
    protected override int Height => (int)GetTextSize().Y;

    public override void Draw(SpriteBatch spriteBatch)
    {
        if (IsHidden()) return;


        spriteBatch.DrawString(font, text, Position, color, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
    }

    private Vector2 GetTextSize()
    {
        return font.MeasureString(text) * scale;
    }
}