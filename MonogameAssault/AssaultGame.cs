using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MonogameAssault;

public sealed class AssaultGame : Game
{
#pragma warning disable IDE0052 // Remove unread private members
    private readonly GraphicsDeviceManager _graphics;
#pragma warning restore IDE0052 // Remove unread private members

    internal static SpriteBatch _spriteBatch; //only one to rule them all!
    internal static Texture2D _texureAtlas;//TODO only one to rule them all, again!, move to resource handler?
    private SceneManager _sceneManager;

    public AssaultGame()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        _sceneManager = new SceneManager(new SceneGame(), Content); //TODO : change to SceneMenu when implemented
    }

    protected override void Update(GameTime gameTime)
    {
        if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        _sceneManager.Update(gameTime);

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        _spriteBatch.Begin();
        _sceneManager.Draw(gameTime);
        _spriteBatch.End();

        base.Draw(gameTime);
    }
}
