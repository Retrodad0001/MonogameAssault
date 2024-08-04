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

    private Matrix _scaleResolutionMatrix;
    private Viewport _viewport;

    public AssaultGame()
    {
        _graphics = new(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        _graphics.PreferredBackBufferWidth = GameStatics.Windows.VIRTUAL_RESOLUTION_WIDTH;
        _graphics.PreferredBackBufferHeight = GameStatics.Windows.VIRTUAL_RESLUTION_HEIGHT;
        IsFixedTimeStep = false;
        _graphics.IsFullScreen = false;
        _graphics.ApplyChanges();
        IsMouseVisible = true;
        Window.AllowUserResizing = true;
        Window.ClientSizeChanged += (sender, args) =>
        {
            RecalculatedResolution();
        };


        RecalculatedResolution();

        base.Initialize();
    }

    protected override void Dispose(bool disposing)
    {
        Window.ClientSizeChanged -= (sender, args) =>
        {
            RecalculatedResolution();
        };
        base.Dispose(disposing);
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
        GraphicsDevice.Clear(Color.Black);
        GraphicsDevice.Viewport = _viewport;

        SpriteBatch.Begin(samplerState: SamplerState.PointClamp, transformMatrix: _scaleResolutionMatrix);
        _sceneManager.Draw(gameTime);
        SpriteBatch.End();

        base.Draw(gameTime);
    }

    private void RecalculatedResolution()
    {
        /* Aristurtle Dev channel:
         * Scale Matrix Independent Resolution Rendering in MonoGame #monogame #gamedev : 
         * https://www.youtube.com/watch?v=BVSSQKlYipo 
         * */

        float screenWidth = GraphicsDevice.PresentationParameters.BackBufferWidth;
        float screenHeight = GraphicsDevice.PresentationParameters.BackBufferHeight;

        if (screenWidth / GameStatics.Windows.DRAW_RESOLUTION_WIDTH > screenHeight / GameStatics.Windows.DRAW_RESOLUTION_HEIGHT)
        {
            float aspectRatio = screenHeight / GameStatics.Windows.DRAW_RESOLUTION_HEIGHT;
            GameStatics.Windows.VIRTUAL_RESOLUTION_WIDTH = (int)(aspectRatio * GameStatics.Windows.DRAW_RESOLUTION_WIDTH);
            GameStatics.Windows.VIRTUAL_RESLUTION_HEIGHT = (int)(screenHeight);

        }
        else
        {
            float aspectRatio = screenWidth / GameStatics.Windows.DRAW_RESOLUTION_WIDTH;
            GameStatics.Windows.VIRTUAL_RESOLUTION_WIDTH = (int)(screenWidth);
            GameStatics.Windows.VIRTUAL_RESLUTION_HEIGHT = (int)(aspectRatio * GameStatics.Windows.DRAW_RESOLUTION_HEIGHT);

        }

        _scaleResolutionMatrix = Matrix.CreateScale(GameStatics.Windows.VIRTUAL_RESOLUTION_WIDTH / (float)GameStatics.Windows.DRAW_RESOLUTION_WIDTH);

        _viewport = new Viewport
        {
            X = (int)(screenWidth / 2 - GameStatics.Windows.VIRTUAL_RESOLUTION_WIDTH / 2),
            Y = (int)(screenHeight / 2 - GameStatics.Windows.VIRTUAL_RESLUTION_HEIGHT / 2),
            Width = GameStatics.Windows.VIRTUAL_RESOLUTION_WIDTH,
            Height = GameStatics.Windows.VIRTUAL_RESLUTION_HEIGHT,
            MinDepth = 0,
            MaxDepth = 1,//TODO resarch more about this
        };
    }

}
