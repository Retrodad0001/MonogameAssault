using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace MonogameAssault;

internal sealed class SceneGame : SceneBase
{
    //TODO private Matrix _camera;
    private EntityManager _entityManager;
    private Rectangle _sourceRectangleWasp = new Rectangle(163, 483, 125, 16);
    private Vector2 _topPositionGameWindows = Vector2.One;


    internal SceneGame()
    {
    }

    internal override void LoadContent(ContentManager contentManager)
    {
        AssaultGame.TextureAtlas = contentManager.Load<Texture2D>(assetName: "TextureAtlas");
        AssaultGame.DebugFont = contentManager.Load<SpriteFont>(assetName: "DebugFont");
        _entityManager = new();
    }

    internal override void Update(GameTime gameTime)
    {
        _entityManager.Update(gameTime);
        _topPositionGameWindows = GameStatics.Windows.GetTopPositionScreen(ref _topPositionGameWindows);
    }

    internal override void Draw(GameTime gameTime)
    {
        //TODO draw only visible entities based on camera (occlusion)
        ReadOnlySpan<Vector2> positionsSpan = new(_entityManager.CurrentPositions);
        for (int i = 0; i < EntityManager.ENEMY_COUNT; i++)
        {
            var position = positionsSpan[i];

            AssaultGame.SpriteBatch.Draw(texture: AssaultGame.TextureAtlas,
                sourceRectangle: _sourceRectangleWasp,
                                         position: position,
                                         color: Color.White);
        }

        AssaultGame.SpriteBatch.DrawString(spriteFont: AssaultGame.DebugFont
            , text: $"FPS: {AssaultGame.FrameRate}" //TODO do with span trick
            , position: _topPositionGameWindows, color: Color.Blue);
    }

    internal override void UnloadContent()
    {
        AssaultGame.TextureAtlas.Dispose();
    }
}