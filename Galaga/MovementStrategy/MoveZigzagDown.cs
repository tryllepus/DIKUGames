using System;
using DIKUArcade.Entities;

namespace Galaga.MovementStrategy
{
    public class MoveZigzagDown : IMovementStrategy
    {
        private float wavePeriod = 0.045f;
        private float MOVEMENT_SPEED = 0.0003f;
        private float amplitude = 0.05f;

        public void MoveEnemies(EntityContainer<Enemy> enemies)
        {
            enemies.Iterate(MoveEnemy);
        }

        public void MoveEnemy(Enemy enemy)
        {
            enemy.Shape.Position.Y -= MOVEMENT_SPEED;
            enemy.Shape.Position.X = enemy.StartPosition.X + amplitude *
                            (float)Math.Sin((2 * Math.PI *
                            (enemy.StartPosition.Y - enemy.Shape.Position.Y)) /
                            wavePeriod);
        }
    }
}