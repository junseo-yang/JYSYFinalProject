using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace JYSYFinalProject
{
    public class Character : DrawableGameComponent
    {
        // Declare Fields
        private SpriteBatch spriteBatch;
        private Texture2D tex;
        private Vector2 position;
        // dimension of a frame (64 * 64 in this case)
        private Vector2 dimension;
        // list of srcRect
        private Rectangle[,] frames;
        // draw frame index, list has indexer
        public int frameIndexRow = -1;
        private int frameIndexCol = -1;

        // fixed delay
        private int delay;
        // fluctulating value
        private int delayCounter;

        private const int ROW = 8;
        private const int COL = 8;

        enum Direction
        {
            Right,
            Up,
            UpRight,
            UpLeft,
            Down,
            DownRight,
            DownLeft,
            Left
        }

        public Character(Game game,
            Vector2 position,
            int delay
            ) : base(game)
        {
            Game1 g = (Game1)game;

            this.spriteBatch = g._spriteBatch;
            this.tex = g.Content.Load<Texture2D>("images/Character/Character_8_directional");
            this.position = position;
            this.delay = delay;

            // Calculation
            this.dimension = new Vector2(tex.Width / COL, tex.Height / ROW);            

            CreateFrames();
        }

        private void CreateFrames()
        {
            frames = new Rectangle[ROW, COL];
            for (int i = 0; i < ROW; i++)
            {
                for (int j = 0; j < COL; j++)
                {
                    int x = (int)(j * dimension.X);
                    int y = (int)(i * dimension.Y);
                    Rectangle r = new Rectangle(x, y, (int)dimension.X, (int)dimension.Y);
                    frames[i, j] = r;
                }
            }
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            // Safety Condition
            if (frameIndexRow >= 0)
            {
                if (frameIndexCol >= 0)
                {
                    spriteBatch.Draw(tex, position, frames[frameIndexRow, frameIndexCol], Color.White);
                }
                else
                {
                    spriteBatch.Draw(tex, position, frames[frameIndexRow, 0], Color.White);
                }
            }
            else
            {
                spriteBatch.Draw(tex, position, frames[(int)Direction.Up, 0], Color.White);
            }
            spriteBatch.End();

            base.Draw(gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            KeyboardState ks = Keyboard.GetState();

            if (ks.IsKeyUp(Keys.Left) && ks.IsKeyUp(Keys.Up) && ks.IsKeyDown(Keys.Right) && ks.IsKeyUp(Keys.Down))
            {
                frameIndexRow = (int)Direction.Right;
            }
            if (ks.IsKeyUp(Keys.Left) && ks.IsKeyDown(Keys.Up) && ks.IsKeyUp(Keys.Right) && ks.IsKeyUp(Keys.Down))
            {
                frameIndexRow = (int)Direction.Up;
            }
            if (ks.IsKeyUp(Keys.Left) && ks.IsKeyDown(Keys.Up) && ks.IsKeyDown(Keys.Right) && ks.IsKeyUp(Keys.Down))
            {
                frameIndexRow = (int)Direction.UpRight;
            }
            if (ks.IsKeyDown(Keys.Left) && ks.IsKeyDown(Keys.Up) && ks.IsKeyUp(Keys.Right) && ks.IsKeyUp(Keys.Down))
            {
                frameIndexRow = (int)Direction.UpLeft;
            }
            if (ks.IsKeyUp(Keys.Left) && ks.IsKeyUp(Keys.Up) && ks.IsKeyUp(Keys.Right) && ks.IsKeyDown(Keys.Down))
            {
                frameIndexRow = (int)Direction.Down;
            }
            if (ks.IsKeyUp(Keys.Left) && ks.IsKeyUp(Keys.Up) && ks.IsKeyDown(Keys.Right) && ks.IsKeyDown(Keys.Down))
            {
                frameIndexRow = (int)Direction.DownRight;
            }
            if (ks.IsKeyDown(Keys.Left) && ks.IsKeyUp(Keys.Up) && ks.IsKeyUp(Keys.Right) && ks.IsKeyDown(Keys.Down))
            {
                frameIndexRow = (int)Direction.DownLeft;
            }
            if (ks.IsKeyDown(Keys.Left) && ks.IsKeyUp(Keys.Up) && ks.IsKeyUp(Keys.Right) && ks.IsKeyUp(Keys.Down))
            {
                frameIndexRow = (int)Direction.Left;
            }
            if (ks.IsKeyUp(Keys.Left) && ks.IsKeyUp(Keys.Up) && ks.IsKeyUp(Keys.Right) && ks.IsKeyUp(Keys.Down))
            {
                frameIndexCol = -1;
            }

            // Increase delayCounter
            delayCounter++;
            // If delaycounter is greater than delay, then frameIndex++
            if (delayCounter > delay)
            {
                frameIndexCol++;

                // 12.4.	Prevent frameIndex increases beyond  maximum value, Initilaize, Hide
                if (frameIndexCol >= COL)
                {
                    frameIndexCol = 0;
                }

                delayCounter = 0;
            }

            base.Update(gameTime);
        }
    }
}
