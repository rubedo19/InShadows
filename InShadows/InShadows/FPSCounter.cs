using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace InShadows
{
    class FPSCounter
    {
        long timeElapsed = 0;
        int framesPerSecond = 0;
        int numFrames = 0;
        public FPSCounter()
        {

        }

        public void tickUpdate(GameTime elapsed){
            //timeElapsed += elapsed.ElapsedGameTime.Milliseconds;

            if (elapsed.TotalGameTime.Seconds != timeElapsed) //seconds resest every minute
            {
                framesPerSecond = numFrames;
                timeElapsed = elapsed.TotalGameTime.Seconds;
                numFrames = 0;
            }
        }

        public void tickDraw()
        {
            numFrames++;
        }

        public void Draw(Renderer2D renderer)
        {
            renderer.BeginScene(Matrix.Identity);
            renderer.DrawText("FPS: " + framesPerSecond , new Vector2(50, 50));
            renderer.EndScene();
        }
    }
}
