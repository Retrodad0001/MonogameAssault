using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace MonogameAssault;

internal sealed class SceneGame : SceneBase
{

    //TODO private Camera _camera;
    private EntityManager _entityManager;
    private Vector2 _positionDebugFont = new Vector2(10, 10);
    private Rectangle _sourceRectangle = new Rectangle(163, 483, 125, 16);


    internal override void LoadContent(ContentManager contentManager)
    {
        AssaultGame.TextureAtlas = contentManager.Load<Texture2D>(assetName: "TextureAtlas");
        AssaultGame.DebugFont = contentManager.Load<SpriteFont>(assetName: "DebugFont");

        //TODO   _camera = new Camera();
        _entityManager = new();
    }

    internal override void Update(GameTime gameTime)
    {
        _entityManager.Update(gameTime);
    }

    internal override void Draw(GameTime gameTime)
    {
        /*
         * "filename": "Wasp.png",
          "frame": {"x":163,"y":483,"w":125,"h":16},
           "rotated": false,
          "trimmed": true,
          "spriteSourceSize": {"x":0,"y":0,"w":125,"h":16},
          "sourceSize": {"w":128,"h":16}
         */

        ReadOnlySpan<Vector2> positionsSpan = new(_entityManager.Positions);
        for (int i = 0; i < EntityManager.EnemyCount; i++)
        {
            var position = positionsSpan[i];

            AssaultGame.SpriteBatch.Draw(texture: AssaultGame.TextureAtlas,
                sourceRectangle: _sourceRectangle,
                                         position: position,
                                         color: Color.White);
        }

        AssaultGame.SpriteBatch.DrawString(spriteFont: AssaultGame.DebugFont
            , text: $"FPS: {AssaultGame.FrameRate}"
            , position: _positionDebugFont, color: Color.Red);
    }

    internal override void UnloadContent()
    {
        AssaultGame.TextureAtlas.Dispose();
    }
}