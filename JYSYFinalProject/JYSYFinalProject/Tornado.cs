using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace JYSYFinalProject
{
    public class Tornado : DrawableGameComponent
    {
        private SpriteBatch spriteBatch;
        private Texture2D tex;
        private Vector2 position;
        private Vector2 speed;

        private Rectangle[] frames;
        private Vector2 origin;

        private int frameIndexCol = 0;
        private int delay = 1;
        private int delayCounter = 0;

        private const int COL = 4;

        private int timeCounter = 0;
        private int existTime = 300;

        public Tornado(Game game,
            SpriteBatch spriteBatch,
            Texture2D tex,
            Vector2 position,
            Vector2 speed) : base(game)
        {
            this.spriteBatch = spriteBatch;
            this.tex = tex;
            this.position = position;
            this.speed = speed;

            frames = new Rectangle[COL];
            for(int i = 0; i < COL; i++)
            {
                this.frames[i] = new Rectangle(i * (tex.Width / COL), 0, (tex.Width / COL), tex.Height);
            }
            this.origin = new Vector2((tex.Width / COL) / 2, tex.Height / 2);
        }
        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(tex, position, frames[frameIndexCol], Color.White, 0f, origin, 1.0f, SpriteEffects.None, 0);
            spriteBatch.End();

            base.Draw(gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            position += speed;
            //top wall
            if (position.Y <= 0)
            {
                speed.Y = -speed.Y;
            }
            //down wall
            if (position.Y >= Shared.stage.Y)
            {
                speed.Y = -speed.Y;
            }
            //left wall
            if (position.X <= 0)
            {
                this.Enabled = false;
                this.Visible = false;
            }

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

            if (timeCounter >= existTime)
            {
                this.Enabled = false;
                this.Visible = false;
            }
            timeCounter++;


            base.Update(gameTime);
        }
    }
}
