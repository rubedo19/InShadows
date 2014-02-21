using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace InShadows
{
    //The renderer2D class will be the main class to render 2D images to the screen
    //The renderer will be able to take Entities and render their texture
    //The renderer will contain a copy of the Graphics Component that is used for the system
    class Renderer2D
    {
        GraphicsDevice graphics;
        SpriteBatch sBatch;
        bool isSceneBegun = false;
        Texture2D currTexture;
        SpriteFont font;

        public SpriteFont TextFont
        {
            set { font = value; }
        }

        public Renderer2D(GraphicsDevice graphics)
        {
            this.graphics = graphics;
            sBatch = new SpriteBatch(graphics);
            currTexture = new Texture2D(graphics, 1, 1);
            currTexture.SetData(new Color[] { Color.White });

        }

        public void BeginScene(Matrix camTransform)
        {
            if (!isSceneBegun)
            {
                sBatch.Begin(SpriteSortMode.Deferred, 
                             BlendState.AlphaBlend, 
                             SamplerState.PointClamp, 
                             DepthStencilState.None, 
                             RasterizerState.CullNone, 
                             null, 
                             camTransform);
                isSceneBegun = true;
            }
            
        }

        public void EndScene()
        {
            if (isSceneBegun)
            {
                sBatch.End();
                isSceneBegun = false;
            }
        }

        public void DrawRect(Rectangle rect, Color color){
            if (!isSceneBegun) return;

            sBatch.Draw(currTexture, rect, color);
        }

        public void DrawEntity(Entity entity){
            if (!isSceneBegun || entity == null || entity.IsInvisible) return;

            sBatch.Draw(entity.Texture, entity.Bounds, entity.Color);
        }

        public void DrawText(String text, Vector2 position)
        {
            sBatch.DrawString(font, text, position, Color.Black);
        }
    }
}
