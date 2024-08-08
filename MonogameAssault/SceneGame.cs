using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonogameAssault.AssetManagement;
using MonogameAssault.Camera2d;
using MonogameAssault.CoreComponents;
using MonogameAssault.DebugStuff;

namespace MonogameAssault;

internal sealed class SceneGame : SceneBase
{
    private EntityManager _entityManager;

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
        const float CAMERA_SPEED = 1f;

        if (Keyboard.GetState().IsKeyDown(Keys.W) || Keyboard.GetState().IsKeyDown(Keys.Up))
        {
            CameraHelper.CameraMatrix *= Matrix.CreateTranslation(0, CAMERA_SPEED, 0);
        }
        if (Keyboard.GetState().IsKeyDown(Keys.S) || Keyboard.GetState().IsKeyDown(Keys.Down))
        {
            CameraHelper.CameraMatrix *= Matrix.CreateTranslation(0, -CAMERA_SPEED, 0);
        }
        if (Keyboard.GetState().IsKeyDown(Keys.A) || Keyboard.GetState().IsKeyDown(Keys.Left))
        {
            CameraHelper.CameraMatrix *= Matrix.CreateTranslation(CAMERA_SPEED, 0, 0);
        }
        if (Keyboard.GetState().IsKeyDown(Keys.D) || Keyboard.GetState().IsKeyDown(Keys.Right))
        {
            CameraHelper.CameraMatrix *= Matrix.CreateTranslation(-CAMERA_SPEED, 0, 0);
        }

        _entityManager.Update(gameTime);
        _topPositionGameWindows = CameraHelper.GetTopPositionScreen(ref _topPositionGameWindows, ref CameraHelper.CameraMatrix);
    }

    internal override void Draw(GameTime gameTime)
    {
        //TODO draw only visible entities based on camera
        ReadOnlySpan<Vector2> positionsSpan = new(_entityManager.positions);
        for (int i = 0; i < EntityManager.ENEMY_COUNT; i++)
        {
            var position = positionsSpan[i];
            var actorKind = _entityManager.actorKinds[i];
            var sourceRectangle = TextureAtlasHelper.Enemies.SourceRectangleWasp1;
            switch (actorKind)
            {
                case ActorKind.EnemyWasp:
                    sourceRectangle = TextureAtlasHelper.Enemies.SourceRectangleWasp1;
                    break;
                default:
                    DebugStuff.DebugHelper.Log(LogLevel.Error, $"ActorKind {actorKind} not implemented in SceneGame.Draw");
                    break;
            }

            AssaultGame.SpriteBatch.Draw(texture: AssaultGame.TextureAtlas,
                sourceRectangle: TextureAtlasHelper.Enemies.SourceRectangleWasp1,
                                         position: position,
                                         color: Color.White);
        }

#if DEBUG

        AssaultGame.SpriteBatch.DrawString(spriteFont: AssaultGame.DebugFont
            , text: $"FPS: {AssaultGame.FrameRate}"
            , position: _topPositionGameWindows, color: Color.Blue);
#endif

    }

    internal override void UnloadContent()
    {
        AssaultGame.TextureAtlas.Dispose();
    }
}
