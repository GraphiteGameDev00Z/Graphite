using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Graphite
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        //Declaring variables
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        public static Texture2D playerTexture;
        public static Texture2D MapTexture;
        public static Texture2D crosshairTexture;
        Vector2 position;
        Vector2 mapPosition;
        Vector2 mousePosition;
        Player_class pc = new Player_class();
        World_class worldmap = new World_class();


        public Game1()
        {

            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferHeight = 1080;
            graphics.PreferredBackBufferWidth = 1920;
            graphics.SynchronizeWithVerticalRetrace = true;
            graphics.IsFullScreen = true;
            Content.RootDirectory = "Content";
            



        }


        protected override void Initialize()
        {
            //initializing the pc
            pc.Initialize();
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            playerTexture = Content.Load<Texture2D>("1320811284797");
            MapTexture = Content.Load<Texture2D>("testmap");
            crosshairTexture = Content.Load<Texture2D>("MYSTERY-OF-THE-PORK");  

        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }


        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();
            //update thine player class
            pc.Update();
            //update thine map class
            worldmap.Update();

            
           

            //getting positions
            mapPosition = worldmap.position;
            position = pc.position;
            mousePosition = pc.mousePosition;

            
            
            base.Update(gameTime);
            
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
            spriteBatch.Draw(MapTexture, mapPosition, Color.White);

            spriteBatch.Draw(
                playerTexture, 
                position, 
                null, 
                new Color(255, 255, 255, (byte)MathHelper.Clamp(pc.stealthCoefficient * 255, 0, 255)),
                pc.rotationAngle * -1, 
                new Vector2(playerTexture.Width / 2, playerTexture.Height / 2), 
                new Vector2(1, 1), 
                SpriteEffects.None, 
                0
                );

            spriteBatch.Draw(
                crosshairTexture, 
                new Vector2(mousePosition.X - crosshairTexture.Width / 2, mousePosition.Y - crosshairTexture.Height / 2), 
                null, 
                Color.White,
                0, 
                new Vector2(crosshairTexture.Width / 2, crosshairTexture.Height / 2 ),
                new Vector2(pc.crossScale, pc.crossScale),
                SpriteEffects.None,
                0
                );
            spriteBatch.End();
          
            base.Draw(gameTime);
        }
    }
}
