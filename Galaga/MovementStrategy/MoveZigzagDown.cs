using System;
using DIKUArcade.Entities;

namespace Galaga.MovementStrategy
{
    public class MoveZigzagDown : IMovementStrategy
    {
        private float wavePeriod = 0.045f;
        public float MOVEMENT_SPEED = 0.002f; //! Har gjort denne public for at kunne fange den i Game
        private float amplitude = 0.05f;

        public void MoveEnemies(EntityContainer<Enemy> enemies)
        {
            enemies.Iterate(MoveEnemy);
        }

        public void MoveEnemy(Enemy enemy)
        {
            if (enemy.hitPoints > enemy.thresholdHP){
                enemy.Shape.Position.Y -= MOVEMENT_SPEED;
                enemy.Shape.Position.X = enemy.StartPosition.X + amplitude *
                                (float)Math.Sin((2 * Math.PI *
                                (enemy.StartPosition.Y - enemy.Shape.Position.Y)) /
                                wavePeriod);
            }
            else{
                enemy.Criticalhealth();
                enemy.Shape.Position.X = enemy.StartPosition.X + amplitude *
                                (float)Math.Sin((2 * Math.PI *
                                (enemy.StartPosition.Y - enemy.Shape.Position.Y)) /
                                wavePeriod);
            }
        }
    }
}