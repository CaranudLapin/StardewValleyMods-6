using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StardewValley;
using StardewValley.Menus;

namespace Common.UI;

public class Button : Element
{
    private const int ContentPadding = 16;

    private readonly Texture2D texture;
    private readonly Rectangle sourceRectangle;
    public Color TextureColor;
    private readonly string text;
    private readonly SpriteFont font;
    private readonly Color textColor;
    private readonly float scale;
    
    protected override int Width => (int)GetTextSize().X + ContentPadding * 2;
    protected override int Height => (int)GetTextSize().Y + ContentPadding * 2;
    
    public Button(string text, Vector2 localPosition, float scale = 1f) :
        this(Game1.menuTexture, new Rectangle(0, 256, 60, 60), Color.White,
            text, Game1.smallFont, Game1.textColor, localPosition, scale)
    {
    }
    

    public Button(Texture2D texture, Rectangle sourceRectangle, Color textureColor, string text, SpriteFont font, Color textColor,Vector2 localPosition, float scale = 1f)
    {
        this.texture = texture;
        this.sourceRectangle = sourceRectangle;
        TextureColor = textureColor;
        this.text = text;
        this.font = font;
        this.textColor = textColor;
        LocalPosition = localPosition;
        this.scale = scale;
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        IClickableMenu.drawTextureBox(spriteBatch, texture, sourceRectangle, (int)Position.X, (int)Position.Y, Width, Height, TextureColor, 1f, false);
        spriteBatch.DrawString(font, text, Position + new Vector2(ContentPadding, ContentPadding), textColor, 
            0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
    }

    private Vector2 GetTextSize()
    {
        return font.MeasureString(text) * scale;
    }
}