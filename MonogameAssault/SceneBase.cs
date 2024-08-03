using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace MonogameAssault;

internal abstract class SceneBase
{
    internal abstract void LoadContent(ContentManager contentManager);
    internal abstract void Update(GameTime gameTime);
    internal abstract void Draw(GameTime gameTime);
    internal abstract void UnloadContent();
}