using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace JYSYFinalProject
{
    public class Bow : DrawableGameComponent
    {
        private SpriteBatch spriteBatch;
        private Texture2D tex;
        public Vector2 position;
        private Vector2 speed;

        private Rectangle[] frames;
        public Vector2 origin;

        public float rotation = 0f;
        private float scale = 1.0f;

        private MouseState oldMouseState;

        private int delay = 1;
        public int frameIndexCol = 0;
        private int delayCounter = 0;

        private const int COL = 10;

        public Bow(Game game,
       SpriteBatch spriteBatch,
       Texture2D tex) : base(game)
        {
            this.spriteBatch = spriteBatch;
            this.tex = tex;
            this.position = new Vector2((Shared.stage.X - ((tex.Width / COL)/2)), ((Shared.stage.Y - tex.Height) / 2));
            this.speed = new Vector2(0, 5);
            frames = new Rectangle[COL];
            for (int i = 0; i < COL; i++)
            {
                this.frames[i] = new Rectangle(i * (tex.Width / COL), 0, (tex.Width / COL), tex.Height);
            }
            this.origin = new Vector2((tex.Width / COL) / 2, tex.Height / 2);
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            if (frameIndexCol > 0)
            {
                spriteBatch.Draw(tex, position, frames[frameIndexCol], Color.White, rotation, origin, scale, SpriteEffects.None, 0);
            }
            else
            {
                spriteBatch.Draw(tex, position, frames[0], Color.White, rotation, origin, scale, SpriteEffects.None, 0);
            }
            spriteBatch.End();

            base.Draw(gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            KeyboardState ks = Keyboard.GetState();
            if (ks.IsKeyDown(Keys.Up))
            {
                position -= speed;
                if (position.Y < tex.Height/2)
                {
                    position.Y = tex.Height / 2;
                }
            }
            if (ks.IsKeyDown(Keys.Down))
            {
                position += speed;
                if (position.Y > Shared.stage.Y - tex.Height / 2)
                {
                    position.Y = Shared.stage.Y - tex.Height / 2;
                }
            }


            MouseState ms = Mouse.GetState();
            float xDiff = ms.X - position.X;
            float yDiff = ms.Y - position.Y;
            if (xDiff < 0)
            {
                rotation = (float)Math.Atan(yDiff / xDiff) + (float)Math.PI + (float)Math.PI / 2;
                if (ms.LeftButton == ButtonState.Pressed && oldMouseState.LeftButton == ButtonState.Released)
                {
                    if (frameIndexCol == 0)
                    {
                        frameIndexCol++;
                    }
                }
            }
            if (frameIndexCol != 0)
            {
                if (delayCounter >= delay)
                {
                    frameIndexCol++;
                    delayCounter = 0;
                }
                delayCounter++;
                if (frameIndexCol == COL)
                {
                    frameIndexCol = 0;
                }
            }
            oldMouseState = ms;

            base.Update(gameTime);
        }
    }


}
