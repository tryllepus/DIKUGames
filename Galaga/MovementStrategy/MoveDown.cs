using DIKUArcade.Entities;
using DIKUArcade.Math;

namespace Galaga.MovementStrategy
{
    public class MoveDown : IMovementStrategy
    {
        public float MOVEMENT_SPEED = 0.002f; //! Har oprettet denne public speed
        public void MoveEnemies(EntityContainer<Enemy> enemies)
        {
            enemies.Iterate(MoveEnemy);
        }

        public void MoveEnemy(Enemy enemy)
        {
            if (enemy.hitPoints > enemy.thresholdHP){
                enemy.Shape.Position.Y -= MOVEMENT_SPEED;
            }
            else{
                enemy.Criticalhealth();
            }
        }
    }
}