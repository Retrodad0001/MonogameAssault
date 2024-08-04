using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace MonogameAssault;

public sealed class AssaultGame : Game
{
    internal static SpriteBatch SpriteBatch; //only one to rule them all!
    internal static Texture2D TextureAtlas;//TODO only one to rule them all, again!, move to resource handler?
    internal static float FrameRate; //TODO move to better location
    internal static Random Random = new();//TODO move to better location
    internal static SpriteFont DebugFont;//TODO move to better location
    private SceneManager _sceneManager;
    private readonly GraphicsDeviceManager _graphics;

    public AssaultGame()
    {
        _graphics = new(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        _graphics.PreferredBackBufferWidth = Constants.Windows.SCREEN_WIDTH;
        _graphics.PreferredBackBufferHeight = Constants.Windows.SCREEN_HEIGHT;
        IsFixedTimeStep = false;
        _graphics.ApplyChanges();
        base.Initialize();
    }

    protected override void LoadContent()
    {
        SpriteBatch = new(GraphicsDevice);
        _sceneManager = new(new SceneGame(), Content); //TODO : change to SceneMenu when implemented
    }

    protected override void Update(GameTime gameTime)
    {
        FrameRate = 1 / (float)gameTime.ElapsedGameTime.TotalSeconds;

        if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        _sceneManager.Update(gameTime);

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        SpriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, null, null);
        _sceneManager.Draw(gameTime);
        SpriteBatch.End();

        base.Draw(gameTime);
    }
}
