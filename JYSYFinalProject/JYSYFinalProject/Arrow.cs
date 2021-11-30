using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace JYSYFinalProject
{
    public class Arrow : DrawableGameComponent
    {
        private SpriteBatch spriteBatch;
        private Texture2D tex;
        private Vector2 position;
        private Vector2 speed;

        private Rectangle srcRect;
        private Vector2 origin;
        private float rotation = 0f;
        private float scale = 1.0f;

        private int timeCounter = 0;
        private int existTime = 60;
        

        public Arrow(Game game,
            SpriteBatch spriteBatch,
            Texture2D tex,
            Vector2 position,
            Vector2 speed, float rotation) : base(game)
        {
            this.spriteBatch = spriteBatch;
            this.tex = tex;
            this.position = position;
            this.speed = speed;
            this.rotation = rotation;
            this.origin = new Vector2(tex.Width / 2, tex.Height / 2);

            this.srcRect = new Rectangle(0, 0, tex.Width, tex.Height);
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(tex, position, srcRect, Color.White, rotation, origin, scale, SpriteEffects.None, 0);
            spriteBatch.End();

            base.Draw(gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            position += speed;
            //top wall
            if (position.Y <= 0)
            {
                this.Enabled = false;
                this.Visible = false;
            }
            //down wall
            if (position.Y >= Shared.stage.Y)
            {
                this.Enabled = false;
                this.Visible = false;
            }
            //left wall
            if (position.X <= 0)
            {
                this.Enabled = false;
                this.Visible = false;
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
