using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace InShadows
{
    class PlayerEntity : Entity
    {

        protected int moveSpeed = 200;
        float posX, posY;
        bool isJumping = false;
        float jumpVelocity = 400;
        float vertVelocity = 0;
        float groundLevelHack;
        public PlayerEntity(int x, int y, int width, int height):
            base(x,y,width, height)
        {
            posX = x;
            posY = y;
            groundLevelHack = posY;
        }

        public void Update(GameTime gameTime)
        {
            KeyboardState keystate = Keyboard.GetState();
            //Console.WriteLine(moveSpeed * (float)gameTime.ElapsedGameTime.Ticks / (float)TimeSpan.TicksPerSecond);
            if (keystate.IsKeyDown(Keys.Left))
            {
                posX -= moveSpeed * (float)gameTime.ElapsedGameTime.Ticks / (float)TimeSpan.TicksPerSecond;
               //Console.WriteLine(moveSpeed * ((float)gameTime.ElapsedGameTime.Milliseconds / 100.0f));
                this.moveTo((int)posX, (int)posY);
            }

            if(keystate.IsKeyDown(Keys.Right)){
                posX += moveSpeed * (float)gameTime.ElapsedGameTime.Ticks / (float)TimeSpan.TicksPerSecond;
                this.moveTo((int)posX, (int)posY);
            }

            if (!isJumping && keystate.IsKeyDown(Keys.Space))
            {
                isJumping = true;
                vertVelocity = jumpVelocity;
            }
           
            if (isJumping)
            {
                posY += vertVelocity * (float)gameTime.ElapsedGameTime.Ticks / (float)TimeSpan.TicksPerSecond;
                vertVelocity -= 2.0f * jumpVelocity * (float)gameTime.ElapsedGameTime.Ticks / (float)TimeSpan.TicksPerSecond;
                if (posY < groundLevelHack)
                {
                    posY = groundLevelHack;
                    isJumping = false;
                }
                this.moveTo((int)posX, (int)posY);
            }
        }

    }
}
