using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonogameAssault.Camera2d;

namespace MonogameAssault;

public sealed class AssaultGame : Game
{
    internal static SpriteBatch SpriteBatch; //only one to rule them all!
    internal static Texture2D TextureAtlas;
    internal static float FrameRate;
    internal static Random Random = new();
    internal static SpriteFont DebugFont;
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
        _graphics.PreferredBackBufferWidth = GameInfo.Windows.VIRTUAL_RESOLUTION_WIDTH;
        _graphics.PreferredBackBufferHeight = GameInfo.Windows.VIRTUAL_RESOLUTION_HEIGHT;
        IsFixedTimeStep = false;
        _graphics.IsFullScreen = false;
        _graphics.ApplyChanges();
        IsMouseVisible = true;
        Window.AllowUserResizing = true;
        Window.ClientSizeChanged += (sender, args) => {
            RecalculatedResolution();
        };

        CameraHelper.CameraMatrix = Matrix.Identity;

        RecalculatedResolution();

        base.Initialize();
    }

    protected override void Dispose(bool disposing)
    {
        Window.ClientSizeChanged -= (sender, args) => {
            RecalculatedResolution();
        };
        base.Dispose(disposing);
    }

    protected override void LoadContent()
    {
        SpriteBatch = new(GraphicsDevice);
        _sceneManager = new(new SceneGame(), Content);
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

        SpriteBatch.Begin(samplerState: SamplerState.PointClamp
            , transformMatrix: CameraHelper.CameraMatrix * _scaleResolutionMatrix);
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

        if (screenWidth / GameInfo.Windows.DRAW_RESOLUTION_WIDTH > screenHeight / GameInfo.Windows.DRAW_RESOLUTION_HEIGHT)
        {
            float aspectRatio = screenHeight / GameInfo.Windows.DRAW_RESOLUTION_HEIGHT;
            GameInfo.Windows.VIRTUAL_RESOLUTION_WIDTH = (int)(aspectRatio * GameInfo.Windows.DRAW_RESOLUTION_WIDTH);
            GameInfo.Windows.VIRTUAL_RESOLUTION_HEIGHT = (int)(screenHeight);

        }
        else
        {
            float aspectRatio = screenWidth / GameInfo.Windows.DRAW_RESOLUTION_WIDTH;
            GameInfo.Windows.VIRTUAL_RESOLUTION_WIDTH = (int)(screenWidth);
            GameInfo.Windows.VIRTUAL_RESOLUTION_HEIGHT = (int)(aspectRatio * GameInfo.Windows.DRAW_RESOLUTION_HEIGHT);

        }

        _scaleResolutionMatrix = Matrix.CreateScale(GameInfo.Windows.VIRTUAL_RESOLUTION_WIDTH / (float)GameInfo.Windows.DRAW_RESOLUTION_WIDTH);

        _viewport = new Viewport {
            X = (int)(screenWidth / 2 - GameInfo.Windows.VIRTUAL_RESOLUTION_WIDTH / 2),
            Y = (int)(screenHeight / 2 - GameInfo.Windows.VIRTUAL_RESOLUTION_HEIGHT / 2),
            Width = GameInfo.Windows.VIRTUAL_RESOLUTION_WIDTH,
            Height = GameInfo.Windows.VIRTUAL_RESOLUTION_HEIGHT,
            MinDepth = 0,
            MaxDepth = 1,//TODO research more depth about this
        };
    }

}
