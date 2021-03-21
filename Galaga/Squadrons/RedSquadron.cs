using System.Collections.Generic;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;

namespace Galaga.Squadrons
{
    public class RedSquadron : ISquadron
    {
        public EntityContainer<Enemy> Enemies { get; }
        public int MaxEnemies { get; }

        public RedSquadron()
        {
            MaxEnemies = 2;
            Enemies = new EntityContainer<Enemy>();
        }

        /*
        public void CreateEnemies(List<Image> enemyStrides)
        {
            var bandits = new List<Enemy>();
            for (float x = 0.61f, y = 0.6f; x <= 0.8f; x += 0.19f, y += 0.3f)
            {
                bandits.Add(new Enemy(
                    new DynamicShape(new Vec2F(x, y), new Vec2F(0.1f, 0.1f)),
                    new ImageStride(80, enemyStrides)));
            }

            for (int i = 0; i < MaxEnemies; i++)
            {
                Enemies.AddEntity(bandits[i]);
            }
        }

        */
        public void CreateEnemies(List<Image> enemyStrides, List<Image> alternativeEnemyStrides)
        {
            var bandits = new List<Enemy>();
            for (float x = 0.61f, y = 0.6f; x <= 0.8f; x += 0.19f, y += 0.3f)
            {
                bandits.Add(new Enemy(
                    new DynamicShape(new Vec2F(x, y), new Vec2F(0.1f, 0.1f)),
                    new ImageStride(80, enemyStrides)));
            }

            for (int i = 0; i < MaxEnemies - 1; i++)
            {
                Enemies.AddEntity(bandits[i]);
            }
        }

    }
}