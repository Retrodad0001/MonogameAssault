using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

internal sealed class SceneManager
{
    private SceneBase _currentScene;
    private readonly ContentManager contentManager;

    internal SceneManager(SceneBase initialScene, ContentManager contentManager)
    {
        _currentScene = initialScene;
        this.contentManager = contentManager;
        _currentScene.LoadContent(contentManager);
    }

    internal void Update(GameTime gameTime)
    {
        _currentScene.Update(gameTime: gameTime);
    }

    internal void Draw(GameTime gameTime)
    {
        _currentScene.Draw(gameTime: gameTime);
    }

    internal void ChangeScene(SceneBase newScene)
    {
        _currentScene.UnloadContent();
        _currentScene = newScene;
        _currentScene.LoadContent(contentManager);
    }
}
