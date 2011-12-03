using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Graphite
{
    class Player_class
    {
        public bool active;
        public int health;
        public Vector2 position;
        public Vector2 mousePosition;
        public Vector2 mouseDistance;
        public float stealthCoefficient;
        public float rotationAngle;
        public float crossScale;
        public bool firing;


        public void Initialize()
        {
            
        }

        public void Update()
        {
            //Places the player sprite in the center of the screen
            position = new Vector2((GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width / 2), (GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height / 2));
            //Decides if the player is still kickin' or not
            if (health <= 0)
            {
                active = false;
            }
            stealthCoefficient = 1f;
            

            KeyboardState CurrentState = Keyboard.GetState();
            //animation-related controls
            Keys[] currentPressedKeys = CurrentState.GetPressedKeys();
            foreach (Keys key in currentPressedKeys)
            {
                if (key == Keys.LeftControl)
                {
                    stealthCoefficient -= 0.3f;
                }
                if (key == Keys.LeftShift)
                {
                    stealthCoefficient += 0.2f;
                }

  
            }
            MouseState currentMouseState = Mouse.GetState();
            mousePosition.X = currentMouseState.X - Game1.crosshairTexture.Width / 2;
            mousePosition.Y = currentMouseState.Y - Game1.crosshairTexture.Height / 2;
            if (currentMouseState.LeftButton == ButtonState.Pressed)
            {
                crossScale = 0.8f;
                firing = true;
            }
            else
            {
                crossScale = 1f;
                firing = false;
            }
            mouseDistance = position - mousePosition;

            rotationAngle = (float)Math.Atan2(mouseDistance.X, mouseDistance.Y);         

        }
    }
}
