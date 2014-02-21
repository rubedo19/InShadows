using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
namespace InShadows
{
    class Level
    {
        protected List<Entity> entities;
        protected Camera camera;
        protected PlayerEntity player;

        public Camera Camera
        {
            get { return camera; }
            set { camera = value; }
        }
        public Level()
        {
            entities = new List<Entity>();
        }

        public void addEntity(Entity entity){
            entities.Add(entity);
        }

        public PlayerEntity Player
        {
            get { return player; }
            set { player = value; }
        }

        public void Draw(Renderer2D renderer)
        {
            renderer.BeginScene(camera.get_transformation());
            foreach (Entity e in entities)
            {
                renderer.DrawEntity(e);
            }
            renderer.DrawEntity(player);
            renderer.EndScene();
        }

        public Entity getEntityAt(int x, int y)
        {
            Vector2 point = camera.screenToWorld(new Vector2(x, y));
            Entity retEntity = null;
            foreach (Entity e in entities)
            {
                if (e.containsPoint((int)point.X, (int)point.Y))
                {
                    retEntity = e;
                }
            }
            return retEntity;
        }

        public void Update(GameTime gameTime)
        {
            player.Update(gameTime);
        }
    }
}
