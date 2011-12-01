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
    class Map_class
    {
        public Vector2 velocity;
        public Vector2 position;
        public void Update()
        {

            KeyboardState CurrentState = Keyboard.GetState();

            //Tune this variable for more balanced walking speeds
            int maxSpeed = 5;


            //Setting controls
            Keys[] currentPressedKeys = CurrentState.GetPressedKeys();
            foreach (Keys key in currentPressedKeys)
            {
                if (key == Keys.W)
                {
                    velocity.Y = velocity.Y + 2;
                }
                if (key == Keys.S)
                {
                    velocity.Y = velocity.Y - 2;
                }
                if (key == Keys.A)
                {
                    velocity.X = velocity.X + 2;
                }
                if (key == Keys.D)
                {
                    velocity.X = velocity.X - 2;
                }
                if (key == Keys.LeftShift && key != Keys.LeftControl)
                {
                    //Tune this variable for more balanced sprinting speed
                    maxSpeed = 8;
                }
                if (key == Keys.LeftControl && key != Keys.LeftShift)
                {
                    //add in bool for crouching
                    maxSpeed = 2;
                }

            }
            //Setting maximum speed limit
            if (velocity.X > maxSpeed)
            {
                velocity.X = maxSpeed;
            }
            if (velocity.X < -maxSpeed)
            {
                velocity.X = -maxSpeed;
            }
            if (velocity.Y > maxSpeed)
            {
                velocity.Y = maxSpeed;
            }
            if (velocity.Y < -maxSpeed)
            {
                velocity.Y = -maxSpeed;
            }

            //Rudimentary friction
            if (velocity.X > 0)
            {
                velocity.X--;
            }
            if (velocity.X < 0)
            {
                velocity.X++;
            }

            if (velocity.Y > 0)
            {
                velocity.Y--;
            }
            if (velocity.Y < 0)
            {
                velocity.Y++;
            }

            //Setting position equal to position + velocity
            position += velocity;
        }
    }
}

