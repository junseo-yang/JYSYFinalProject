using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace JYSYFinalProject
{
    public class Fireball : DrawableGameComponent
    {
        private SpriteBatch spriteBatch;
        private Texture2D tex;
        private Vector2 position;
        private Vector2 speed;
        private Rectangle[] frames;
        private Vector2 origin;

        private int delay = 1;
        public int frameIndexCol = 0;
        private int delayCounter = 0;

        private const int COL = 5;

        public Fireball(Game game,
            SpriteBatch spriteBatch,
            Texture2D tex,
            Vector2 position) : base(game)
        {
            this.spriteBatch = spriteBatch;
            this.tex = tex;
            this.position = position;
            this.speed = new Vector2(-7,0);
            frames = new Rectangle[COL];
            for(int i=0;i<COL;i++)
            {
                this.frames[i] = new Rectangle(i * (tex.Width / COL), 0, (tex.Width / COL), tex.Height);
            }
            this.origin.X = (tex.Width / COL) / 2;
            this.origin.Y = tex.Height / 2;
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
            base.Update(gameTime);
        }
    }
}
