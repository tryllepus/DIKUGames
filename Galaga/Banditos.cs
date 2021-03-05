using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;



namespace Galaga
{

    public class Banditos : IBander
    {
        public EntityContainer<Enemy> enemies { get; }

        public int TotalEnemies { get; }

        public Banditos()
        {
            this.TotalEnemies = 2;
            this.enemies = new EntityContainer<Enemy>();
        }

        public void EnemyCreation(List<Image> enemyStrides)
        {
            var bandit = new List<Enemy>();
            /*
            for (float x = 0.20f, y = 0.8f; x <= 0.3f; x += 0.20f, y += 0.2f)
            {
                bandit.Add(new DynamicShape(new Vec2F(x, y), new Vec2F(0.1f, 0.1f)),
                                            new ImageStride(80, enemyStrides));
            }
            */
        }
    }
}