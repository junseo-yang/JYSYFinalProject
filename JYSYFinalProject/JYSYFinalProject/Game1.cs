using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace JYSYFinalProject
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;

        public SpriteBatch _spriteBatch;

        /* Scene */
        private StartScene startScene;
        private HelpScene helpScene;
        private ActionScene actionScene;
        public const int QUIT = 4;

        /* Character */
        private Character character;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            Shared.stage = new Vector2(_graphics.PreferredBackBufferWidth,
                _graphics.PreferredBackBufferHeight);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here

            /* Scene */
            startScene = new StartScene(this);
            this.Components.Add(startScene);

            helpScene = new HelpScene(this);
            this.Components.Add(helpScene);

            actionScene = new ActionScene(this);
            this.Components.Add(actionScene);

            startScene.show();

            /* Character */
            Vector2 position = new Vector2(Shared.stage.X / 2, Shared.stage.Y / 2);
            character = new Character(this, position, 3);
            actionScene.SceneComponents.Add(character);
        }


        private void hideAllScenes()
        {
            foreach (GameScene item in Components)
            {
                item.hide();
            }
        }


        protected override void Update(GameTime gameTime)
        {
            //if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            //    Exit();

            // TODO: Add your update logic here

            int selectedIndex = 0;
            KeyboardState ks = Keyboard.GetState();
            if (startScene.Enabled)
            {
                selectedIndex = startScene.Menu.SelectedIndex;
                if (selectedIndex == 0 && ks.IsKeyDown(Keys.Enter))
                {
                    startScene.hide();
                    actionScene.show();
                }
                else if (selectedIndex == 1 && ks.IsKeyDown(Keys.Enter))
                {
                    startScene.hide();
                    helpScene.show();
                }
                else if (selectedIndex == QUIT && ks.IsKeyDown(Keys.Enter))
                {
                    Exit();
                }
            }

            if (helpScene.Enabled || actionScene.Enabled)
            {
                if (ks.IsKeyDown(Keys.Escape))
                {
                    hideAllScenes();
                    startScene.show();
                }
            }

            base.Update(gameTime);
        }
       


        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
