using System.Collections.Generic;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;

namespace Galaga
{
    public interface IBander
    {
        EntityContainer<Enemy> enemies { get; }
        int TotalEnemies { get; }

        void EnemyCreation(List<Image> enemyStrides);
    }
}