using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace InShadows
{

    class Entity
    {
        //Temp hack to add a default texture
        public static Texture2D defaultTexture;

        public static void SetDefaultTexture(Texture2D texture){
            defaultTexture = texture;
        }
        //END hack

        int posX, posY;
        int width, height;

        Texture2D texture;
        Color color;
        Rectangle bounds;
        bool isHighlighted;
        bool isInvisible = false;

        public Rectangle Bounds
        {
            get { return bounds; }
        }

        public bool IsInvisible
        {
            get { return isInvisible; }
            set { isInvisible = value; }
        }

        public Texture2D Texture
        {
            get { return texture; }
            set { texture = value; }
        }

        public Color Color
        {
            get { return (isHighlighted) ? Color.HotPink:color; }
            set { color = value; }
        }

        public Entity(int x, int y, int width, int height)
        {
            this.width = width;
            this.height = height;
            posX = x;
            posY = y;

            bounds = new Rectangle(posX, posY, width, height);
            texture = defaultTexture;
        }

        public void move(int x, int y)
        {
            posX += x;
            posY += y;

            bounds.X = posX;
            bounds.Y = posY;
        }

        public void moveTo(int x, int y)
        {
            posX = x;
            posY = y;

            bounds.X = posX;
            bounds.Y = posY;
        }

        public void setSize(int width, int height)
        {
            this.width = width;
            this.height = height;

            bounds.Width = this.width;
            bounds.Height = this.height;
        }

        public void resize(int width, int height)
        {
            this.width += width;
            this.height += height;

            bounds.Width = this.width;
            bounds.Height = this.height;
        }

        public bool containsPoint(int x, int y)
        {
            return bounds.Contains(x, y);
        }

        public void toggleHighlight()
        {
            isHighlighted = !isHighlighted;
        }
    }
}
