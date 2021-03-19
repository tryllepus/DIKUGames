using System;
using System.IO;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Physics;

namespace Galaga
{
    public class Enemy : Entity
    {
        public Vec2F StartPosition { get; }
        private Player player;
        private float MOVEMENT_SPEED = 0.01f;
        private float New_MOVEMENT_SPEED = 0.0f;
        public int hitPoints { get; private set; }
        public int hitMark { get; private set; }
        public int thresholdHP { get; private set; }
        private IBaseImage enemyStridesRed;
        public bool critCondition { get; private set; }

        public Enemy(DynamicShape shape, IBaseImage image)
            : base(shape, image)
        {
            this.StartPosition = shape.Position.Copy();
            this.hitPoints = 100;
            this.thresholdHP = 30;
            hitMark = 20;

            this.enemyStridesRed = new Image(Path.Combine("Assets", "Images", "RedMonster.png"));

        }

        /// <summary>
        /// Enemy should lose hitpoints upon collision with a playerShot
        /// </summary>
        public void HitMarker()
        {
            {
                hitPoints -= hitMark;
            }
        }
        public void Criticalhealth()
        {
            if (hitPoints <= thresholdHP)
            {
                critCondition = true;
                this.Image = enemyStridesRed;
                New_MOVEMENT_SPEED = 2 * MOVEMENT_SPEED;
                MOVEMENT_SPEED = New_MOVEMENT_SPEED;
            }
        }
        public bool isDead()
        {
            return hitPoints <= 0;
        }

    }
}