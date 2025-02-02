using weizinai.StardewValleyMod.Common.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace weizinai.StardewValleyMod.TestMod.Framework;

public class TestUI
{
    private readonly RootElement ui = new();

    public TestUI()
    {
        var text = new Text("Hello, world!", new Vector2(100, 100));
        var button = new Button(I18n.UI_Button_Name(), new Vector2(200, 200))
        {
            OnHover = (element, _) => (element as Button)!.TextureColor = Color.White * 0.5f,
            OffHover = element => (element as Button)!.TextureColor = Color.White
        };
        this.ui.AddChild(text, button);
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        this.ui.Draw(spriteBatch);
        this.ui.PerformHoverAction(spriteBatch);
    }

    public void Update()
    {
        this.ui.Update();
    }
}