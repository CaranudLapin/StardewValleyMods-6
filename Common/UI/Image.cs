using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Common.UI;

public class Image : Element
{
    private readonly Texture2D texture;
    private readonly Rectangle sourceRectangle;
    private readonly Color color;
    private readonly float scale;

    protected override int Width => (int)GetImageSize().X;
    protected override int Height => (int)GetImageSize().Y;

    public Image(Texture2D texture, Vector2 localPosition, Rectangle sourceRectangle, Color? color = null, float scale = 1f)
    {
        this.texture = texture;
        LocalPosition = localPosition;
        this.sourceRectangle = sourceRectangle;
        this.color = color ?? Color.White;
        this.scale = scale;
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        if (IsHidden()) return;
        
        spriteBatch.Draw(texture, Position, sourceRectangle, color, 0, Vector2.Zero, scale, SpriteEffects.None, -1);
    }

    private Vector2 GetImageSize()
    {
        return new Vector2(sourceRectangle.Width, sourceRectangle.Height) * scale;
    }
}