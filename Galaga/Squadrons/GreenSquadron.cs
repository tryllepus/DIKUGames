using System.Collections.Generic;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;

namespace Galaga.Squadrons
{
    public class GreenSquadron : ISquadron
    {
        public EntityContainer<Enemy> Enemies { get; }

        public int MaxEnemies { get; }
        public GreenSquadron()
        {
            MaxEnemies = 3;
            Enemies = new EntityContainer<Enemy>();
        }

        public void CreateEnemies(List<Image> enemyStrides, List<Image> alternativeEnemyStrides)
        {
            var bandits = new List<Enemy>();
            for (float x = 0.15f, y = 0.6f; x <= 0.3f; x += 0.15f, y += 0.2f)
            {
                bandits.Add(new Enemy(
                    new DynamicShape(new Vec2F(x, y), new Vec2F(0.1f, 0.1f)),
                    new ImageStride(80, enemyStrides)));

                for (int i = 0; i < MaxEnemies; i++)
                {
                    Enemies.AddEntity(bandits[i]);
                }
            }
        }
    }
}