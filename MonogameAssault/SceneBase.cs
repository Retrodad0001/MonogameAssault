using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

internal abstract class SceneBase
{
    internal abstract void LoadContent(ContentManager contentManager);
    internal abstract void Update(GameTime gameTime);
    internal abstract void Draw(GameTime gameTime);
    internal abstract void UnloadContent();
}
