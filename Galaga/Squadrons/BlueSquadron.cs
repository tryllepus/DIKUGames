using System.Collections.Generic;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;

namespace Galaga.Squadrons
{
    public class BlueSquadron : ISquadron
    {
        public EntityContainer<Enemy> Enemies { get; }
        public int MaxEnemies { get; }

        public BlueSquadron()
        {
            MaxEnemies = 3;
            Enemies = new EntityContainer<Enemy>();
        }


        public void CreateEnemies(List<Image> enemyStrides, List<Image> alternativeEnemyStrides)
        {
            var bandits = new List<Enemy>();
            for (float x = 0.31f; x <= 0.6f; x += 0.29f)
            {
                bandits.Add(new Enemy(
                    new DynamicShape(new Vec2F(x, 0.5f), new Vec2F(0.1f, 0.1f)),
                    new ImageStride(80, enemyStrides)));
            }

            for (int i = 0; i < MaxEnemies - 1; i++)
            {
                Enemies.AddEntity(bandits[i]);

            }
        }
        /*

        public void CreateEnemies(List<Image> enemyStrides)
        {
            var bandits = new List<Enemy>();
            for (float x = 0.31f; x <= 0.6f; x += 0.29f)
            {
                bandits.Add(new Enemy(
                    new DynamicShape(new Vec2F(x, 0.5f), new Vec2F(0.1f, 0.1f)),
                    new ImageStride(80, enemyStrides)));
            }

            for (int i = 0; i < MaxEnemies; i++)
            {
                Enemies.AddEntity(bandits[i]);

            }
        }
        */
    }
}