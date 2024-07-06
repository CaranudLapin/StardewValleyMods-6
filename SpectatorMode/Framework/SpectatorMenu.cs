using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using StardewValley;
using StardewValley.BellsAndWhistles;
using StardewValley.Menus;
using xTile.Dimensions;

namespace weizinai.StardewValleyMod.SpectatorMode.Framework;

internal class SpectatorMenu : IClickableMenu
{
    private readonly ModConfig config = ModEntry.Config;

    private bool followPlayer;

    private readonly Farmer targetFarmer;
    private GameLocation targetLocation;

    private readonly GameLocation originLocation;
    private readonly Location originViewport;

    public SpectatorMenu(GameLocation targetLocation, Farmer? targetFarmer = null, bool followPlayer = false)
    {
        // 初始化
        this.targetFarmer = targetFarmer ?? Game1.player;
        this.targetLocation = targetLocation;
        this.followPlayer = followPlayer;

        this.originLocation = Game1.currentLocation;
        this.originViewport = Game1.viewport.Location;

        // 切换视角
        Game1.globalFadeToBlack(this.Init);
    }

    public override void update(GameTime time)
    {
        if (this.followPlayer)
        {
            if (!this.targetLocation.Equals(this.targetFarmer.currentLocation))
            {
                this.InitLocationData(this.targetLocation, this.targetFarmer.currentLocation);
                // Game1.globalFadeToBlack(() => this.InitLocationData(this.targetLocation, this.targetFarmer.currentLocation));
                this.targetLocation = this.targetFarmer.currentLocation;
                // Game1.globalFadeToClear();
            }

            Game1.viewport.Location = this.GetViewportFromFarmer();
            Game1.panScreen(0, 0);
            return;
        }

        PanScreenHelper.PanScreen(this.config.MoveSpeed, this.config.MoveThreshold);
    }

    public override void draw(SpriteBatch b)
    {
        var title = this.followPlayer
            ? I18n.UI_SpectatorMode_Title(this.targetFarmer.displayName)
            : I18n.UI_SpectatorMode_Title(this.targetLocation.DisplayName);
        SpriteText.drawStringWithScrollCenteredAt(b, title, Game1.uiViewport.Width / 2, 64);
        
        if (this.config.ShowTimeAndMoney) Game1.dayTimeMoneyBox.draw(b);

        this.drawMouse(b);
    }

    public override void receiveKeyPress(Keys key)
    {
        base.receiveKeyPress(key);

        if (this.config.ToggleStateKey.JustPressed()) this.followPlayer = !this.followPlayer;
    }

    protected override void cleanupBeforeExit()
    {
        var locationRequest = Game1.getLocationRequest(this.originLocation.NameOrUniqueName);
        locationRequest.OnWarp += delegate
        {
            Game1.player.viewingLocation.Value = null;
            Game1.viewportFreeze = false;
            Game1.viewport.Location = this.originViewport;
            Game1.displayFarmer = true;
            Game1.displayHUD = true;
        };
        Game1.warpFarmer(locationRequest, Game1.player.TilePoint.X, Game1.player.TilePoint.Y, Game1.player.FacingDirection);
    }

    private void Init()
    {
        this.InitLocationData(Game1.currentLocation, this.targetLocation);
        Game1.displayFarmer = false;
        Game1.displayHUD = false;
        Game1.viewportFreeze = true;
        Game1.viewport.Location = this.GetInitialViewport();
        Game1.globalFadeToClear();
    }

    private void InitLocationData(GameLocation oldLocation, GameLocation newLocation)
    {
        oldLocation.cleanupBeforePlayerExit();
        Game1.currentLocation = newLocation;
        Game1.player.viewingLocation.Value = newLocation.NameOrUniqueName;
        newLocation.resetForPlayerEntry();
    }

    private Location GetInitialViewport()
    {
        if (this.followPlayer) return this.GetViewportFromFarmer();

        var layer = this.targetLocation.Map.Layers[0];
        return new Location(layer.LayerWidth / 2 * 64 - Game1.viewport.Width / 2, layer.LayerHeight / 2 * 64 - Game1.viewport.Height / 2);
    }

    private Location GetViewportFromFarmer()
    {
        var x = (int)this.targetFarmer.Position.X - Game1.viewport.Width / 2;
        var y = (int)this.targetFarmer.Position.Y - Game1.viewport.Height / 2;
        return new Location(x, y);
    }
}