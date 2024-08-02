using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonogameAssault;

internal sealed class SceneGame : SceneBase
{
    internal override void LoadContent(ContentManager contentManager)
    {

        AssaultGame._texureAtlas = contentManager.Load<Texture2D>("TextureAtlas");
    }

    internal override void Update(GameTime gameTime)
    {

    }

    internal override void Draw(GameTime gameTime)
    {
        AssaultGame._spriteBatch.Draw(AssaultGame._texureAtlas, new Vector2(0, 0), Color.White);
    }

    internal override void UnloadContent()
    {

    }
}