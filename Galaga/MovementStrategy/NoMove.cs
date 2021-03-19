using DIKUArcade.Entities;
using DIKUArcade.Math;

namespace Galaga.MovementStrategy
{
    public class NoMove : IMovementStrategy
    {
        public void MoveEnemies(EntityContainer<Enemy> enemies)
        {
            enemies.Iterate(MoveEnemy);
        }

        public void MoveEnemy(Enemy enemy)
        {
            enemy.Shape.Position = new Vec2F(0.0f, 0.0f);
        }
    }
}