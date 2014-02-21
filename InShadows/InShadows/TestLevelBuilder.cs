using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace InShadows
{
    class TestLevelBuilder
    {
        public static Level buildTestLevel(Texture2D texture)
        {
            Level testLevel = new Level();

            Entity entity = new Entity(0, 20, 800, 30);
            entity.Color = Color.OrangeRed;
            entity.Texture = texture;

            testLevel.addEntity(entity);

            entity = new Entity(40, 75, 32, 32);
            entity.Color = Color.BlanchedAlmond;
            entity.Texture = texture;

            testLevel.addEntity(entity);


            PlayerEntity player = new PlayerEntity(20, 51, 32, 64);
            player.Color = Color.DarkCyan;
            testLevel.Player = player;


            return testLevel;
        }
    }
}
