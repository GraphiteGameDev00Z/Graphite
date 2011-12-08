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
    public class Main : Microsoft.Xna.Framework.Game
    {
        //--Content
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Color BackColour = Color.Lavender;

        //--Sprites

        //--Sounds

        //Class Files
        WorldPlayers WP = new WorldPlayers();
        TCPServer_Class TCP = new TCPServer_Class();
        //Variables

        public Main()
        {
            graphics = new GraphicsDeviceManager(this);
            //Full Screen \ Resolution

        }
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            WP.Initialize();
            base.Initialize();
        }
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            //load game content

        }
        protected override void UnloadContent()
        {
            //Unload any non ContentManager content
        }
        protected override void Update(GameTime gameTime)
        {
            //Update WorldPlayers
            WP.Update();
            base.Update(gameTime);
        }
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(BackColour);
            //DRAW
            for (int i = 1; i < WP.intNumPlayer; i++)
            {
            //  WP.Players[i].Username;
            }
            base.Draw(gameTime);
        }
    }
}
