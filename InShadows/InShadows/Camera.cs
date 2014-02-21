using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace InShadows
{
    class Camera
    {
        protected float zoom; // Camera Zoom
        protected Matrix transform; // Matrix Transform
        protected Vector2 pos; // Camera Position
        protected float rotation; // Camera Rotation
        protected Viewport viewport;
        public Matrix Transform
        {
            get { return transform; }
        }

        // Sets and gets zoom
        public float Zoom
        {
            get { return zoom; }
            set { zoom = value; if (zoom < 0.1f) zoom = 0.1f; } // Negative zoom will flip image
        }

        public float Rotation
        {
            get { return rotation; }
            set { rotation = value; }
        }

        // Auxiliary function to move the camera
        public void Move(Vector2 amount)
        {
            pos += amount;
        }
        // Get set position
        public Vector2 Pos
        {
            get { return pos; }
            set { pos = value; }
        }

        public Viewport Viewport
        {
            set { viewport = value; }
        }

        public Camera(Viewport viewport)
        {
            transform = Matrix.Identity;
            zoom = 1.0f;
            pos = Vector2.Zero;
            this.viewport = viewport;
        }

        public Matrix get_transformation()
        {
            transform =       
              Matrix.CreateTranslation(new Vector3(-pos.X, -pos.Y, 0)) *
                                         Matrix.CreateRotationZ(Rotation) *
                                         Matrix.CreateScale(new Vector3(Zoom, -Zoom, 1)) *
                                         Matrix.CreateTranslation(new Vector3(viewport.Width * 0.5f, viewport.Height * 0.5f, 0));
            
            return transform;
        }

        public Vector2 screenToWorld(Vector2 screenPoint)
        {
            return Vector2.Transform(screenPoint, get_transformation());
        }
    }
}
