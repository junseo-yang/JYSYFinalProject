using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace JYSYFinalProject
{
    public class StartScene : GameScene
    {
        private MenuComponent menu;
        public MenuComponent Menu { get => menu; set => menu = value; }

        private SpriteBatch spriteBatch;

        string[] menuItems = { "Start game", "Help", "High Score", "Credit", "Quit" };

        public StartScene(Game game) : base(game)
        {
            Game1 g = (Game1)game;
            this.spriteBatch = g._spriteBatch;
            SpriteFont regularFont = g.Content.Load<SpriteFont>("fonts/regularFont");
            SpriteFont hilightFont = g.Content.Load<SpriteFont>("fonts/hilightFont");

            menu = new MenuComponent(g, spriteBatch, regularFont, hilightFont, menuItems);
            this.Components.Add(menu);

        }
    }
}
