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
            MaxEnemies = 6;
            Enemies = new EntityContainer<Enemy>();
        }




        public void CreateEnemies(List<Image> enemyStrides, List<Image> alternativeEnemyStrides)
        {
            for (int i = 0; i < MaxEnemies; i++)
            {
                Enemies.AddEntity(new Enemy(
                    new DynamicShape(new Vec2F(0.2f + (float)i * 0.1f, 0.9f),
                    new Vec2F(0.1f, 0.1f)),
                    new ImageStride(80, enemyStrides)));
            }
        }


    }
}