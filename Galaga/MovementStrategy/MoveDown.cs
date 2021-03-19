using DIKUArcade.Entities;
using DIKUArcade.Math;

namespace Galaga.MovementStrategy
{
    public class MoveDown : IMovementStrategy
    {
        public void MoveEnemies(EntityContainer<Enemy> enemies)
        {
            enemies.Iterate(MoveEnemy);
        }

        public void MoveEnemy(Enemy enemy)
        {
            enemy.Shape.Position.Y -= 0.001f;
        }
    }
}