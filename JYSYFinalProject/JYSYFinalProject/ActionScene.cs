using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

namespace JYSYFinalProject
{
    public class ActionScene : GameScene
    {
        private SpriteBatch spriteBatch;
        private Bow bow;
        private int fireballCooltime = 0;
        private int tornadoCooltime = 0;
        private MouseState oldMouseState;
        private KeyboardState oldKeyboardState;


        private SoundEffect arrowSound;
        private SoundEffect fireballSound;
        private SoundEffect tornadoSound;


        public ActionScene(Game game) : base(game)
        {
            Game1 g = (Game1)game;
            this.spriteBatch = g._spriteBatch;

            arrowSound = g.Content.Load<SoundEffect>("sound/shoot");
            fireballSound = g.Content.Load<SoundEffect>("sound/FireballSound");
            tornadoSound = g.Content.Load<SoundEffect>("sound/WindSound");

            Texture2D texCastle = g.Content.Load<Texture2D>("images/Castle");
            Castle castle = new Castle(game, spriteBatch, texCastle);

            Texture2D texBow = g.Content.Load<Texture2D>("images/Bow");
            bow = new Bow(game, spriteBatch, texBow);

            this.SceneComponents.Add(castle);
            this.SceneComponents.Add(bow);
        }

        public override void Update(GameTime gameTime)
        {
            MouseState ms = Mouse.GetState();
            float xDiff = ms.X - bow.position.X;
            float yDiff = ms.Y - bow.position.Y;
            if (xDiff < 0)
            {
                if (ms.LeftButton == ButtonState.Pressed && oldMouseState.LeftButton == ButtonState.Released && bow.frameIndexCol==0)
                {
                    Vector2 speed = new Vector2(xDiff * 0.02f, yDiff * 0.02f);
                    Arrow arrow = new Arrow(this.Game, spriteBatch, this.Game.Content.Load<Texture2D>("images/Arrow"), bow.position, speed, bow.rotation);
                    this.SceneComponents.Add(arrow);
                    arrowSound.Play();
                }
                if (ms.RightButton == ButtonState.Pressed && tornadoCooltime == 0)
                {
                    Vector2 speed = new Vector2(xDiff * 0.025f, yDiff * 0.025f);
                    Tornado tornado = new Tornado(this.Game, spriteBatch, this.Game.Content.Load<Texture2D>("images/Tornado"), bow.position, speed);
                    this.SceneComponents.Add(tornado);
                    tornadoCooltime = 300;
                    tornadoSound.Play();
                }
            }


            KeyboardState ks = Keyboard.GetState();

            if(ks.IsKeyDown(Keys.Left) && oldKeyboardState.IsKeyUp(Keys.Left) && fireballCooltime == 0)
            {
                Fireball fireball = new Fireball(this.Game, spriteBatch, this.Game.Content.Load<Texture2D>("images/Fireball"), bow.position);
                this.SceneComponents.Add(fireball);
                fireballCooltime = 200;
                fireballSound.Play();
            }

            if(fireballCooltime>0)
            {
                fireballCooltime--;
            }
            if(tornadoCooltime > 0)
                {
                tornadoCooltime--;
            }



            oldMouseState = ms;
            oldKeyboardState = ks;


            base.Update(gameTime);
        }
    }
}
